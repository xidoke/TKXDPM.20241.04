using AIMS.Controllers.Product;
using AIMS.Services;
using AIMS.Views.Product;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIMS.Views
{
    public partial class HomeView : UserControl
    {
        private MediaController mediaController;
        public HomeView()
        {
            InitializeComponent();
            mediaController = new MediaController();
        }

        private async void HomeView_Load(object sender, EventArgs e)
        {
            await mediaController.LoadMediaListbyCategory(flpDVD, "DVD", "productMiniCard");
            await mediaController.LoadMediaListbyCategory(flpCD, "CD", "productMiniCard");
            await mediaController.LoadMediaListbyCategory(flpBook, "Book", "productMiniCard");
        }

        private void LoadProductCards(FlowLayoutPanel flp, List<AIMS.Models.Entities.Media> products)
        {
            if (flp == null)
            {
                Console.WriteLine("FlowLayoutPanel is null.");
                return;
            }
            flp.Controls.Clear();
            if (products == null || products.Count == 0)
            {
                Console.WriteLine("No products to display.");
                return;
            }
            foreach (var product in products)
            {
                ProductMiniCard productCard = new ProductMiniCard();
                productCard.LoadProduct(product);
                flp.Controls.Add(productCard);
            }
        }

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
