using AIMS.Models.Entities;
using AIMS.Services;
using AIMS.Views;
using AIMS.Views.Product;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIMS.Controllers.Product
{
    public class MediaController
    {
        private readonly IMediaService mediaService;
        private readonly IBookService bookService;
        private readonly IDVDService dvdService;
        private readonly ICDService cdService;
        private readonly SearchMediaResultView searchMediaResultView;
        private readonly ProductMiniCard productMiniCard;
        private readonly ProductCard productCard;
        public List<Media> dvd_list;
        public List<Media> cd_list;
        public List<Media> book_list;
        #region Liên quan tới hiển thị chi tiết sản phẩm
        public Book currentBook;
        public int currentID = -1;
        public DVD currentDVD;
        public CD currentCD;

        public MediaController(IMediaService mediaService, IBookService bookService, ICDService cdService, 
            IDVDService dvdService, SearchMediaResultView searchMediaResultView, ProductMiniCard productMiniCard, ProductCard productCard)
        {
            this.mediaService = mediaService;
            this.bookService = bookService;
            this.cdService = cdService;
            this.dvdService = dvdService;
            this.searchMediaResultView = searchMediaResultView;
            this.productMiniCard = productMiniCard;
            this.productCard = productCard;
        }
        
        public async Task LoadBookDetails()
        {
            if (currentID == -1)
            {
                await Task.Delay(1000);
            }
            currentBook = await bookService.GetBookByIdAsync(currentID);
            if (currentBook != null)
            {
                Media currentMedia = await mediaService.GetMediaByIdAsync(currentID);

                BookDetailsView.Instance.lblTitle.Text = currentMedia.Title;
                BookDetailsView.Instance.lblPrice.Text = $"{currentMedia.getPriceFormat()}đ";
                BookDetailsView.Instance.lblAuthor.Text = $"Tác giả: {currentBook.author}";
                BookDetailsView.Instance.lblPublish_date.Text = $"Ngày xuất bản: {currentBook.getPublishedDate()}";
                BookDetailsView.Instance.lblPublisher.Text = $"Nhà xuất bản: {currentBook.publisher}";
                BookDetailsView.Instance.lblLanguage.Text = $"Ngôn ngữ: {currentBook.language}";
                BookDetailsView.Instance.lblCoverType.Text = $"Loại bìa: {currentBook.coverType}";
                BookDetailsView.Instance.lblPageNumber.Text = $"Số trang: {currentBook.numberOfPages}";
                if (!string.IsNullOrEmpty(currentMedia.ImgURL))
                {
                    using (WebClient webClient = new WebClient())
                    {
                        try
                        {
                            byte[] data = await webClient.DownloadDataTaskAsync(currentMedia.ImgURL);
                            using (MemoryStream mem = new MemoryStream(data))
                            {
                                BookDetailsView.Instance.productImage.Image = Image.FromStream(mem);
                            }
                        }
                        catch (WebException ex)
                        {
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
                else
                {
                    BookDetailsView.Instance.productImage.Image = null;
                }
                BookDetailsView.Instance.Refresh();
            }
        }
        public async Task LoadCDDetails()
        {
            if (currentID == -1)
            {
                await Task.Delay(1000);
            }
            currentCD = await cdService.GetCDByIdAsync(currentID);
            if (currentCD != null)
            {
                AIMS.Models.Entities.Media currentMedia = await mediaService.GetMediaByIdAsync(currentID);

                CDDetailsView.Instance.lblTitle.Text = currentMedia.Title;
                CDDetailsView.Instance.lblPrice.Text = $"{currentMedia.getPriceFormat()}đ";

                CDDetailsView.Instance.lblArtist.Text = $"Nghệ sĩ: {currentCD.artist}";
                CDDetailsView.Instance.lblRelease_date.Text = $"Ngày phát hành: {currentCD.getReleasedDate()}";
                CDDetailsView.Instance.lblRecordLabel.Text = $"Hãng thu âm: {currentCD.recordLabel}";
                CDDetailsView.Instance.lblTrackList.Text = $"Danh sách bản nhạc: {currentCD.tracklist}";
                if (!string.IsNullOrEmpty(currentMedia.ImgURL))
                {
                    using (WebClient webClient = new WebClient())
                    {
                        try
                        {
                            byte[] data = await webClient.DownloadDataTaskAsync(currentMedia.ImgURL);
                            using (MemoryStream mem = new MemoryStream(data))
                            {
                                CDDetailsView.Instance.productImage.Image = Image.FromStream(mem);
                            }
                        }
                        catch (WebException ex)
                        {
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
                else
                {
                    CDDetailsView.Instance.productImage.Image = null;
                }
                CDDetailsView.Instance.Refresh();
            }
        }
        public async Task LoadDVDDetails()
        {
            if (currentID == -1)
            {
                await Task.Delay(1000);
            }
            currentDVD = await dvdService.GetDVDByIdAsync(currentID);
            if (currentDVD != null)
            {
                AIMS.Models.Entities.Media currentMedia = await mediaService.GetMediaByIdAsync(currentID);

                DVDDetailsView.Instance.lblTitle.Text = currentMedia.Title;
                DVDDetailsView.Instance.lblPrice.Text = $"{currentMedia.GetType()}đ";
                DVDDetailsView.Instance.lblDirector.Text = $"Đạo diễn: {currentDVD.director}";
                DVDDetailsView.Instance.lblStudio.Text = $"Studio: {currentDVD.studio}";
                DVDDetailsView.Instance.lblRelease_date.Text = $"Ngày phát hành: {currentDVD.getReleasedDate()}";
                DVDDetailsView.Instance.lblDics_type.Text = $"Loại đĩa: {currentDVD.discType}";
                DVDDetailsView.Instance.lblRuntime.Text = $"Thời lượng: {currentDVD.runtime} phút";
                DVDDetailsView.Instance.lblSubtitle.Text = $"Phụ đề: {currentDVD.subtitle}";
                if (!string.IsNullOrEmpty(currentMedia.ImgURL))
                {
                    using (WebClient webClient = new WebClient())
                    {
                        try
                        {
                            byte[] data = await webClient.DownloadDataTaskAsync(currentMedia.ImgURL);
                            using (MemoryStream mem = new MemoryStream(data))
                            {
                                DVDDetailsView.Instance.productImage.Image = Image.FromStream(mem);
                            }
                        }
                        catch (WebException ex)
                        {
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
                else
                {
                    DVDDetailsView.Instance.productImage.Image = null;
                }
                DVDDetailsView.Instance.Refresh();
            }
        }
        #endregion
        public async Task<AIMS.Models.Entities.Media> GetMediaAsync(int mediaID)
        {
            return await mediaService.GetMediaByIdAsync(mediaID);
        }
        public void ViewCategoryList(string category)
        {
            searchMediaResultView.SearchContent = "";
            searchMediaResultView.Category = category;
            MainForm.Instance.mainFormPanel.Controls.Clear();
            MainForm.Instance.mainFormPanel.Controls.Add(searchMediaResultView);
            searchMediaResultView.Show();
        }
        public void ViewSearchResult(string searchContent)
        {
            searchMediaResultView.SearchContent = searchContent;
            searchMediaResultView.Category = "";
            MainForm.Instance.mainFormPanel.Controls.Clear();
            MainForm.Instance.mainFormPanel.Controls.Add(searchMediaResultView);
            searchMediaResultView.Show();
        }
        // Load sản phẩm cho Card
        private void LoadProductCards(FlowLayoutPanel flp, List<AIMS.Models.Entities.Media> list, string cardType)
        {
            flp.Controls.Clear();
            foreach (var product in list)
            {
                if (cardType == "productMiniCard")
                {
                    productMiniCard.LoadProduct(product);
                    flp.Controls.Add(productMiniCard);
                }
                if (cardType == "productCard")
                {
                    productCard.LoadProduct(product);
                    flp.Controls.Add(productCard);
                }
            }
        }
        // Load danh sách sản phẩm theo danh mục
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
        // Lọc và sắp xếp danh sách sản phẩm 
        public async Task FilterAndSortProductsAdvancedAsync(FlowLayoutPanel flp, string cardType, string title = "", decimal? minPrice = null, decimal? maxPrice = null, string categoryFilter = null, bool sortByPriceDesc = false)
        {
            List<Media> allMedia = await GetAllMedia();
            IEnumerable<Media> filteredList = allMedia;
            if (!string.IsNullOrEmpty(title))
                filteredList = filteredList.Where(m => m.Title.ToLower().Contains(title.ToLower().Trim()));
            if (minPrice.HasValue)
                filteredList = filteredList.Where(m => m.Price >= minPrice.Value);
            if (maxPrice.HasValue)
                filteredList = filteredList.Where(m => m.Price <= maxPrice.Value);
            if (!string.IsNullOrEmpty(categoryFilter))
                filteredList = filteredList.Where(m => m.Category.Equals(categoryFilter, StringComparison.OrdinalIgnoreCase));
            List<Media> sortedList;
            if (sortByPriceDesc)
                sortedList = filteredList.OrderByDescending(m => m.Price).ToList();
            else
                sortedList = filteredList.OrderBy(m => m.Price).ToList();
            LoadProductCards(flp, sortedList, cardType);
        }
        // Lấy danh sách tất cả sản phẩm trên hệ thống (Media)
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
