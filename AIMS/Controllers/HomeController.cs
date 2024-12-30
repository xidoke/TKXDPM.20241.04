using AIMS.Data.Entities;
using AIMS.Data.Repositories.Interfaces;
using AIMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AIMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMediaRepository _mediaRepository;
        public HomeController(ILogger<HomeController> logger, IMediaRepository mediaRepository)
        {
            _logger = logger;
            _mediaRepository = mediaRepository;
        }

        public async Task<IActionResult> Index()
        {
            // Get CDs, DVDs, and Books using the repository methods
            var cds = await _mediaRepository.GetCDsAsync();
            var dvds = await _mediaRepository.GetDVDsAsync();
            var books = await _mediaRepository.GetBooksAsync();

            // Create the ViewModel and populate it with the data
            var viewModel = new HomeViewModel
            {
                CDs = cds,
                DVDs = dvds,
                Books = books
            };

            return View(viewModel);
        }

        [HttpGet]
        [HttpGet]
        public async Task<IActionResult> SearchResultView(string searchTerm = null, string category = null, string sortBy = null)
        {
            // Lấy danh sách media dựa theo từ khóa tìm kiếm và danh mục
            List<Media> medias = await _mediaRepository.SearchAsync(searchTerm, category);

            // Sắp xếp danh sách media
            medias = SortMedias(medias, sortBy);

            ViewBag.Category = category;
            ViewBag.SortBy = sortBy;

            return View("~/Views/SearchResultView.cshtml", medias);
        }
        private List<Media> SortMedias(List<Media> medias, string sortBy)
        {
            switch (sortBy)
            {
                case "price-asc":
                    return medias.OrderBy(m => m.Price).ToList();
                case "price-desc":
                    return medias.OrderByDescending(m => m.Price).ToList();
                case "title-asc":
                    return medias.OrderBy(m => m.Title).ToList();
                case "title-desc":
                    return medias.OrderByDescending(m => m.Title).ToList();
                default:
                    return medias;
            }
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> MediaDetailsView(int id, string category)
        {
            if (category == "CD")
            {
                var cds = await _mediaRepository.GetCDsAsync();
                var cd = cds.FirstOrDefault(c => c.Id == id);
                if (cd == null)
                {
                    return NotFound(); // Hoặc chuyển hướng đến trang lỗi
                }
                ViewBag.Category = "CD";
                return View("~/Views/Media/MediaDetailsView.cshtml", cd);
            }
            else if (category == "Book")
            {
                var books = await _mediaRepository.GetBooksAsync();
                var book = books.FirstOrDefault(c => c.Id == id); // Thay Books bằng DbSet tương ứng của bạn
                if (book == null)
                {
                    return NotFound(); // Hoặc chuyển hướng đến trang lỗi
                }
                ViewBag.Category = "Book";
                return View("~/Views/Media/MediaDetailsView.cshtml", book);
            }
            else if (category == "DVD")
            {
                var dvds = await _mediaRepository.GetDVDsAsync();
                var dvd = dvds.FirstOrDefault(c => c.Id == id);// Thay DVDs bằng DbSet tương ứng của bạn
                if (dvd == null)
                {
                    return NotFound(); // Hoặc chuyển hướng đến trang lỗi
                }
                ViewBag.Category = "DVD";
                return View("~/Views/Media/MediaDetailsView.cshtml", dvd);
            }
            else
            {
                return BadRequest(); // Hoặc chuyển hướng đến trang lỗi
            }
        }
    }
}
