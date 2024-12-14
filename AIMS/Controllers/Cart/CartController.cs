using AIMS.Models.Entities;
using AIMS.Services;
using AIMS.Views.Cart;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIMS.Controllers.Cart
{
    public class CartController
    {
        private MediaService mediaService;
        public CartController()
        {
            mediaService = new MediaService();
        }
        public List<CartItem> cartItems;
        private string cartPath = "cart.json";
        // Lưu giỏ hàng
        public void SaveCart()
        {
            File.WriteAllText(cartPath, JsonConvert.SerializeObject(CartView.Instance.cartItemBindingSource));
        }
        // Đường dẫn tới chỗ lưu giỏ hàng
        public string ServerPath = "Data/QLTK/serverconfig.ini";
        public string getSumTotalMoney()
        {
            int count = 0;
            foreach (var item in GetCartItems())
            {
                if (item != null)
                    count += item.total_money;
            }
            return string.Format("{0:N0}", count);
        }
        // Load danh sách mặt hàng trong giỏ từ file
        public void LoadCart()
        {
            CartView.Instance.cartItemBindingSource.Clear();
            if (File.Exists(cartPath))
            {
                try
                {
                    string data = File.ReadAllText(cartPath);
                    var items = JsonConvert.DeserializeObject<CartItem[]>(data);
                    foreach (var item in items)
                    {
                        CartView.Instance.cartItemBindingSource.Add(item);
                    }
                }
                catch
                {

                }
            }
        }
        // Thêm mặt hàng vào giỏ hàng
        public async Task AddMediaToCart(int mediaID, int quantity)
        {
            CartView.Instance.cartItemBindingSource.Add(new CartItem()
            {
                media_id = mediaID,
                media_title = "",
                isSelected = false,
                total_money = 0,
                isPossibleToPlaceOrder = "",
                quantity = quantity
            });
            SaveCart();
            AIMS.Models.Entities.Media currentMedia = await mediaService.GetMediaByIdAsync(mediaID);
            MessageBox.Show($"Thêm thành công {currentMedia.title} [{currentMedia.id}] vào giỏ hàng!");
        }
        // Lấy ra danh sách những mặt hàng đã bôi đen
        public List<CartItem> GetCartItemsSelected()
        {
            var rows = CartView.Instance.dataGridViewCartItems.SelectedRows.Cast<DataGridViewRow>().ToList();
            List<CartItem> list = new List<CartItem>();
            foreach (var row in rows)
            {
                list.Add(row.DataBoundItem as CartItem);
            }
            return list;
        }
        // Lấy ra danh sách những mặt hàng trong giỏ hàng đã tích chọn để đặt hàng (isSelected : chọn để đặt hàng)
        public List<CartItem> GetCartItemsSelectedToOrder()
        {
            return GetCartItems().FindAll(x => x != null && x.isSelected);
        }
        // Lấy ra tất cả mặt hàng trong giỏ hàng
        public List<CartItem> GetCartItems()
        {
            return CartView.Instance.cartItemBindingSource.Cast<CartItem>().ToList();
        }
        // Lấy ra mặt hàng đang chọn hiện tại
        public CartItem getCartItem(int index)
        {
            if (index == -1)
            {
                return CartView.Instance.cartItemBindingSource.Current as CartItem;
            }
            return CartView.Instance.cartItemBindingSource[index] as CartItem;
        }
        // Cập nhật (tên, số lượng, giá, trạng thái của mặt hàng)
        public async Task UpdateCartItemsAsync()
        {
            foreach (CartItem cartItem in CartView.Instance.cartItemBindingSource)
            {
                Media media = await mediaService.GetMediaByIdAsync(cartItem.media_id);
                if (media != null)
                {
                    cartItem.total_money = media.price;
                    cartItem.media_title = media.title;
                    cartItem.isPossibleToPlaceOrder = cartItem.quantity <= media.quantity ? "Sẵn sàng" : "Hết hàng";
                }
                CartView.Instance.dataGridViewCartItems.Refresh();
            }
        }
    }
}
