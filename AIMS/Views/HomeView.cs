using AIMS.Controllers.Product;
using AIMS.Views.Product;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AIMS.Views
{
    public partial class HomeView : UserControl
    {
        private readonly MediaController mediaController;
        private readonly NavBar navBar;
        public static HomeView Instance;
        public HomeView(MediaController mediaController, NavBar navBar)
        {
            InitializeComponent();
            this.mediaController = mediaController;
            Instance = this;
            this.navBar = navBar;
        }

        private async void HomeView_Load(object sender, EventArgs e)
        {
            flpNavBar.Controls.Add(navBar);
            navBar.Show();
            await mediaController.LoadMediaListbyCategory(AIMS.Views.HomeView.Instance.flpDVD, "DVD", "productMiniCard");
            await mediaController.LoadMediaListbyCategory(AIMS.Views.HomeView.Instance.flpCD, "CD", "productMiniCard");
            await mediaController.LoadMediaListbyCategory(AIMS.Views.HomeView.Instance.flpBook, "Book", "productMiniCard");
        }

        //private void LoadProductCards(FlowLayoutPanel flp, List<AIMS.Models.Entities.Media> products)
        //{
        //    if (flp == null)
        //    {
        //        Console.WriteLine("FlowLayoutPanel is null.");
        //        return;
        //    }
        //    flp.Controls.Clear();
        //    if (products == null || products.Count == 0)
        //    {
        //        Console.WriteLine("No products to display.");
        //        return;
        //    }
        //    foreach (var product in products)
        //    {
        //        productMiniCard.LoadProduct(product);
        //        flp.Controls.Add(productMiniCard);
        //    }
        //}

        private void lblViewMoreDVD_Click(object sender, EventArgs e)
        {
            mediaController.ViewCategoryList("DVD");
        }

        private void lblViewMoreCD_Click(object sender, EventArgs e)
        {
            mediaController.ViewCategoryList("CD");
        }

        private void lblViewMoreBOOK_Click(object sender, EventArgs e)
        {
            mediaController.ViewCategoryList("Book");
        }
    }
}
