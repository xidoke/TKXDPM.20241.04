using AIMS.Repositories;
using AIMS.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AIMS.Models.Entities;

namespace AIMS.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user_checkName = await _userRepository.GetByUsername(model.Username);

                if (user_checkName != null && user_checkName.Password == model.Password)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user_checkName.Username),
                        new Claim(ClaimTypes.NameIdentifier, user_checkName.Id.ToString()),
                        new Claim(ClaimTypes.Email, user_checkName.Email.ToString()),
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = model.RememberMe,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60)
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties
                    );

                    TempData["SuccessMessage"] = "Đăng nhập thành công!";
                    return RedirectToAction("Index", "Home");
                }

                TempData["ErrorMessage"] = "Tên đăng nhập hoặc mật khẩu không đúng.";
                ModelState.AddModelError(string.Empty, "Tên đăng nhập hoặc mật khẩu không đúng.");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(model.Username))
                {
                    TempData["ErrorMessage"] = "Tên đăng nhập là bắt buộc.";
                    ModelState.AddModelError("Username", "Tên đăng nhập là bắt buộc.");
                }

                if (string.IsNullOrWhiteSpace(model.Password))
                {
                    TempData["ErrorMessage"] = "Mật khẩu là bắt buộc.";
                    ModelState.AddModelError("Password", "Mật khẩu là bắt buộc.");
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            // Kiểm tra từng trường bị bỏ trống và thêm lỗi vào ModelState
            if (string.IsNullOrWhiteSpace(model.Fullname))
            {
                ModelState.AddModelError("Fullname", "Tên đầy đủ là bắt buộc.");
                TempData["ErrorMessage"] = "Tên đầy đủ là bắt buộc.";
            }

            if (string.IsNullOrWhiteSpace(model.Username))
            {
                ModelState.AddModelError("Username", "Tên đăng nhập là bắt buộc.");
                TempData["ErrorMessage"] = "Tên đăng nhập là bắt buộc.";
            }

            if (string.IsNullOrWhiteSpace(model.Email))
            {
                ModelState.AddModelError("Email", "Email là bắt buộc.");
                TempData["ErrorMessage"] = "Email là bắt buộc.";
            }

            if (string.IsNullOrWhiteSpace(model.Password))
            {
                ModelState.AddModelError("Password", "Mật khẩu là bắt buộc.");
                TempData["ErrorMessage"] = "Mật khẩu là bắt buộc.";
            }

            // Nếu có lỗi, trả về View cùng với ModelState
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Kiểm tra xem username hoặc email có tồn tại hay không
            var user_checkUsernameExist = await _userRepository.GetByUsername(model.Username);
            var user_checkEmailExist = await _userRepository.GetByEmail(model.Email);

            if (user_checkUsernameExist != null)
            {
                ModelState.AddModelError("Username", "Tên đăng nhập đã tồn tại.");
                TempData["ErrorMessage"] = "Tên đăng nhập đã tồn tại.";
                return View(model);
            }

            if (user_checkEmailExist != null)
            {
                ModelState.AddModelError("Email", "Email đã được sử dụng.");
                TempData["ErrorMessage"] = "Email đã được sử dụng.";
                return View(model);
            }

            // Tạo người dùng mới
            var user = new User
            {
                Fullname = model.Fullname,
                Username = model.Username,
                Email = model.Email,
                Password = model.Password,
                Phone = model.Phone,
                Admin = 0,
                Status = "Activated",
                Salt = "1",
            };

            await _userRepository.Add(user);

            TempData["SuccessMessage"] = "Đăng ký thành công! Bạn có thể đăng nhập ngay bây giờ.";
            return RedirectToAction("Login", "Account");
        }


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData["SuccessMessage"] = "Đăng xuất thành công!";
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            TempData["ErrorMessage"] = "Bạn không có quyền truy cập vào trang này.";
            return View();
        }
    }
}
