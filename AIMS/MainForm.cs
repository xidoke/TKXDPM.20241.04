using AIMS.Views.Cart;
using AIMS.Views.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIMS
{
    public partial class MainForm : Form
    {
        public static MainForm Instance;
        public MainForm()
        {
            InitializeComponent();
            Instance = this;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CartView cartView = new CartView();
            ProductDetailsView productDetailsView = new ProductDetailsView(12);
            mainFormPanel.Controls.Add(cartView);
            mainFormPanel.Controls.Add(productDetailsView);
            cartView.Visible = false;
            productDetailsView.Visible = true;
            productDetailsView.BringToFront();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CartView cartView = new CartView();
            ProductDetailsView productDetailsView = new ProductDetailsView(12);
            mainFormPanel.Controls.Add(cartView);
            mainFormPanel.Controls.Add(productDetailsView);
            cartView.Visible = true;
            productDetailsView.Visible = false;
            cartView.BringToFront();
        }
    }
}
