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
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Tên đăng nhập hoặc mật khẩu không đúng.");
                TempData["ErrorMessage"] = "Tên đăng nhập hoặc mật khẩu không đúng.";
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
            if (ModelState.IsValid)
            {
                var user_checkUsernameExist = await _userRepository.GetByUsername(model.Username);
                var user_checkEmailExist = await _userRepository.GetByEmail(model.Email);
                if (user_checkUsernameExist != null)
                {
                    ModelState.AddModelError("Username", "Tên đăng nhập đã tồn tại.");
                    return View(model);
                }
                if (user_checkEmailExist != null)
                {
                    ModelState.AddModelError("Email", "Email đã được sử dụng.");
                    return View(model);
                }
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
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}