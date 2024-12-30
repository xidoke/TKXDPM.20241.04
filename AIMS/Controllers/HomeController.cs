using AIMS.Data.Repositories.Interfaces;
using AIMS.Models;
using Microsoft.AspNetCore.Mvc;
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


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
