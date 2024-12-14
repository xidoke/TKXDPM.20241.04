using AIMS.Controllers.Cart;
using AIMS.Controllers.Product;
using AIMS.Models.Entities;
using AIMS.Views.Order;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AIMS.Views.Product
{
    public partial class DVDDetailsView : UserControl
    {
        private MediaController mediaController;
        private CartController cartController;
        public static DVDDetailsView Instance;
        public DVDDetailsView(int mediaID)
        {
            InitializeComponent();
            Instance = this;
            mediaController = new MediaController();
            cartController = new CartController();
            mediaController.currentID = mediaID;
        }

        private async void ProductDetailsView_Load(object sender, EventArgs e)
        {
            NavBar navBar = new NavBar();
            flpNavBar.Controls.Add(navBar);
            navBar.Show();
            await mediaController.LoadDVDDetails();
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
    }
}
