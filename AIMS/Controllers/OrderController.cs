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
        public OrderController(IProvinceRepository provinceRepository, IDistrictRepository districtRepository, IWardRepository wardRepository, IVnPayService vnPayservice, IMediaRepository mediaRepository)
        {
            _provinceRepository = provinceRepository;
            _districtRepository = districtRepository;
            _wardRepository = wardRepository;
            _vnPayservice = vnPayservice;
            _mediaRepository = mediaRepository;
        }
        private List<CartItem> GetCartFromSession()
        {
            var cartJson = HttpContext.Session.GetString("Cart");
            if (string.IsNullOrEmpty(cartJson))
            {
                return new List<CartItem>();
            }
            return JsonSerializer.Deserialize<List<CartItem>>(cartJson);
        }
        private const string OrderMediaListSessionKey = "OrderMediaList";
        public IActionResult PlaceOrderView()
        {
            var orderMediaListJson = HttpContext.Session.GetString(OrderMediaListSessionKey);

            if (string.IsNullOrEmpty(orderMediaListJson))
            {
                return RedirectToAction("CartView", "Cart");
            }

            var orderMediaList = JsonSerializer.Deserialize<List<OrderMedia>>(orderMediaListJson);
            var provinces = _provinceRepository.GetAllAsync().Result;
            ViewBag.Provinces = new SelectList(provinces, "Id", "Name");
            //HttpContext.Session.Remove(OrderMediaListSessionKey);
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

        [Authorize]
        public IActionResult PaymentCallBack()
        {
            var response = _vnPayservice.PaymentExecute(Request.Query);

            if (response == null || response.VnPayResponseCode != "00")
            {
                TempData["Message"] = $"Lỗi thanh toán VN Pay: {response.VnPayResponseCode}";
                return RedirectToAction("PaymentFail");
            }

            TempData["Message"] = $"Thanh toán VNPay thành công";
            return RedirectToAction("PaymentSuccess");
        }
        public async Task<int> CalculateShippingFee(List<OrderMedia> list, string province, bool isRushOrder)
        {
            bool isInnerCity = province.Equals("Thành phố Hà Nội", StringComparison.OrdinalIgnoreCase) ||
                               province.Equals("Thành phố Hồ Chí Minh", StringComparison.OrdinalIgnoreCase);

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
                {
                    rushOrderFee += 10000 * item.Quantity;
                }

                totalItemValue += media.Value;
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
               

                // 6. Tính toán TotalPrice (đã bao gồm phí vận chuyển)
                var orderMediaListJson = HttpContext.Session.GetString(OrderMediaListSessionKey);
                // Kiểm tra null trước khi deserialize
                if (string.IsNullOrEmpty(orderMediaListJson))
                {
                    return Json(new { success = false, message = "OrderMediaList not found in session." });
                }

                var orderMediaList = JsonSerializer.Deserialize<List<OrderMedia>>(orderMediaListJson);
                // Kiểm tra null sau khi deserialize và các item trong list
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
                    // Kiểm tra các property bắt buộc (non-nullable)
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

                orderData.TotalPrice = orderMediaList.Sum(item => item.Quantity * item.Price) + orderData.ShippingFee;

                // 7. Lưu OrderDataTemp vào Session
                // Kiểm tra null trước khi serialize
                if (orderData == null)
                {
                    return Json(new { success = false, message = "OrderData is null." });
                }

                // Kiểm tra null cho các thuộc tính quan trọng của orderData trước khi serialize
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

                // Thêm kiểm tra các property khác của orderData nếu cần thiết

                HttpContext.Session.SetString(OrderDataTempSessionKey, JsonSerializer.Serialize(orderData));

                // 8. Trả về kết quả thành công, bao gồm shippingFee đã tính toán
                return Json(new { success = true, message = "Order data saved successfully.", shippingFee = orderData.ShippingFee });
            }
            catch (Exception ex)
            {
                // Log lỗi ra console hoặc file log để debug
                Console.WriteLine(ex.ToString());

                // Trả về thông tin lỗi cho client
                return Json(new { success = false, message = $"Error saving order data: {ex.Message}" });
            }
        }

        [HttpPost]
        public IActionResult CreatePaymentUrl(OrderData orderData)
        {
            var orderDataTempJson = HttpContext.Session.GetString(OrderDataTempSessionKey);
            if (string.IsNullOrEmpty(orderDataTempJson))
            {
                // Xử lý trường hợp không tìm thấy OrderDataTemp
                TempData["ErrorMessage"] = "Order information not found.";
                return RedirectToAction("PlaceOrderView");
            }

            var orderDataTemp = JsonSerializer.Deserialize<OrderData>(orderDataTempJson);

            var orderInfo = new VnPayRequest
            {
                Amount = orderDataTemp.TotalPrice,
                CreatedDate = DateTime.Now,
                OrderId = int.Parse(DateTime.Now.Ticks.ToString()) // Or use a better unique identifier
            };

            var paymentUrl = _vnPayservice.CreatePaymentUrl(HttpContext, orderInfo);
            return Redirect(paymentUrl);
        }
    }
}
