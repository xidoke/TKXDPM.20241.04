using AIMS.Models.Entities;
using AIMS.Services;
using AIMS.Views.Order;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIMS.Controllers.Order
{
    public class PlaceOrderController
    {
        private MediaService mediaService;
        public PlaceOrderController()
        {
            mediaService = new MediaService();
        }
        public List<CartItem> CartItems;
        
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
        public int countItemIsNotEnough()
        {
            int count = 0;
            foreach (var item in GetOrderItems())
            {
                if (item != null && !item.isReady)
                    count++;
            }
            return count;
        }
        public List<TempOrderItem> GetOrderItems()
        {
            return PlaceOrderView.Instance.tempOrderItemBindingSource.Cast<TempOrderItem>().ToList();
        }
        public int countItemSupportedRushOrder()
        {
            int count = 0;
            foreach (var item in GetOrderItems())
            {
                if (item.isSupportRushOrder)
                    count++;
            }
            return count;
        }
        public string GetStringTotalMoneyFormat()
        {
            return string.Format("{0:N0}", GetTotalMoneyWithVAT());
        }
        public double GetTotalMoneyWithVAT()
        {
            double total = 0;
            foreach (var item in GetOrderItems())
                total += item.price;
            double vatAmount = total * 0.1;
            return total + vatAmount;
        }
    }
}
