using AIMS.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AIMS.Controllers
{
    public class CartController : Controller
    {
        private const string OrderMediaListSessionKey = "OrderMediaList";
        [HttpPost]
        public IActionResult ProcessOrder(List<CartItem> SelectedItems)
        {
            List<CartItem> selectedProducts = SelectedItems.Where(item => item.isSelected).ToList();

            List<OrderMedia> Temp = selectedProducts.Select(item => new OrderMedia
            {
                MediaId = item.ProductId,
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
        public IActionResult AddToCart(int mediaID, string mediaTitle, int mediaQuantity, int mediaPrice, string mediaImgUrl)
        {
            var product = new CartItem
            {
                ProductId = mediaID,
                ProductName = mediaTitle,
                Quantity = mediaQuantity,
                ImageUrl = mediaImgUrl,
                Price = mediaQuantity
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

        // Cập nhật số lượng sản phẩm
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
            return RedirectToAction("Index");
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
    }
}
