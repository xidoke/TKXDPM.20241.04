using AIMS.Controllers.Cart;
using AIMS.Controllers.Order;
using AIMS.Controllers.Product;
using AIMS.Models.Entities;
using AIMS.Views.Order;
using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace AIMS.Views.Product
{
    public partial class BookDetailsView : UserControl
    {
        private MediaController mediaController;
        private CartController cartController;
        private PlaceOrderController placeOrderController;
        public static BookDetailsView Instance;
        public BookDetailsView(int mediaID)
        {
            InitializeComponent();
            Instance = this;
            mediaController = new MediaController();
            cartController = new CartController();
            placeOrderController = new PlaceOrderController();
            mediaController.currentID = mediaID;
        }

        private async void BookDetailsView_Load(object sender, EventArgs e)
        {
            NavBar navBar = new NavBar();
            flpNavBar.Controls.Add(navBar);
            navBar.Show();
            await mediaController.LoadBookDetails();
        }

        private async void addToCartButton_Click(object sender, EventArgs e)
        {
            await cartController.AddMediaToCart(mediaController.currentID, int.Parse(quantityNumericUpDown.Value.ToString()));
        }

        private async void placeOrderButton_Click(object sender, EventArgs e)
        {
            if (int.Parse(quantityNumericUpDown.Value.ToString()) == 0)
            {
                MessageBox.Show("Vui lòng chọn số lượng lớn hơn 0!");
                return;
            }
            List<CartItem> currentCart = new List<CartItem>()
            {
                new CartItem() {
                    isSelected = true, 
                    isPossibleToPlaceOrder = "", 
                    media_id = mediaController.currentID, 
                    quantity = int.Parse(quantityNumericUpDown.Value.ToString()),
                    media_title = "",
                    total_money = 0,
                },

            };
            AIMS.Models.Entities.Media media = await mediaController.GetMediaAsync(mediaController.currentID);
            if (!media.isEnough(int.Parse(quantityNumericUpDown.Value.ToString())))
            {
                MessageBox.Show("Mặt hàng này đã hết hàng!");
                return;
            }
            PlaceOrderView placeOrderView = new PlaceOrderView(currentCart);
            MainForm.Instance.mainFormPanel.Controls.Clear();
            MainForm.Instance.mainFormPanel.Controls.Add(placeOrderView);
            placeOrderView.Show();
        }

        private void flpNavBar_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
