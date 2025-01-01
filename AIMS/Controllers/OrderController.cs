using AIMS.Data.Entities;
using AIMS.Data.Repositories.Interfaces;
using AIMS.Models;
using AIMS.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;

namespace AIMS.Controllers
{
    public class OrderController : Controller
    {
        private readonly IProvinceRepository _provinceRepository;
        private readonly IDistrictRepository _districtRepository;
        private readonly IWardRepository _wardRepository;
        private readonly IVnPayService _vnPayservice;
        private readonly IMediaRepository _mediaRepository;
        private readonly IOrderRepository _orderRepository;
        public OrderController(IProvinceRepository provinceRepository, IOrderRepository orderRepository, IDistrictRepository districtRepository, IWardRepository wardRepository, IVnPayService vnPayservice, IMediaRepository mediaRepository)
        {
            _provinceRepository = provinceRepository;
            _districtRepository = districtRepository;
            _wardRepository = wardRepository;
            _vnPayservice = vnPayservice;
            _orderRepository = orderRepository;
            _mediaRepository = mediaRepository;
        }
        private List<CartItem> GetCartFromSession()
        {
            var cartJson = HttpContext.Session.GetString("Cart");
            if (string.IsNullOrEmpty(cartJson))
                return new List<CartItem>();
            return JsonSerializer.Deserialize<List<CartItem>>(cartJson);
        }
        private const string OrderMediaListSessionKey = "OrderMediaList";
        public IActionResult PlaceOrderView()
        {
            var orderMediaListJson = HttpContext.Session.GetString(OrderMediaListSessionKey);
            if (string.IsNullOrEmpty(orderMediaListJson))
                return RedirectToAction("CartView", "Cart");
            var orderMediaList = JsonSerializer.Deserialize<List<OrderMedia>>(orderMediaListJson);
            var provinces = _provinceRepository.GetAllAsync().Result;
            ViewBag.Provinces = new SelectList(provinces, "Id", "Name");
            return View(orderMediaList);
        }
        [HttpGet]
        public async Task<IActionResult> GetDistrictsByProvince(string provinceId)
        {
            var districts = await _districtRepository.GetDistrictsByProvinceIdAsync(provinceId);
            return Json(districts);
        }

        [HttpGet]
        public async Task<IActionResult> GetWardsByDistrict(string districtId)
        {
            var wards = await _wardRepository.GetWardsByDistrictIdAsync(districtId);
            return Json(wards);
        }

