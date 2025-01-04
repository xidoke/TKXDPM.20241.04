﻿using AIMS.Models.Entities;
using AIMS.Repositories;
using AIMS.Service;
using AIMS.Utils;
using AIMS.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;
using System.Text.Json;

namespace AIMS.Controllers
{
    public class OrderController : Controller
    {
        private readonly IProvinceRepository _provinceRepository;
        private readonly IDistrictRepository _districtRepository;
        private readonly IWardRepository _wardRepository;
        private readonly IMediaRepository _mediaRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly DeliveryInfoValidator _deliveryInforValidator;
        private readonly IShippingFeeService _shippingFeeService;

        public OrderController(IProvinceRepository provinceRepository, IOrderRepository orderRepository, IDistrictRepository districtRepository,
            IWardRepository wardRepository, IMediaRepository mediaRepository, DeliveryInfoValidator deliveryInfoValidator, IShippingFeeService shippingFeeService)
        {
            _provinceRepository = provinceRepository;
            _districtRepository = districtRepository;
            _wardRepository = wardRepository;
            _orderRepository = orderRepository;
            _mediaRepository = mediaRepository;
            _deliveryInforValidator = deliveryInfoValidator;
            _shippingFeeService = shippingFeeService;
        }

        private const string OrderMediaListSessionKey = "OrderMediaList";

        [HttpPost]
        public async Task<IActionResult> ProcessOrderFromCart(List<CartItem> SelectedItems)
        {
            List<CartItem> selectedProducts = SelectedItems.Where(item => item.isSelected).ToList();
            foreach (var item in selectedProducts)
            {
                var media = await _mediaRepository.GetByIdAsync(item.MediaID);
                if (media.Quantity < item.Quantity) return Json(new { success = false, message = $"Không đủ số lượng cho sản phẩm {media.Title}. Số lượng tồn kho: {media.Quantity}" });
            }
            List<OrderMedia> Temp = selectedProducts.Select(item => new OrderMedia
            {
                MediaId = item.MediaID,
                Name = item.MediaName,
                Quantity = item.Quantity,
                Price = (int)item.Price,
            }).ToList();
            HttpContext.Session.SetString(OrderMediaListSessionKey, JsonSerializer.Serialize(Temp));
            return RedirectToAction("PlaceOrderView", "Order");
        }

        [HttpPost]
        public async Task<IActionResult> ProcessOrderDirectly(int mediaID, int mediaQuantity)
        {
            try
            {
                var media = await _mediaRepository.GetByIdAsync(mediaID);
                if (media == null) return Json(new { success = false, message = "Không tìm thấy sản phẩm." });
                if (media.Quantity < mediaQuantity)return Json(new { success = false, message = $"Không đủ số lượng cho sản phẩm {media.Title}. Số lượng tồn kho: {media.Quantity}" });
                var orderMediaList = new List<OrderMedia>
                {
                    new OrderMedia
                    {
                        MediaId = mediaID,
                        Name = media.Title,
                        Quantity = mediaQuantity,
                        Price = media.Price
                    }
                };
                HttpContext.Session.SetString(OrderMediaListSessionKey, JsonSerializer.Serialize(orderMediaList));
                return Json(new { success = true, redirectUrl = Url.Action("PlaceOrderView", "Order") });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi khi đặt hàng: " + ex.Message });
            }
        }

