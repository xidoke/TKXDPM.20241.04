using AIMS.Controllers.Address;
using AIMS.Controllers.Cart;
using AIMS.Controllers.Order;
using AIMS.Controllers.Product;
using AIMS.Models.Entities;
using AIMS.Views.Order;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIMS.Views.Cart
{
    public partial class CartView : UserControl
    {
        private readonly CartController cartController;
        private readonly PlaceOrderController placeOrderController;
        private readonly AddressController addressController;
        private readonly MediaController mediaController;
        private readonly NavBar navBar; 
        public static CartView Instance;

        public CartView(CartController cartController, PlaceOrderController placeOrderController, 
            AddressController addressController, NavBar navBar, MediaController mediaController)
        {
            InitializeComponent();
            this.cartController = cartController;
            this.placeOrderController = placeOrderController;
            this.addressController = addressController;
            this.mediaController = mediaController;
            Instance = this;
            this.navBar = navBar;
        }

        private async void CartView_Load(object sender, EventArgs e)
        {
            flpNavBar.Controls.Add(navBar);
            navBar.Show();
            cartController.LoadCart();
            if (this.cartItemBindingSource.Count > 0)
            {
                await cartController.UpdateCartItemsAsync();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (this.cartItemBindingSource.Count > 0)
                {
                    this.placeOrderButton.Visible = true;
                    label1.Text = $"Tổng tiền sản phẩm: {cartController.getSumTotalMoney()} đ";
                    label1.Visible = true;
                }
                else
                {
                    label1.Visible = false;
                    this.placeOrderButton.Visible = false;
                }
                if (cartController.GetCartItemsSelected().Count > 1)
                {
                    label2.Text = $"Bạn đang chọn: {cartController.GetCartItemsSelected().Count} sản phẩm";
                }
                else
                {
                    label2.Text = $"Bạn đang chọn: {cartController.GetCartItemsSelected()[0].media_title}";
                }
            }
            catch
            {

            }
        }
        // Xóa hàng ra khỏi giỏ hàng
        private void button1_Click(object sender, EventArgs e)
        {
            var CartItems = cartController.GetCartItemsSelected();
            foreach (CartItem item in CartItems)
            {
                if (item == null)
                    return;
                this.cartItemBindingSource.Remove(item);
            }
            this.Refresh();
            cartController.SaveCart();
        }
        // Điều chỉnh số lượng vật phẩm trong giỏ hàng
        private void button2_Click(object sender, EventArgs e)
        {
            CartItem cartItem = cartController.getCartItem(-1);
            if (cartItem != null)
            {
                cartItem.quantity = int.Parse(cartItemQuantityNumeric.Value.ToString());
            }
            this.Refresh();
            cartController.SaveCart();
        }

        private void dataGridViewCartItems_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridViewCartItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewCartItems.CurrentCell != null)
            {
                cartItemQuantityNumeric.Value = int.Parse(dataGridViewCartItems.Rows[dataGridViewCartItems.CurrentCell.RowIndex].Cells[3].Value.ToString());
                this.Refresh();
            }
        }

        private async void updateCartButton_Click(object sender, EventArgs e)
        {
            if (this.cartItemBindingSource.Count > 0)
            {
                await cartController.UpdateCartItemsAsync();
                await Task.Delay(1000);
                MessageBox.Show("Giỏ hàng đã được cập nhật!");
            }
        }

        private async void placeOrderButton_Click(object sender, EventArgs e)
        {
            if (cartController.GetCartItemsSelectedToOrder().Count > 0)
            {
                int count = await cartController.countItemNotEnough();
                if (count > 0)
                {
                    MessageBox.Show("Vui lòng kiểm tra lại số lượng");
                    return;
                }
            }
            cartController.SaveCart();
            PlaceOrderView placeOrderView = new PlaceOrderView(cartController.GetCartItemsSelectedToOrder(), placeOrderController, 
                addressController, navBar, mediaController);
            MainForm.Instance.mainFormPanel.Controls.Clear();
            MainForm.Instance.mainFormPanel.Controls.Add(placeOrderView);
            placeOrderView.Show();
        }
    }
}