        [HttpGet]
        public async Task<IActionResult> PaymentCallBack()
        {
            var response = _vnPayservice.PaymentExecute(Request.Query);
            if (response == null || response.VnPayResponseCode != "00")
            {
                TempData["Message"] = $"Lỗi thanh toán VN Pay: {response.VnPayResponseCode}";
                return RedirectToAction("PaymentFail");
            }
            var orderInfoJson = HttpContext.Session.GetString(OrderInfoSessionKey);
            if (string.IsNullOrEmpty(orderInfoJson))
            {
                TempData["ErrorMessage"] = "Order information not found.";
                return RedirectToAction("Index", "Home");
            }
            var orderInfo = JsonSerializer.Deserialize<VnPayRequest>(orderInfoJson);
            var orderId = orderInfo.OrderId;
            TempData["PaymentResult"] = "Success"; 
            TempData["OrderId"] = orderId;
            TempData["TransactionId"] = response.TransactionId;
            TempData["Amount"] = orderInfo.Amount.ToString();
            TempData["PaymentTime"] = DateTime.Now; 
            TempData["Message"] = $"Thanh toán VNPay thành công";
            if (TempData["PaymentResult"]?.ToString() == "Success")
            {
                #region Add OrderData/OrderMedia to Database
                var orderDataTempJson = HttpContext.Session.GetString(OrderDataTempSessionKey);
                var ordatDataSession = JsonSerializer.Deserialize<OrderData>(orderDataTempJson);
                var orderData = new OrderData
                {
                    City = "Hà Nội", 
                    Address = ordatDataSession.Address, 
                    Phone = ordatDataSession.Phone,
                    Email = ordatDataSession.Email, 
                    ShippingFee = ordatDataSession.ShippingFee,
                    CreatedAt = ordatDataSession.CreatedAt,
                    Instructions = ordatDataSession.Instructions,
                    Type = "VnPay", 
                    TotalPrice = ordatDataSession.TotalPrice,
                    Status = 0,
                    Fullname = ordatDataSession.Fullname 
                };
                var orderMediaJson = HttpContext.Session.GetString(OrderMediaListSessionKey);
                List<OrderMedia> orderMedias = new List<OrderMedia>();
                if (!string.IsNullOrEmpty(orderMediaJson))
                {
                    var listOrderItem = JsonSerializer.Deserialize<List<OrderMedia>>(orderMediaJson);
                    foreach (var orderMedia in listOrderItem)
                    {
                        var media = await _mediaRepository.GetByIdAsync(orderMedia.MediaId);
                        if (media != null)
                        {
                            orderMedias.Add(new OrderMedia
                            {
                                MediaId =media.Id,
                                Price = media.Price,
                                Quantity = orderMedia.Quantity,
                                Name = media.Title
                            });
                        }
                    }
                }
                var orderID_after_added = await _orderRepository.CreateOrderAsync(orderData);
                foreach (var orderMedia in orderMedias)
                {
                    orderMedia.OrderId = orderID_after_added;
                }
                await _orderRepository.AddOrderMediasAsync(orderMedias);
                #endregion
                ViewBag.OrderId = TempData["OrderId"]?.ToString();
                ViewBag.TransactionId = TempData["TransactionId"]?.ToString();
                if (TempData["Amount"] != null)
                    ViewBag.Amount = decimal.Parse(TempData["Amount"].ToString());
                ViewBag.PaymentTime = TempData["PaymentTime"];
                TempData.Remove("PaymentResult");
                TempData.Remove("OrderId");
                TempData.Remove("TransactionId");
                TempData.Remove("Amount");
                TempData.Remove("PaymentTime");
                HttpContext.Session.Remove(OrderDataTempSessionKey);
                HttpContext.Session.Remove(OrderInfoSessionKey);
                HttpContext.Session.Remove(OrderMediaListSessionKey);
                return View("PaymentResult");
            }
            else
            {
                TempData["ErrorMessage"] = "Payment information not found or payment failed.";
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<int> CalculateShippingFee(List<OrderMedia> list, string province, bool isRushOrder)
        {
            bool isInnerCity = province.Contains("Thành phố Hà Nội") || province.Contains("Thành phố Hồ Chí Minh");
            int totalItemValue = 0;
            double totalWeight = 0;
            double heaviestItemWeight = 0;
            int rushOrderFee = 0;
            foreach (var item in list)
            {
                var media = await _mediaRepository.GetByIdAsync(item.MediaId);
                double itemWeight = item.Quantity * media.Weight;
                totalWeight += itemWeight;
                heaviestItemWeight = Math.Max(heaviestItemWeight, itemWeight);
                if (media.RushSupport)
                    rushOrderFee += 10000 * item.Quantity;
                totalItemValue += media.Value;
            }

            int baseFee;
            double weightLimit = isInnerCity ? 3 : 0.5;
            int initialPrice = isInnerCity ? 22000 : 30000;
            int additionalFeePerUnit = 2500;
            if (heaviestItemWeight <= weightLimit)
                baseFee = initialPrice;
            else
            {
                double excessWeight = heaviestItemWeight - weightLimit;
                int additionalUnits = (int)Math.Ceiling(excessWeight / 0.5);
                baseFee = initialPrice + (additionalUnits * additionalFeePerUnit);
            }
            int freeShippingDiscount = 0;
            if (totalItemValue > 100000)
                freeShippingDiscount = Math.Min(baseFee, 25000);
            return isRushOrder ? Math.Max(0, Math.Abs(baseFee - freeShippingDiscount)) + rushOrderFee : Math.Max(0, baseFee - freeShippingDiscount);
        }
     private const string OrderDataTempSessionKey = "OrderDataTemp";
        [HttpPost]
        public async Task<IActionResult> SaveOrderData(OrderData orderData, string province, string district, string ward, string shippingMethod)
        {
            if (string.IsNullOrEmpty(orderData.Fullname))
            {
                return Json(new { success = false, message = "Vui lòng nhập họ tên người nhận." });
            }
            if (string.IsNullOrEmpty(orderData.Phone))
            {
                return Json(new { success = false, message = "Vui lòng nhập số điện thoại người nhận." });
            }
            if (string.IsNullOrEmpty(orderData.Address))
            {
                return Json(new { success = false, message = "Vui lòng nhập địa chỉ." });
            }
            if (string.IsNullOrEmpty(province))
            {
                return Json(new { success = false, message = "Vui lòng chọn tỉnh/thành." });
            }
            if (string.IsNullOrEmpty(district))
            {
                return Json(new { success = false, message = "Vui lòng chọn quận/huyện." });
            }
            if (string.IsNullOrEmpty(ward))
            {
                return Json(new { success = false, message = "Vui lòng chọn phường/xã." });
            }
            if (string.IsNullOrEmpty(shippingMethod))
            {
                return Json(new { success = false, message = "Vui lòng chọn phương thức vận chuyển." });
            }

            try
            {
                var provinceName = (await _provinceRepository.GetById(province))?.Name;
                var districtName = (await _districtRepository.GetById(district))?.Name;
                var wardName = (await _wardRepository.GetById(ward))?.Name;
                orderData.Address = $"{orderData.Address}, {wardName}, {districtName}, {provinceName}";
                orderData.Type = shippingMethod;
               

                var orderMediaListJson = HttpContext.Session.GetString(OrderMediaListSessionKey);
                if (string.IsNullOrEmpty(orderMediaListJson))
                {
                    return Json(new { success = false, message = "OrderMediaList not found in session." });
                }
                var orderMediaList = JsonSerializer.Deserialize<List<OrderMedia>>(orderMediaListJson);
                if (orderMediaList == null)
                {
                    return Json(new { success = false, message = "Failed to deserialize OrderMediaList." });
                }
                foreach (var item in orderMediaList)
                {
                    if (item == null)
                    {
                        return Json(new { success = false, message = "An item in OrderMediaList is null." });
                    }
                    if (item.Name == null)
                    {
                        return Json(new { success = false, message = "An item in OrderMediaList has a null Name property." });
                    }
                    if (item.Quantity <= 0)
                    {
                        return Json(new { success = false, message = "An item in OrderMediaList has a non-positive Quantity." });
                    }
                    if (item.Price <= 0)
                    {
                        return Json(new { success = false, message = "An item in OrderMediaList has a non-positive Price." });
                    }
                }
                if (shippingMethod == "rush")
                {
                    orderData.ShippingFee = await CalculateShippingFee(orderMediaList, provinceName, true);
                }
                else
                {
                    orderData.ShippingFee = await CalculateShippingFee(orderMediaList, provinceName, false);
                }

                orderData.TotalPrice = (float)orderMediaList.Sum(item => item.Quantity * item.Price * 1.1) + orderData.ShippingFee;

                if (orderData == null)
                {
                    return Json(new { success = false, message = "OrderData is null." });
                }

                if (orderData.Fullname == null)
                {
                    return Json(new { success = false, message = "OrderData.Fullname is null." });
                }

                if (orderData.Phone == null)
                {
                    return Json(new { success = false, message = "OrderData.Phone is null." });
                }

                if (orderData.Address == null)
                {
                    return Json(new { success = false, message = "OrderData.Address is null." });
                }

                HttpContext.Session.SetString(OrderDataTempSessionKey, JsonSerializer.Serialize(orderData));

                return Json(new { success = true, message = "Order data saved successfully.", shippingFee = orderData.ShippingFee });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return Json(new { success = false, message = $"Error saving order data: {ex.Message}" });
            }
        }

        [HttpPost]
        public IActionResult CreatePaymentUrl()
        {
            var orderDataTempJson = HttpContext.Session.GetString(OrderDataTempSessionKey);
            if (string.IsNullOrEmpty(orderDataTempJson))
            {
                TempData["ErrorMessage"] = "Order information not found.";
                return RedirectToAction("PlaceOrderView");
            }

            var orderDataTemp = JsonSerializer.Deserialize<OrderData>(orderDataTempJson);

            var orderInfo = new VnPayRequest
            {
                Amount = orderDataTemp.TotalPrice,
                CreatedDate = DateTime.Now,
                OrderId = DateTime.Now.Ticks.ToString()
            };
            HttpContext.Session.SetString(OrderInfoSessionKey, JsonSerializer.Serialize(orderInfo));
            var paymentUrl = _vnPayservice.CreatePaymentUrl(HttpContext, orderInfo);
            return Redirect(paymentUrl);
        }
        private const string OrderInfoSessionKey = "OrderInfo";


    }
}