        public IActionResult PlaceOrderView()
        {
            // Check if there is any order media in session
            var orderMediaListJson = HttpContext.Session.GetString(OrderMediaListSessionKey);
            if (string.IsNullOrEmpty(orderMediaListJson))
                return RedirectToAction("CartView", "Cart");
            var orderMediaList = JsonSerializer.Deserialize<List<OrderMedia>>(orderMediaListJson);
            var provinces = _provinceRepository.GetAllAsync().Result;
            ViewBag.Provinces = new SelectList(provinces, "Id", "Name");
            ViewBag.UserEmail = _orderRepository.GetCurrentUserEmail();
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

        private const string OrderDataTempSessionKey = "OrderDataTemp";

        [HttpPost]
        public async Task<IActionResult> SaveOrderData(OrderData orderData, string province, string district, string ward, string shippingMethod)
        {
            string validationResult = _deliveryInforValidator.Validate(orderData.Fullname, orderData.Phone, orderData.Address, province, district, ward, orderData.Email, shippingMethod);
            if (validationResult.Contains("Vui lòng")) return Json(new { success = false, message = validationResult });
            try
            {
                var provinceName = (await _provinceRepository.GetById(province))?.Name;
                var districtName = (await _districtRepository.GetById(district))?.Name;
                var wardName = (await _wardRepository.GetById(ward))?.Name;
                orderData.City = provinceName;
                orderData.Address = $"{orderData.Address}, {wardName}, {districtName}, {provinceName}";
                orderData.Type = shippingMethod;
                var orderMediaListJson = HttpContext.Session.GetString(OrderMediaListSessionKey);
                if (string.IsNullOrEmpty(orderMediaListJson)) return Json(new { success = false, message = "OrderMediaList not found in session." });
                var orderMediaList = JsonSerializer.Deserialize<List<OrderMedia>>(orderMediaListJson);
                if (orderMediaList == null) return Json(new { success = false, message = "Failed to deserialize OrderMediaList." });
                foreach (var item in orderMediaList)
                {
                    if (item == null) return Json(new { success = false, message = "An item in OrderMediaList is null." });
                    if (item.Name == null) return Json(new { success = false, message = "An item in OrderMediaList has a null Name property." });
                    if (item.Quantity <= 0) return Json(new { success = false, message = "An item in OrderMediaList has a non-positive Quantity." });
                    if (item.Price <= 0) return Json(new { success = false, message = "An item in OrderMediaList has a non-positive Price." });
                }
                orderData.ShippingFee = await _shippingFeeService.CalculateShippingFee(orderMediaList, provinceName, shippingMethod.Contains("rush"));
                orderData.TotalPrice = (float)orderMediaList.Sum(item => item.Quantity * item.Price * 1.1)+ orderData.ShippingFee;
                if (orderData == null) return Json(new { success = false, message = "OrderData is null." });
                if (orderData.Fullname == null) return Json(new { success = false, message = "OrderData.Fullname is null." });
                if (orderData.Phone == null) return Json(new { success = false, message = "OrderData.Phone is null." });
                if (orderData.Address == null) return Json(new { success = false, message = "OrderData.Address is null." });
                DateTime vietnamNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
                string formattedDateTime = vietnamNow.ToString("HH:mm:ss dd/MM/yyyy");
                orderData.CreatedAt = formattedDateTime;
                HttpContext.Session.SetString(OrderDataTempSessionKey, JsonSerializer.Serialize(orderData));
                return Json(new { success = true, message = "Order data saved successfully.", shippingFee = orderData.ShippingFee });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"{ex.Message}" });
            }
        }

        [Authorize] 
        public async Task<IActionResult> PlaceOrderHistoryView(string sortOrder, string currentFilter, string searchTerm)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.IdSortParm = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewBag.CreatedAtSortParm = sortOrder == "CreatedAt" ? "createdat_desc" : "CreatedAt";
            ViewBag.TotalPriceSortParm = sortOrder == "TotalPrice" ? "totalprice_desc" : "TotalPrice";
            ViewBag.StatusSortParm = sortOrder == "Status" ? "status_desc" : "Status";
            if (searchTerm != null)
            {
            }
            else
                searchTerm = currentFilter;
            ViewBag.CurrentSearch = searchTerm;
            var userOrders = await _orderRepository.GetAllOrdersAsync();
            if (!String.IsNullOrEmpty(searchTerm))
                userOrders = userOrders.Where(s => s.Id.ToString().Contains(searchTerm)
                                               || s.CreatedAt.Contains(searchTerm)
                                               || s.TotalPrice.ToString().Contains(searchTerm)
                                               || s.Status.ToString().Contains(searchTerm)).ToList();
            switch (sortOrder)
            {
                case "id_desc":
                    userOrders = userOrders.OrderByDescending(s => s.Id).ToList();
                    break;
                case "CreatedAt":
                    userOrders = userOrders.OrderBy(s => DateTime.ParseExact(s.CreatedAt, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture)).ToList();
                    break;
                case "createdat_desc":
                    userOrders = userOrders.OrderByDescending(s => DateTime.ParseExact(s.CreatedAt, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture)).ToList();
                    break;
                case "TotalPrice":
                    userOrders = userOrders.OrderBy(s => s.TotalPrice).ToList();
                    break;
                case "totalprice_desc":
                    userOrders = userOrders.OrderByDescending(s => s.TotalPrice).ToList();
                    break;
                case "Status":
                    userOrders = userOrders.OrderBy(s => s.Status).ToList();
                    break;
                case "status_desc":
                    userOrders = userOrders.OrderByDescending(s => s.Status).ToList();
                    break;
                default:
                    userOrders = userOrders.OrderBy(s => s.Id).ToList();
                    break;
            }
            return View(userOrders);
        }

        public async Task<IActionResult> OrderDetails(int orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null) return NotFound();
            var orderMedias = await _orderRepository.GetOrderMediasByOrderIdAsync(orderId);
            var viewModel = new OrderDetailsViewModel
            {
                Order = order,
                OrderMedias = orderMedias
            };
            return View(viewModel);
        }
    }
}