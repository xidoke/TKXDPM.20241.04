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
            new CartItem
            {
                ProductId = 3,
                ProductName = "Relaxed Fit T-shirt",
                Price = 12.99m,
                Quantity = 5,
                ImageUrl = "/Content/Images/tshirt.jpg", // Đường dẫn ảnh
            },
           new CartItem
            {
               ProductId = 4,
                ProductName = "Relaxed Fit T-shirt",
                Price = 12.99m,
                Quantity = 5,
                ImageUrl = "/Content/Images/tshirt.jpg", // Đường dẫn ảnh
            },
           new CartItem
            {   ProductId = 5,
                ProductName = "Relaxed Fit T-shirt",
                Price = 12.99m,
                Quantity = 5,
                ImageUrl = "/Content/Images/tshirt.jpg", // Đường dẫn ảnh
            },
        };
        // Hiển thị giỏ hàng
        public IActionResult CartView()
        {
            var cart = cartItems;
            ViewBag.GrandTotal = cart.Sum(item => item.Price * item.Quantity); // Tính tổng tiền và truyền qua ViewBag
            return View(cart); // Truyền danh sách CartItem trực tiếp sang View
        }

        // Thêm sản phẩm vào giỏ hàng (cần chỉnh sửa lại logic)
        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity)
        {
            // TODO: Lấy thông tin sản phẩm từ database dựa vào productId
            // Đây là ví dụ, bạn cần thay thế bằng logic thực tế của bạn
            var product = new CartItem
            {
                ProductId = productId,
                ProductName = "Product " + productId,
                ImageUrl = "https://mir-s3-cdn-cf.behance.net/project_modules/max_1200/ad8984166761989.641d86af1a88c.png",
                Price = 10000
            };

            var cart = GetCartFromSession();

            var existingItem = cart.FirstOrDefault(i => i.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                product.Quantity = quantity;
                cart.Add(product);
            }

            SaveCartToSession(cart);

            return RedirectToAction("Index");
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
            return RedirectToAction("Index");
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
