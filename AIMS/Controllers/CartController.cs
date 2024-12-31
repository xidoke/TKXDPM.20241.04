using AIMS.Data.Entities;
using AIMS.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text.Json;

namespace AIMS.Controllers
{
    public class CartController : Controller
    {
        private readonly IMediaRepository mediaRepository;
        public CartController(IMediaRepository mediaRepository)
        {
            this.mediaRepository = mediaRepository;
        }
        private const string OrderMediaListSessionKey = "OrderMediaList";
        [HttpPost]
        public IActionResult ProcessOrder(List<CartItem> SelectedItems)
        {
            List<CartItem> selectedProducts = SelectedItems.Where(item => item.isSelected).ToList();

            List<OrderMedia> Temp = selectedProducts.Select(item => new OrderMedia
            {
                MediaId = item.ProductId,
                Name = item.ProductName,
                Quantity = item.Quantity,
                Price = (int)item.Price,
            }).ToList();

            HttpContext.Session.SetString(OrderMediaListSessionKey, JsonSerializer.Serialize(Temp));
            return RedirectToAction("PlaceOrderView", "Order");
        }
        private const string CartSessionKey = "Cart";
        private List<CartItem> cartItems = new List<CartItem>
        {
           
        };
        // Hiển thị giỏ hàng
        public IActionResult CartView()
        {
            var cart = GetCartFromSession();
            ViewBag.GrandTotal = cart.Sum(item => item.Price * item.Quantity); // Tính tổng tiền và truyền qua ViewBag
            return View(cart); // Truyền danh sách CartItem trực tiếp sang View
        }

        // Thêm sản phẩm vào giỏ hàng (cần chỉnh sửa lại logic)
        [HttpPost]
        public async Task<IActionResult> AddToCart(int mediaID,int mediaQuantity)
        {
            var media = await mediaRepository.GetByIdAsync(mediaID);
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
            if (existingItem != null)
            {
                existingItem.Quantity += mediaQuantity;
            }
            else
            {
                product.Quantity = mediaQuantity;
                cart.Add(product);
            }

            SaveCartToSession(cart);

            return RedirectToAction("CartView", "Cart");
        }

        // Xóa sản phẩm khỏi giỏ hàng
        public IActionResult RemoveFromCart(int productId)
        {
            var cart = GetCartFromSession();
            var itemToRemove = cart.FirstOrDefault(i => i.ProductId == productId);
            if (itemToRemove != null)
            {
                cart.Remove(itemToRemove);
            }
            SaveCartToSession(cart);
            return RedirectToAction("CartView", "Cart");
        }

        [HttpPost]
        public IActionResult UpdateQuantity(int productId, int quantity)
        {
            var cart = GetCartFromSession();
            var itemToUpdate = cart.FirstOrDefault(i => i.ProductId == productId);
            if (itemToUpdate != null)
            {
                itemToUpdate.Quantity = quantity;
            }
            SaveCartToSession(cart);
            decimal grandTotal = cart.Sum(item => item.Price * item.Quantity);
            return Json(new { grandTotal = grandTotal.ToString("N0", new CultureInfo("vi-VN")) });
        }

        // Lấy giỏ hàng từ Session
        private List<CartItem> GetCartFromSession()
        {
            var cartJson = HttpContext.Session.GetString(CartSessionKey);
            if (string.IsNullOrEmpty(cartJson))
            {
                return new List<CartItem>(); // Trả về danh sách rỗng thay vì CartViewModel mới
            }
            return JsonSerializer.Deserialize<List<CartItem>>(cartJson);
        }

        // Lưu giỏ hàng vào Session
        private void SaveCartToSession(List<CartItem> cart)
        {
            var cartJson = JsonSerializer.Serialize(cart);
            HttpContext.Session.SetString(CartSessionKey, cartJson);
        }
        [HttpPost]
        public IActionResult UpdateCart(List<CartItem> SelectedItems)
        {
            // ... your existing code to update quantities in the session ...

            // Recalculate grand total
            var cart = GetCartFromSession();
            decimal grandTotal = cart.Sum(item => item.Price * item.Quantity);

            // Return the updated grand total as part of the response
            return Json(new { grandTotal = grandTotal.ToString("C") });
        }

        [HttpPost]
        public async Task<IActionResult> CheckStock(int productId, int quantity)
        {
            var product = await mediaRepository.GetByIdAsync(productId);

            if (product == null)
            {
                return Json(new { status = "Không tìm thấy sản phẩm" });
            }

            if (product.Quantity >= quantity)
            {
                return Json(new { status = "Đủ số lượng" });
            }
            else
            {
                return Json(new { status = "Không đủ số lượng" });
            }
        }
    }
}
