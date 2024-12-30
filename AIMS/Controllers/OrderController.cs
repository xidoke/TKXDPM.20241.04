using AIMS.Data.Entities;
using AIMS.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;

namespace AIMS.Controllers
{
    public class OrderController : Controller
    {
        private readonly IProvinceRepository _provinceRepository;
        private readonly IDistrictRepository _districtRepository;
        private readonly IWardRepository _wardRepository;
        // ... other dependencies ...

        public OrderController(IProvinceRepository provinceRepository, IDistrictRepository districtRepository, IWardRepository wardRepository /* ... other dependencies ... */)
        {
            _provinceRepository = provinceRepository;
            _districtRepository = districtRepository;
            _wardRepository = wardRepository;
            // ... other initializations ...
        }
        private List<CartItem> GetCartFromSession()
        {
            var cartJson = HttpContext.Session.GetString("Cart");
            if (string.IsNullOrEmpty(cartJson))
            {
                return new List<CartItem>();
            }
            return JsonSerializer.Deserialize<List<CartItem>>(cartJson);
        }

        private const string OrderMediaListSessionKey = "OrderMediaList";
        public IActionResult PlaceOrderView()
        {
            // Retrieve the Temp data from Session
            var orderMediaListJson = HttpContext.Session.GetString(OrderMediaListSessionKey);
            
            if (string.IsNullOrEmpty(orderMediaListJson))
            {
                // Handle the case where Session is empty (e.g., user directly accessed the URL)
                return RedirectToAction("CartView", "Cart"); // Redirect to cart
            }

            // Deserialize the JSON back to a list of OrderMedia
            var orderMediaList = JsonSerializer.Deserialize<List<OrderMedia>>(orderMediaListJson);
            var provinces = _provinceRepository.GetAllAsync().Result; // Consider making this async
            ViewBag.Provinces = new SelectList(provinces, "Id", "Name");
            // Clear the data from session after using it
            HttpContext.Session.Remove(OrderMediaListSessionKey);

            // Pass the list to the view
            return View(orderMediaList);
        }
        [HttpGet]
        public async Task<IActionResult> GetDistrictsByProvince(string provinceId)
        {
            var districts = await _districtRepository.GetDistrictsByProvinceIdAsync(provinceId);
            return Json(districts);
        }

        [HttpGet]
        public async Task<IActionResult> GetWardsByDistrict(string districtId)
        {
            var wards = await _wardRepository.GetWardsByDistrictIdAsync(districtId);
            return Json(wards);
        }
    }
}
