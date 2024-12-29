using AIMS.Controllers.Product;
using AIMS.Views.Cart;
using System;
using System.Windows.Forms;

namespace AIMS.Views
{
    public partial class NavBar : UserControl
    {
        private readonly MediaController mediaController;
        private readonly CartView cartView;
        private readonly HomeView homeView;
        public NavBar(MediaController mediaController, CartView cartView, HomeView homeView)
        {
            InitializeComponent();
            this.mediaController = mediaController;
            this.cartView = cartView;
            this.homeView = homeView;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Nếu ấn Enter ở trong SearchBox sẽ tìm kiếm theo kết quả
            if (e.KeyChar == (char)Keys.Enter && !string.IsNullOrEmpty(textBox1.Text))
            {
                mediaController.ViewSearchResult(this.textBox1.Text);
            }
        }

        private void btnViewCart_Click(object sender, EventArgs e)
        {
            MainForm.Instance.mainFormPanel.Controls.Clear();
            MainForm.Instance.mainFormPanel.Controls.Add(cartView);
            cartView.Visible = true;
            cartView.BringToFront();
        }

        private void homeButton_Click(object sender, EventArgs e)
        {
            MainForm.Instance.mainFormPanel.Controls.Clear();
            MainForm.Instance.mainFormPanel.Controls.Add(homeView);
            homeView.Visible = true;
            homeView.BringToFront();
        }
    }
}
