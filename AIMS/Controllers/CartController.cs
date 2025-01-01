using AIMS.Data.Entities;
using AIMS.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text.Json;

namespace AIMS.Controllers
{
    public class CartController : Controller
    {
        private readonly IMediaRepository _mediaRepository;

        public CartController(IMediaRepository mediaRepository)
        {
            _mediaRepository = mediaRepository;
        }

        private const string CartSessionKey = "Cart";

        public IActionResult CartView()
        {
            var cart = GetCartFromSession();
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
                    ProductId = mediaID,
                    ProductName = media.Title,
                    Quantity = mediaQuantity,
                    ImageUrl = media.ImgUrl,
                    Price = media.Price,
                };
                var cart = GetCartFromSession();
                var existingItem = cart.FirstOrDefault(i => i.ProductId == mediaID);
                if (existingItem != null) existingItem.Quantity += mediaQuantity; else cart.Add(product);
                SaveCartToSession(cart);
                return Json(new { success = true, message = "Thêm giỏ hàng thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi khi thêm vào giỏ hàng: " + ex.Message });
            }
        }

        public IActionResult RemoveFromCart(int productId)
        {
            var cart = GetCartFromSession();
            var itemToRemove = cart.FirstOrDefault(i => i.ProductId == productId);
            if (itemToRemove != null) cart.Remove(itemToRemove);
            SaveCartToSession(cart);
            return RedirectToAction("CartView");
        }

        [HttpPost]
        public IActionResult UpdateQuantity(int productId, int quantity)
        {
            var cart = GetCartFromSession();
            var itemToUpdate = cart.FirstOrDefault(i => i.ProductId == productId);
            if (itemToUpdate != null) itemToUpdate.Quantity = quantity;
            SaveCartToSession(cart);
            decimal grandTotal = cart.Sum(item => item.Price * item.Quantity);
            return Json(new { grandTotal = grandTotal.ToString("N0", new CultureInfo("vi-VN")) });
        }

        private List<CartItem> GetCartFromSession()
        {
            var cartJson = HttpContext.Session.GetString(CartSessionKey);
            if (string.IsNullOrEmpty(cartJson)) return new List<CartItem>();
            return JsonSerializer.Deserialize<List<CartItem>>(cartJson);
        }

        private void SaveCartToSession(List<CartItem> cart)
        {
            var cartJson = JsonSerializer.Serialize(cart);
            HttpContext.Session.SetString(CartSessionKey, cartJson);
        }

        [HttpPost]
        public async Task<IActionResult> CheckStock(int productId, int quantity)
        {
            var product = await _mediaRepository.GetByIdAsync(productId);
            if (product == null) return Json(new { status = "Không tìm thấy sản phẩm" });
            return (product.Quantity >= quantity ? Json(new { status = "Đủ số lượng" }) : Json(new { status = $"Không đủ số lượng. Hiện có: {product.Quantity}" }));
        }
    }
}