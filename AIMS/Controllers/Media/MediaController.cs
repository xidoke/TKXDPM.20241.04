using AIMS.Models.Entities;
using AIMS.Services;
using AIMS.Views.Cart;
using AIMS.Views.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace AIMS.Controllers.Product
{
    public class MediaController
    {
        private MediaService mediaService;
        public List<AIMS.Models.Entities.Media> dvd_list;
        public List<AIMS.Models.Entities.Media> cd_list;
        public List<AIMS.Models.Entities.Media> book_list;
        public MediaController() 
        { 
            mediaService = new MediaService();
        }
        public void ViewCategoryList(string category)
        {
            switch (category)
            {
                case "DVD":
                    DVDProductView dvdProductView = new DVDProductView();
                    MainForm.Instance.mainFormPanel.Controls.Clear();
                    MainForm.Instance.mainFormPanel.Controls.Add(dvdProductView);
                    dvdProductView.Show();
                    break;
                case "CD":
                    CDProductView cdProductView = new CDProductView();
                    MainForm.Instance.mainFormPanel.Controls.Clear();
                    MainForm.Instance.mainFormPanel.Controls.Add(cdProductView);
                    cdProductView.Show();
                    break;
                case "Book":
                    BookProductView bookProductView = new BookProductView();
                    MainForm.Instance.mainFormPanel.Controls.Clear();
                    MainForm.Instance.mainFormPanel.Controls.Add(bookProductView);
                    bookProductView.Show();
                    break;
            }
        }
        private void LoadProductCards(FlowLayoutPanel flp, List<AIMS.Models.Entities.Media> list, string cardType)
        {
            flp.Controls.Clear();
            foreach (var product in list)
            {
                if (cardType == "productMiniCard")
                {
                    ProductMiniCard productCard = new ProductMiniCard();
                    productCard.LoadProduct(product);
                    flp.Controls.Add(productCard);
                }
                if (cardType == "productCard")
                {
                    ProductCard productCard = new ProductCard();
                    productCard.LoadProduct(product);
                    flp.Controls.Add(productCard);
                }
            }
        }
        public async Task LoadMediaListbyCategory(FlowLayoutPanel flp, string category, string cardType)
        {
            string categoryToSearch = category;
            string whereClause = "category = @category";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                 { "category", categoryToSearch }
            };
            switch (category)
            {
                case "DVD":
                    dvd_list = await mediaService.GetMediasAsync(whereClause, parameters);
                    LoadProductCards(flp, dvd_list, cardType);
                    break;
                case "CD":
                    cd_list = await mediaService.GetMediasAsync(whereClause, parameters);
                    LoadProductCards(flp, cd_list, cardType);
                    break;
                case "Book":
                    book_list = await mediaService.GetMediasAsync(whereClause, parameters);
                    LoadProductCards(flp, book_list, cardType);
                    break;
            }
        }
        public async Task SearchProductByTitleAsync(string title, FlowLayoutPanel flp, string category, string cardType)
        {
            title = string.IsNullOrEmpty(title) ? "" : title.Trim().ToLower();
            List<Media> list;
            if (category.Contains("DVD"))
                list = dvd_list;
            else if (category.Contains("CD"))
                list = cd_list;
            else
                list = book_list;
            if (!string.IsNullOrEmpty(title))
            {
                var filteredList = list.Where(m => m.title.ToLower().Contains(title)).ToList();
                LoadProductCards(flp, filteredList, cardType);
            }
            else
                LoadProductCards(flp, list, cardType);
        }

        public async Task SortByPrice(FlowLayoutPanel flp, string category, int isAscending, string cardType)
        {
            List<Media> sortedList;
            switch (category)
            {
                case "DVD":
                    sortedList = isAscending == 1
                        ? new List<Media>(dvd_list.OrderByDescending(p => p.price))
                        : new List<Media>(dvd_list.OrderBy(p => p.price));
                    break;
                case "CD":
                    sortedList = isAscending == 1
                        ? new List<Media>(cd_list.OrderByDescending(p => p.price))
                        : new List<Media>(cd_list.OrderBy(p => p.price));
                    break;
                case "Book":
                    sortedList = isAscending == 1
                        ? new List<Media>(book_list.OrderByDescending(p => p.price))
                        : new List<Media>(book_list.OrderBy(p => p.price));
                    break;
                default:
                    return; 
            }
            LoadProductCards(flp, sortedList, cardType);
        }
    }
}
