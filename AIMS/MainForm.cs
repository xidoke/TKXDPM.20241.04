using AIMS.Models.Entities;
using AIMS.Services;
using AIMS.Views;
using AIMS.Views.Cart;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AIMS
{
    public partial class MainForm : Form
    {
        private readonly CartView cartView;
        private readonly HomeView homeView;
        public static MainForm Instance;
        public MainForm(CartView cartView, HomeView homeView)
        {
            InitializeComponent();
            Instance = this;
            this.cartView = cartView;
            this.homeView = homeView;
            MainForm.Instance.mainFormPanel.Controls.Clear();
            MainForm.Instance.mainFormPanel.Controls.Add(cartView);
            cartView.Visible = true;
            cartView.BringToFront();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            mainFormPanel.Controls.Add(homeView);
            homeView.Visible = true;
            homeView.BringToFront();
        }
    }
}
