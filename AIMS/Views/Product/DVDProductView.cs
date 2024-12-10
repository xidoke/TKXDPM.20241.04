using AIMS.Models.Entities;
using AIMS.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIMS.Views.Product
{
    public partial class DVDProductView : UserControl
    {
        private List<AIMS.Models.Entities.Media> dvdList;
        private MediaService mediaService = new MediaService();
        public DVDProductView()
        {
            InitializeComponent();
            LoadDVDs();
        }

        private void DVDProductView_Load(object sender, EventArgs e)
        {

        }
        private async void LoadDVDs()
        {
            string categoryToSearch = "DVD";
            string whereClause = "category = @category";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                 { "category", categoryToSearch }
            };
            dvdList = await mediaService.GetMediasAsync(whereClause, parameters);
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

        private void sortByPrice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(sortByPrice.Text))
            {
                isAscending = sortByPrice.SelectedIndex == 1;
                dvdList = sortByPrice.SelectedIndex == 1 ? dvdList.OrderByDescending(p => p.price).ToList() : dvdList.OrderBy(p => p.price).ToList();
            }
            LoadProductCards();
        }

        private async void buttonSearch_Click(object sender, EventArgs e)
        {
            string searchText = searchBox.Text.ToLower();
            LoadDVDs();
            if (string.IsNullOrWhiteSpace(searchText))
            {
                LoadDVDs();
            }
            else
            {
                MediaService mediaService = new MediaService();
                string whereClause = "LOWER(title) LIKE @title";
                Dictionary<string, object> parameters = new Dictionary<string, object>
                    {
                        { "title", $"%{searchText}%" }
                    };
                List<Media> filteredList = await mediaService.GetMediasAsync(whereClause, parameters);
                dvdList = filteredList;
            }
            LoadProductCards();
        }
    }
}
