using AIMS.Enum;
using AIMS.Models.Entities;
using AIMS.Services;
using AIMS.Utils;
using AIMS.Views.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIMS.Controllers.Order
{
    public class PlaceOrderController
    {
        private readonly IMediaService mediaService;
        public OrderData orderData;
        public List<CartItem> CartItems;

        public PlaceOrderController(IMediaService mediaService)
        {
            this.mediaService = mediaService;
            orderData = new OrderData();
        }
        
        public async Task LoadItemsOrder()
        {
            PlaceOrderView.Instance.tempOrderItemBindingSource.Clear();
            if (CartItems.Count > 0)
            {
                foreach (var item in CartItems)
                {
                    if (item != null)
                    {
                        AIMS.Models.Entities.Media currentItem = await mediaService.GetMediaByIdAsync(item.media_id);
                        if (currentItem != null)
                        {
                            PlaceOrderView.Instance.tempOrderItemBindingSource.Add(new TempOrderItem()
                            {
                                mediaID = item.media_id,
                                quantity = item.quantity,
                                mediaName = currentItem.Title,
                                price = currentItem.Price * item.quantity,
                                isReady = currentItem.isEnough(item.quantity),
                                isSupportRushOrder = currentItem.IsSupportRushShipping
                            });
                        }
                    }
                }
            }
            
        }

        public int CountItemIsNotEnough()
        {
            int count = 0;
            foreach (var item in GetOrderItems())
            {
                if (item != null && !item.isReady)
                    count++;
            }
            return count;
        }

        private List<TempOrderItem> GetOrderItems()
        {
            return PlaceOrderView.Instance.tempOrderItemBindingSource.Cast<TempOrderItem>().ToList();
        }

        public int CountItemSupportedRushOrder()
        {
            int count = 0;
            foreach (var item in GetOrderItems())
            {
                if (item.isSupportRushOrder)
                    count++;
            }
            return count;
        }

        public async Task CreateOrder()
        {
            List<OrderMedia> orderMedias = GetOrderItems().Select(item => new OrderMedia()
            {
                mediaID = item.mediaID,
                quantity = item.quantity,
                price = item.price
            }).ToList();
            await new OrderService().CreateOrderAsync(orderData, orderMedias);
        }

        public async Task SetOrderData(string name, string phoneNumber, string address, string selectedCity,string selectedDistrict, 
            string selectedWard, int shippingFee, bool isRushOrder, string deliveryTime, string description)
        {
            orderData.fullname = name;
            orderData.province = selectedCity;
            orderData.address = address + ", " + selectedWard + ", " + selectedDistrict;
            orderData.phone = phoneNumber;
            orderData.shippingFee = shippingFee;
            orderData.totalPrice = await getTotalPrice(selectedCity, isRushOrder);
            orderData.type = isRushOrder ? OrderTypeEnum.Rush.ToString() :
                    OrderTypeEnum.Normal.ToString();
            orderData.instructions = deliveryTime + " - " + description;
            orderData.status = StatusEnum.Pending.ToString();
        }

        public string GetStringTotalMoneyFormat()
        {
            return string.Format("{0:N0}", GetTotalPriceWithVAT());
        }

        public async Task<int> getTotalPrice(string selectedCity, bool isRushOrder)
        {
            return GetTotalPriceWithVAT() + await CalculateShippingFee(selectedCity, isRushOrder);
        }

        private int GetTotalPriceWithVAT()
        {
            int total = 0;
            foreach (var item in GetOrderItems())
                total += item.price;
            int vatAmount = (int)(total * Constants.PERCENT_VAT);
            return total + vatAmount; 
        }

        public async Task<int> CalculateShippingFee(string province, bool isRushOrder)
        {
            bool isInnerCity = province.Equals("Thành phố Hà Nội", StringComparison.OrdinalIgnoreCase) ||
                               province.Equals("Thành phố Hồ Chí Minh", StringComparison.OrdinalIgnoreCase);

            int totalItemValue = 0;
            double totalWeight = 0;
            double heaviestItemWeight = 0;
            int rushOrderFee = 0;

            foreach (var item in GetOrderItems())
            {
                double itemWeight = item.quantity * (await mediaService.GetMediaByIdAsync(item.mediaID)).Weight;

                totalWeight += itemWeight;
                heaviestItemWeight = Math.Max(heaviestItemWeight, itemWeight);

                if (item.isSupportRushOrder)
                {
                    rushOrderFee += 10000 * item.quantity;
                }

                totalItemValue += item.value;
            }

            int baseFee;
            double weightLimit = isInnerCity ? 3 : 0.5; 
            int initialPrice = isInnerCity ? 22000 : 30000; 
            int additionalFeePerUnit = 2500; 

            if (heaviestItemWeight <= weightLimit)
            {
                baseFee = initialPrice;
            }
            else
            {
                double excessWeight = heaviestItemWeight - weightLimit;
                int additionalUnits = (int)Math.Ceiling(excessWeight / 0.5);
                baseFee = initialPrice + (additionalUnits * additionalFeePerUnit);
            }

            int freeShippingDiscount = 0;
            if (totalItemValue > 100000)
            {
                freeShippingDiscount = Math.Min(baseFee, 25000);
            }

            return isRushOrder ? Math.Max(0, baseFee - freeShippingDiscount) + rushOrderFee : Math.Max(0, baseFee - freeShippingDiscount);
        }
    }
}
