using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIMS.Views.Product
{
    public partial class DVDProductView : UserControl
    {
        private List<AIMS.Models.Entities.Media> dvdList;
        public DVDProductView()
        {
            InitializeComponent();
            LoadDVDs();
        }

        private void DVDProductView_Load(object sender, EventArgs e)
        {

        }
        private void LoadDVDs()
        {
            dvdList = new List<AIMS.Models.Entities.Media>
            {
                new AIMS.Models.Entities.Media { id = 1, title = "DVD 1", price = 10, type = "DVD" },
                new AIMS.Models.Entities.Media { id = 2, title = "DVD 2", price = 12, type = "DVD"},
                new AIMS.Models.Entities.Media { id = 3, title = "DVD 3", price = 15, type = "DVD"},
                new AIMS.Models.Entities.Media { id = 4, title = "DVD 4", price = 9, type = "DVD" },
                new AIMS.Models.Entities.Media { id = 5, title = "DVD 5", price = 11, type = "DVD" },
                new AIMS.Models.Entities.Media { id = 6, title = "Another DVD", price = 13, type = "DVD" },
                new AIMS.Models.Entities.Media { id = 7, title = "DVD Movie", price = 17, type = "DVD" },
                new AIMS.Models.Entities.Media { id = 8, title = "DVD 8", price = 14, type = "DVD" },
                new AIMS.Models.Entities.Media { id = 9, title = "DVD 9", price = 16, type = "DVD" },
            };
            dvdList = dvdList.Where(p => p.type == "DVD").ToList();

            LoadProductCards();
        }

        private void LoadProductCards()
        {
            flpDVD_Product.Controls.Clear();

            foreach (var product in dvdList)
            {
                ProductCard productCard = new ProductCard();
                productCard.LoadProduct(product);
                flpDVD_Product.Controls.Add(productCard);
            }
        }
        private bool isAscending = true;
        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(searchBox.Text))
            {
                LoadDVDs();
                return;
            }
            string searchText = searchBox.Text.ToLower();
            List<AIMS.Models.Entities.Media> filteredList;

            if (string.IsNullOrWhiteSpace(searchText))
            {
                filteredList = dvdList;
            }
            else
            {
                filteredList = dvdList.Where(p =>
                    p.title.ToLower().Contains(searchText)
                ).ToList();
            }

            dvdList = filteredList;
            LoadProductCards();     
        }

        private void sortByPrice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(sortByPrice.Text))
            {
                isAscending = sortByPrice.SelectedIndex == 1;
                dvdList = sortByPrice.SelectedIndex == 1 ? dvdList.OrderByDescending(p => p.price).ToList() : dvdList.OrderBy(p => p.price).ToList();
            }
            LoadProductCards();
        }
    }
}
