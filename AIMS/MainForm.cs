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
        public static MainForm Instance;
        public MainForm()
        {
            InitializeComponent();
            Instance = this;
            CartView cartView = new CartView();
            MainForm.Instance.mainFormPanel.Controls.Clear();
            MainForm.Instance.mainFormPanel.Controls.Add(cartView);
            cartView.Visible = true;
            cartView.BringToFront();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            HomeView homeViewUC = new HomeView();
            mainFormPanel.Controls.Add(homeViewUC);
            homeViewUC.Visible = true;
            homeViewUC.BringToFront();
        }
    }
}
