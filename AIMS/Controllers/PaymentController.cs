using AIMS.Data.Entities;
using AIMS.Service.Interfaces;
using AIMS.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using AIMS.Repositories;
using AIMS.Models;
using AIMS.Utils;
using AIMS.Data.Repositories.Interfaces;
namespace AIMS.Controllers
{
    public class PaymentController : Controller
    {
        private const string OrderMediaListSessionKey = "OrderMediaList";
        private const string OrderDataTempSessionKey = "OrderDataTemp";
        private const string PaymentMethodSessionKey = "PaymentMethod";
        private const string OrderInfoSessionKey = "OrderInfo";
        private readonly IVnPayService _vnPayservice;
        private readonly IOrderRepository _orderRepository;
        private readonly IEmailService _emailService;
        private readonly IMediaRepository _mediaRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICartRepository _cartRepository;
        private readonly IUserRepository _userRepository;
        public PaymentController(IUserRepository userRepository, ICartRepository cartRepository, IVnPayService vnPayservice, IOrderRepository orderRepository, IEmailService emailService, IMediaRepository mediaRepository, IHttpContextAccessor httpContextAccessor)
        {
            _vnPayservice = vnPayservice;
            _orderRepository = orderRepository;
            _emailService = emailService;
            _mediaRepository = mediaRepository;
            _httpContextAccessor = httpContextAccessor;
            _cartRepository = cartRepository;
            _userRepository = userRepository;   
        }

        public IActionResult PaymentInfo()
        {
            var orderMediaListJson = HttpContext.Session.GetString(OrderMediaListSessionKey);
            var orderDataTempJson = HttpContext.Session.GetString(OrderDataTempSessionKey);
            if (string.IsNullOrEmpty(orderMediaListJson) || string.IsNullOrEmpty(orderDataTempJson)) return RedirectToAction("PlaceOrderView", "Order");
            ViewBag.OrderMediaListJson = orderMediaListJson;
            ViewBag.OrderDataTempJson = orderDataTempJson;
            return View();
        }

        [HttpPost]
        public IActionResult SavePaymentMethod(string paymentMethod)
        {
            if (string.IsNullOrEmpty(paymentMethod)) return BadRequest("Payment method is required.");
            HttpContext.Session.SetString(PaymentMethodSessionKey, paymentMethod);
            return CreatePayment(paymentMethod);
        }

        private IActionResult CreatePayment(string paymentMethod)
        {
            var orderDataTempJson = HttpContext.Session.GetString(OrderDataTempSessionKey);
            var orderDataTemp = JsonSerializer.Deserialize<OrderData>(orderDataTempJson);
            switch (paymentMethod)
            {
                case "vnpay":
                    var orderInfo = new VnPayRequest
                    {
                        Amount = orderDataTemp.TotalPrice,
                        CreatedDate = DateTime.Now,
                        OrderId = DateTime.Now.Ticks.ToString()
                    };
                    HttpContext.Session.SetString(OrderInfoSessionKey, JsonSerializer.Serialize(orderInfo));
                    var paymentUrl = _vnPayservice.CreatePaymentUrl(HttpContext, orderInfo);
                    return Json(new { redirectUrl = paymentUrl });
                case "momo":
                    return Json(new { message = "MOMO payment is under development." });
                case "domesticCard":
                    return Json(new { message = "Domestic card payment is under development." });
                default:
                    return BadRequest("Invalid payment method.");
            }
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
                var paymentMethod = HttpContext.Session.GetString(PaymentMethodSessionKey);
                var orderData = new OrderData
                {
                    City = "Hà Nội",
                    Address = ordatDataSession.Address,
                    Phone = ordatDataSession.Phone,
                    Email = ordatDataSession.Email,
                    ShippingFee = ordatDataSession.ShippingFee,
                    Instructions = ordatDataSession.Instructions,
                    Type = paymentMethod,
                    CreatedAt = ordatDataSession.CreatedAt,
                    TotalPrice = ordatDataSession.TotalPrice,
                    Status = "0",
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
                                MediaId = media.Id,
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
                #region Prepare data for email
                decimal totalProductPriceExcludingVAT = orderMedias.Sum(om => om.Price * om.Quantity);
                var order_after_added = await _orderRepository.GetOrderByIdAsync(orderID_after_added);
                decimal totalProductPriceIncludingVAT = totalProductPriceExcludingVAT * 1.1m;
                var emailModel = new OrderDetailsEmailViewModel
                {
                    Order = order_after_added,
                    OrderMedias = orderMedias,
                    TotalProductPriceExcludingVAT = totalProductPriceExcludingVAT,
                    TotalProductPriceIncludingVAT = totalProductPriceIncludingVAT,
                    TransactionId = response.TransactionId,
                    TransactionContent = $"Thanh toán đơn hàng {orderID_after_added}",
                    TransactionDate = DateTime.Now
                };
                #endregion
                #region Send Email
                await _emailService.SendOrderDetailsAsync(emailModel);
                #endregion
                ViewBag.OrderId = orderID_after_added;
                ViewBag.TransactionId = response.TransactionId;
                ViewBag.Amount = orderInfo.Amount;
                ViewBag.PaymentTime = DateTime.Now;
                TempData.Remove("PaymentResult");
                TempData.Remove("OrderId");
                TempData.Remove("TransactionId");
                TempData.Remove("Amount");
                TempData.Remove("PaymentTime");
                #region Clear Item Ordered in cart
                var mediaIdsToRemove = orderMedias.Select(om => om.MediaId).ToList();
                if (User.Identity.IsAuthenticated)
                {
                    foreach (var mediaId in mediaIdsToRemove)
                    {
                        await _cartRepository.DeleteCartItemAsync(mediaId);
                    }
                }
                else
                {
                    var cartController = new CartController(_mediaRepository, _httpContextAccessor, _cartRepository, _userRepository);
                    var cart = cartController.GetCartFromSession();
                    cart.RemoveAll(item => mediaIdsToRemove.Contains(item.MediaID));
                    cartController.SaveCartToSession(cart);
                }
                #endregion
                #region Clear Session of OrderData, OrderInfo, OrderMediaList
                HttpContext.Session.Remove(OrderDataTempSessionKey);
                HttpContext.Session.Remove(OrderInfoSessionKey);
                HttpContext.Session.Remove(OrderMediaListSessionKey);
                HttpContext.Session.Remove(PaymentMethodSessionKey);
                #endregion
                return View("PaymentResult");
            }
            else
            {
                TempData["ErrorMessage"] = "Payment information not found or payment failed.";
                return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult PaymentFail()
        {
            return View();
        }
    }
}