using AIMS.Models.Entities;
using AIMS.Services;
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
    public partial class CDProductView : UserControl
    {
        private List<AIMS.Models.Entities.Media> cdList;
        private MediaService mediaService = new MediaService();
        public CDProductView()
        {
            InitializeComponent();
            LoadCDs();
        }

        private async void buttonSearch_Click(object sender, EventArgs e)
        {
            string searchText = searchBox.Text.ToLower();
            LoadCDs();
            if (string.IsNullOrWhiteSpace(searchText))
            {
                LoadCDs();
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
                cdList = filteredList;
            }
            LoadProductCards();
        }

        private void CDProductView_Load(object sender, EventArgs e)
        {

        }
        private async void LoadCDs()
        {
            string categoryToSearch = "CD";
            string whereClause = "category = @category";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                 { "category", categoryToSearch }
            };
            cdList = await mediaService.GetMediasAsync(whereClause, parameters);
            LoadProductCards();
        }

        private void LoadProductCards()
        {
            flpCD.Controls.Clear();

            foreach (var product in cdList)
            {
                ProductCard productCard = new ProductCard();
                productCard.LoadProduct(product);
                flpCD.Controls.Add(productCard);
            }
        }
        private bool isAscending = true;

        private void sortByPrice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(sortByPrice.Text))
            {
                isAscending = sortByPrice.SelectedIndex == 1;
                cdList = sortByPrice.SelectedIndex == 1 ? cdList.OrderByDescending(p => p.price).ToList() : cdList.OrderBy(p => p.price).ToList();
            }
            LoadProductCards();
        }
    }
}
