using AIMS.Models.Entities;
using AIMS.Services;
using AIMS.Views;
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
            SearchMediaResultView searchMediaResultsPage = new SearchMediaResultView("", category);
            MainForm.Instance.mainFormPanel.Controls.Clear();
            MainForm.Instance.mainFormPanel.Controls.Add(searchMediaResultsPage);
            searchMediaResultsPage.Show();
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
        public async Task FilterAndSortProductsAdvancedAsync(FlowLayoutPanel flp, string cardType, string title = "", decimal? minPrice = null, decimal? maxPrice = null, string categoryFilter = null, bool sortByPriceDesc = false)
        {
            List<Media> allMedia = await GetAllMedia();
            IEnumerable<Media> filteredList = allMedia;
            if (!string.IsNullOrEmpty(title))
                filteredList = filteredList.Where(m => m.title.ToLower().Contains(title.ToLower().Trim()));
            if (minPrice.HasValue)
                filteredList = filteredList.Where(m => m.price >= minPrice.Value);
            if (maxPrice.HasValue)
                filteredList = filteredList.Where(m => m.price <= maxPrice.Value);
            if (!string.IsNullOrEmpty(categoryFilter))
                filteredList = filteredList.Where(m => m.category.Equals(categoryFilter, StringComparison.OrdinalIgnoreCase));      
            List<Media> sortedList;
            if (sortByPriceDesc) 
                sortedList = filteredList.OrderByDescending(m => m.price).ToList();
            else
                sortedList = filteredList.OrderBy(m => m.price).ToList();
            LoadProductCards(flp, sortedList, cardType);
        }

        private async Task<List<Media>> GetAllMedia()
        {
            List<Media> allMedia = new List<Media>();

            if (dvd_list == null || dvd_list.Count == 0)
                dvd_list = await mediaService.GetMediasAsync("category = @category", new Dictionary<string, object> { { "category", "DVD" } });
            if (cd_list == null || cd_list.Count == 0)
                cd_list = await mediaService.GetMediasAsync("category = @category", new Dictionary<string, object> { { "category", "CD" } });
            if (book_list == null || book_list.Count == 0)
                book_list = await mediaService.GetMediasAsync("category = @category", new Dictionary<string, object> { { "category", "Book" } });
            allMedia.AddRange(dvd_list);
            allMedia.AddRange(cd_list);
            allMedia.AddRange(book_list);
            return allMedia;
        }
        public string getCategory(int index)
        {
            switch (index)
            {
                case 0:
                    return "DVD";
                case 1:
                    return "CD";
                case 2:
                    return "Book";
                default:
                    return null;
            }
        }
    }
}
