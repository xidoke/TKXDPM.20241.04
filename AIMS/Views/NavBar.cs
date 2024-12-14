using AIMS.Controllers.Product;
using AIMS.Views.Cart;
using System;
using System.Windows.Forms;

namespace AIMS.Views
{
    public partial class NavBar : UserControl
    {
        private MediaController mediaController;
        public NavBar()
        {
            InitializeComponent();
            mediaController = new MediaController();
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
            CartView cartView = new CartView();
            MainForm.Instance.mainFormPanel.Controls.Clear();
            MainForm.Instance.mainFormPanel.Controls.Add(cartView);
            cartView.Visible = true;
            cartView.BringToFront();
        }

        private void homeButton_Click(object sender, EventArgs e)
        {
            HomeView homeViewUC = new HomeView();
            MainForm.Instance.mainFormPanel.Controls.Clear();
            MainForm.Instance.mainFormPanel.Controls.Add(homeViewUC);
            homeViewUC.Visible = true;
            homeViewUC.BringToFront();
        }
    }
}
