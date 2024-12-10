using AIMS.Views.Product;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AIMS.Views
{
    public partial class HomeView : UserControl
    {
        private List<AIMS.Models.Entities.Media> dvdList;
        private List<AIMS.Models.Entities.Media> cdList;
        private List<AIMS.Models.Entities.Media> bookList;
        public HomeView()
        {
            InitializeComponent();
            InitializeProductLists();
            LoadProducts();
        }

        private void HomeView_Load(object sender, EventArgs e)
        {

        }
        private void InitializeProductLists()
        {
            // Ví dụ tạo dữ liệu mẫu:
            dvdList = new List<AIMS.Models.Entities.Media>
            {
                new AIMS.Models.Entities.Media { title = "DVD 1", price = 10, type = "DVD" },
                new AIMS.Models.Entities.Media { title = "DVD 2", price = 12, type = "DVD" },
                new AIMS.Models.Entities.Media { title = "DVD 3", price = 15, type = "DVD" },
                new AIMS.Models.Entities.Media { title = "DVD 4", price = 9, type = "DVD" },
                new AIMS.Models.Entities.Media { title = "DVD 5", price = 11, type = "DVD" },
                new AIMS.Models.Entities.Media { title = "DVD 6", price = 15, type = "DVD" },
                new AIMS.Models.Entities.Media { title = "DVD 7", price = 9, type = "DVD" },
                new AIMS.Models.Entities.Media { title = "DVD 8", price = 11, type = "DVD" }
            };

            cdList = new List<AIMS.Models.Entities.Media>
            {
                new AIMS.Models.Entities.Media { title = "CD 1", price = 8, type = "CD" },
                new AIMS.Models.Entities.Media { title = "CD 2", price = 7, type = "CD" },
                new AIMS.Models.Entities.Media { title = "CD 3", price = 10, type = "CD" },
                new AIMS.Models.Entities.Media { title = "CD 4", price = 6, type ="CD"},
                new AIMS.Models.Entities.Media { title = "CD 5", price = 9, type = "CD" },
                new AIMS.Models.Entities.Media { title = "CD 6", price = 10, type = "CD" },
                new AIMS.Models.Entities.Media { title = "CD 7", price = 6, type ="CD"},
                new AIMS.Models.Entities.Media { title = "CD 8", price = 9, type = "CD" }
            };

            bookList = new List<AIMS.Models.Entities.Media>
            {
                new AIMS.Models.Entities.Media { title = "Book 1", price = 20, type = "BOOK" },
                new AIMS.Models.Entities.Media { title = "Book 2", price = 25, type ="BOOK" },
                new AIMS.Models.Entities.Media { title = "Book 3", price = 18, type = "BOOK" },
                new AIMS.Models.Entities.Media { title = "Book 4", price = 22, type = "BOOK" },
                new AIMS.Models.Entities.Media { title = "Book 5", price = 27, type ="BOOK" },
                 new AIMS.Models.Entities.Media { title = "Book 6", price = 18, type = "BOOK" },
                new AIMS.Models.Entities.Media { title = "Book 7", price = 22, type = "BOOK" },
                new AIMS.Models.Entities.Media { title = "Book 8", price = 27, type ="BOOK" }
            };
        }

        private void LoadProducts()
        {
            LoadProductCards(flpDVD, dvdList);
            LoadProductCards(flpCD, cdList);
            LoadProductCards(flpBook, bookList);
        }

        private void LoadProductCards(FlowLayoutPanel flp, List<AIMS.Models.Entities.Media> products)
        {
            foreach (var product in products)
            {
                ProductMiniCard productCard = new ProductMiniCard();
                productCard.LoadProduct(product);
                flp.Controls.Add(productCard);
            }
        }

        private void lblViewMoreDVD_Click(object sender, EventArgs e)
        {
            DVDProductView dvdProductView = new DVDProductView();
            MainForm.Instance.mainFormPanel.Controls.Clear();
            MainForm.Instance.mainFormPanel.Controls.Add(dvdProductView);
            dvdProductView.Show();
        }

        private void lblViewMoreCD_Click(object sender, EventArgs e)
        {

        }

        private void lblViewMoreBOOK_Click(object sender, EventArgs e)
        {

        }
    }
}
