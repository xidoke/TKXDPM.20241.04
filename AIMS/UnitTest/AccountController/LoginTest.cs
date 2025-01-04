using AIMS.Controllers;
using AIMS.Models;
using AIMS.Models.Entities;
using AIMS.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AIMS.UnitTest.AccountControllerTests
{
    [TestFixture]
    public class LoginControllerTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private AccountController _controller;
        private Mock<ITempDataDictionary> _tempDataMock;

        [SetUp]
        public void SetUp()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _controller = new AccountController(_userRepositoryMock.Object);

            // Mock HttpContext
            var httpContext = new DefaultHttpContext();

            // Mock AuthenticationService
            var authenticationServiceMock = new Mock<IAuthenticationService>();
            authenticationServiceMock
                .Setup(service => service.SignInAsync(
                    It.IsAny<HttpContext>(),
                    It.IsAny<string>(),
                    It.IsAny<ClaimsPrincipal>(),
                    It.IsAny<AuthenticationProperties>()))
                .Returns(Task.CompletedTask);

            // Add AuthenticationService to HttpContext
            httpContext.RequestServices = new ServiceCollection()
                .AddSingleton(authenticationServiceMock.Object)
                .BuildServiceProvider();

            _controller.ControllerContext = new ControllerContext { HttpContext = httpContext };

            // Mock TempData
            _tempDataMock = new Mock<ITempDataDictionary>();
            _controller.TempData = _tempDataMock.Object;

            // Mock IUrlHelper
            var urlHelperMock = new Mock<IUrlHelper>();
            urlHelperMock
                .Setup(url => url.Action(It.IsAny<UrlActionContext>()))
                .Returns("http://localhost/Home/Index");
            _controller.Url = urlHelperMock.Object;
        }

        [Test]
        public async Task Login_ValidCredentials_RedirectsToHome_WithSuccessMessage()
        {
            // Arrange
            var model = new LoginViewModel
            {
                Username = "testuser",
                Password = "password123",
                RememberMe = true
            };

            var user = new User
            {
                Username = "testuser",
                Password = "password123",
                Email = "testuser@example.com",
                Id = 1
            };

            _userRepositoryMock
                .Setup(repo => repo.GetByUsername(It.IsAny<string>()))
                .ReturnsAsync(user);

            // Act
            var result = await _controller.Login(model);

            // Assert
            Assert.That(result, Is.InstanceOf<RedirectToActionResult>());
            var redirectResult = result as RedirectToActionResult;
            Assert.That(redirectResult.ActionName, Is.EqualTo("Index"));
            Assert.That(redirectResult.ControllerName, Is.EqualTo("Home"));

            // Check TempData
            _tempDataMock.VerifySet(tempData => tempData["SuccessMessage"] = "Đăng nhập thành công!", Times.Once);
        }

        [Test]
        public async Task Login_InvalidUsername_ReturnsViewWithErrorMessage()
        {
            // Arrange
            var model = new LoginViewModel
            {
                Username = "wronguser",
                Password = "password123",
                RememberMe = true
            };

            _userRepositoryMock
                .Setup(repo => repo.GetByUsername(It.IsAny<string>()))
                .ReturnsAsync((User)null);

            // Act
            var result = await _controller.Login(model);

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());
            var viewResult = result as ViewResult;
            Assert.That(viewResult.ViewData.ModelState.ErrorCount, Is.EqualTo(1));
            Assert.That(viewResult.ViewData.ModelState[""].Errors[0].ErrorMessage, Is.EqualTo("Tên đăng nhập hoặc mật khẩu không đúng."));

            // Check TempData
            _tempDataMock.VerifySet(tempData => tempData["ErrorMessage"] = "Tên đăng nhập hoặc mật khẩu không đúng.", Times.Once);
        }

        [Test]
        public async Task Login_InvalidPassword_ReturnsViewWithErrorMessage()
        {
            // Arrange
            var model = new LoginViewModel
            {
                Username = "testuser",
                Password = "wrongpassword",
                RememberMe = true
            };

            var user = new User
            {
                Username = "testuser",
                Password = "password123", // Correct password
                Email = "testuser@example.com",
                Id = 1
            };

            _userRepositoryMock
                .Setup(repo => repo.GetByUsername(It.IsAny<string>()))
                .ReturnsAsync(user);

            // Act
            var result = await _controller.Login(model);

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());
            var viewResult = result as ViewResult;
            Assert.That(viewResult.ViewData.ModelState.ErrorCount, Is.EqualTo(1));
            Assert.That(viewResult.ViewData.ModelState[""].Errors[0].ErrorMessage, Is.EqualTo("Tên đăng nhập hoặc mật khẩu không đúng."));

            // Check TempData
            _tempDataMock.VerifySet(tempData => tempData["ErrorMessage"] = "Tên đăng nhập hoặc mật khẩu không đúng.", Times.Once);
        }

        [Test]
        public async Task Login_EmptyUsername_ReturnsViewWithErrorMessage()
        {
            // Arrange
            var model = new LoginViewModel
            {
                Username = "",
                Password = "password123",
                RememberMe = true
            };

            _controller.ModelState.AddModelError("Username", "Tên đăng nhập là bắt buộc.");

            // Act
            var result = await _controller.Login(model);

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());
            var viewResult = result as ViewResult;
            Assert.That(viewResult.ViewData.ModelState.ContainsKey("Username"));

            // Check TempData
            _tempDataMock.VerifySet(tempData => tempData["ErrorMessage"] = "Tên đăng nhập là bắt buộc.", Times.Once);
        }

        [Test]
        public async Task Login_EmptyPassword_ReturnsViewWithErrorMessage()
        {
            // Arrange
            var model = new LoginViewModel
            {
                Username = "testuser",
                Password = "",
                RememberMe = true
            };

            _controller.ModelState.AddModelError("Password", "Mật khẩu là bắt buộc.");

            // Act
            var result = await _controller.Login(model);

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());
            var viewResult = result as ViewResult;
            Assert.That(viewResult.ViewData.ModelState.ContainsKey("Password"));

            // Check TempData
            _tempDataMock.VerifySet(tempData => tempData["ErrorMessage"] = "Mật khẩu là bắt buộc.", Times.Once);
        }
    }
}
