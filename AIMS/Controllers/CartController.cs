using AIMS.Models.Entities;
using AIMS.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace AIMS.Controllers
{
    public class CartController : Controller
    {
        private readonly IMediaRepository _mediaRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICartRepository _cartRepository;
        private readonly IUserRepository _userRepository;

        public CartController(IMediaRepository mediaRepository, IHttpContextAccessor httpContextAccessor, ICartRepository cartRepository, IUserRepository userRepository)
        {
            _mediaRepository = mediaRepository;
            _httpContextAccessor = httpContextAccessor;
            _cartRepository = cartRepository;
            _userRepository = userRepository;
        }

        private const string CartSessionKey = "Cart";

        public async Task<IActionResult> CartView()
        {
            List<CartItem> cart;
            if (User.Identity.IsAuthenticated)
            {
                cart = await _cartRepository.GetCartItemsAsync(_userRepository.GetCurrentUserEmail());
            }
            else
            {
                cart = GetCartFromSession();
            }
            ViewBag.GrandTotal = cart.Sum(item => item.Price * item.Quantity);
            return View(cart);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int mediaID, int mediaQuantity)
        {
            try
            {
                var media = await _mediaRepository.GetByIdAsync(mediaID);
                if (media == null) return Json(new { success = false, message = "Không tìm thấy sản phẩm." });

                var product = new CartItem
                {
                    MediaID = mediaID,
                    MediaName = media.Title,
                    Quantity = mediaQuantity,
                    MediaImgUrl = media.ImgUrl,
                    Price = media.Price,
                    Status = "---------"
                };

                if (User.Identity.IsAuthenticated)
                {
                    // Add to database
                    var existingCartItem = await _cartRepository.GetCartItemByIdAsync(mediaID);
                    if (existingCartItem != null)
                    {
                        existingCartItem.Quantity += mediaQuantity;
                        await _cartRepository.UpdateCartItemAsync(existingCartItem);
                    }
                    else
                    {
                        product.Email = _userRepository.GetCurrentUserEmail();
                        await _cartRepository.AddCartItemAsync(product);
                    }
                }
                else
                {
                    // Add to session
                    var cart = GetCartFromSession();
                    var existingItem = cart.FirstOrDefault(i => i.MediaID == mediaID);
                    if (existingItem != null) existingItem.Quantity += mediaQuantity; else cart.Add(product);
                    SaveCartToSession(cart);
                }

                return Json(new { success = true, message = "Thêm giỏ hàng thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi khi thêm vào giỏ hàng: " + ex.Message });
            }
        }
        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            if (User.Identity.IsAuthenticated)
            {
                await _cartRepository.DeleteCartItemAsync(productId);
            }
            else
            {
                var cart = GetCartFromSession();
                var itemToRemove = cart.FirstOrDefault(i => i.MediaID == productId);
                if (itemToRemove != null) cart.Remove(itemToRemove);
                SaveCartToSession(cart);
            }
            return RedirectToAction("CartView");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCart()
        {
            if (!User.Identity.IsAuthenticated)
            {
                // Xử lý logic cho cart dựa trên session (giữ nguyên code của bạn)
                string jsonString;
                using (StreamReader reader = new StreamReader(HttpContext.Request.Body))
                {
                    jsonString = await reader.ReadToEndAsync();
                }

                // Log JSON string for debugging
                Console.WriteLine("Received JSON: " + jsonString);

                // Deserialize JSON sang List<UpdateCartRequest>
                List<UpdateCartRequest> updateRequests = string.IsNullOrEmpty(jsonString) ? new List<UpdateCartRequest>() : JsonSerializer.Deserialize<List<UpdateCartRequest>>(jsonString);

                if (updateRequests == null || !updateRequests.Any())
                {
                    return Json(new { success = false, message = "updateRequests is null or empty" });
                }
                var cart = GetCartFromSession();
                foreach (var request in updateRequests)
                {
                    var itemToUpdate = cart.FirstOrDefault(i => i.MediaID == request.productId);
                    if (itemToUpdate != null)
                    {
                        itemToUpdate.Quantity = request.quantity;
                    }
                }
                SaveCartToSession(cart);
                return Json(new { success = true });
            }

            // Xử lý logic cập nhật database khi đã đăng nhập
            try
            {
                // Đọc dữ liệu JSON thô từ Request.Body
                string jsonString;
                using (StreamReader reader = new StreamReader(HttpContext.Request.Body))
                {
                    jsonString = await reader.ReadToEndAsync();
                }

                // Log JSON string for debugging
                Console.WriteLine("Received JSON: " + jsonString);

                // Cấu hình JsonSerializerOptions để deserialize không phân biệt chữ hoa/thường
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                // Deserialize JSON sang List<UpdateCartRequest>
                List<UpdateCartRequest> updateRequests = string.IsNullOrEmpty(jsonString) ? new List<UpdateCartRequest>() : JsonSerializer.Deserialize<List<UpdateCartRequest>>(jsonString, options);

                if (updateRequests == null || !updateRequests.Any())
                {
                    return Json(new { success = false, message = "Database Cart: updateRequests is null or empty" });
                }

                foreach (var request in updateRequests)
                {
                    var existingCartItem = await _cartRepository.GetCartItemByIdAsync(request.productId);
                    if (existingCartItem != null)
                    {
                        existingCartItem.Quantity = request.quantity;
                        existingCartItem.isSelected = request.isSelected;
                        await _cartRepository.UpdateCartItemAsync(existingCartItem);
                    }
                }

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                // Log the exception message for debugging
                Console.WriteLine("Error in UpdateCart: " + ex.Message);
                return Json(new { success = false, message = ex.Message });
            }
        }
        public List<CartItem> GetCartFromSession()
        {
            // Lấy Items từ Session
            var cartJson = _httpContextAccessor.HttpContext?.Session.GetString(CartSessionKey);
            if (string.IsNullOrEmpty(cartJson)) return new List<CartItem>();
            return JsonSerializer.Deserialize<List<CartItem>>(cartJson);
        }

        public void SaveCartToSession(List<CartItem> cart)
        {
            var cartJson = JsonSerializer.Serialize(cart);
            _httpContextAccessor.HttpContext?.Session.SetString(CartSessionKey, cartJson);
        }

        [HttpPost]
        public async Task<IActionResult> CheckStock(int productId, int quantity)
        {
            var product = await _mediaRepository.GetByIdAsync(productId);

            // Potential NullReferenceException if product is null
            if (product.Quantity >= quantity)
            {
                return Json(new { status = "Đủ số lượng" });
            }
            else
            {
                return Json(new { status = $"Không đủ số lượng. Hiện có: {product.Quantity}" });
            }
        }
        public class UpdateCartRequest
        {
            public int productId { get; set; }
            public int quantity { get; set; }
            public bool isSelected { get; set; }
        }
    }
}