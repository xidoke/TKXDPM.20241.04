using AIMS.Controllers;
using AIMS.Models.Entities;
using AIMS.Models;
using AIMS.Repositories;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

[TestFixture]
public class RegisterControllerTests
{
    private Mock<IUserRepository> _userRepositoryMock;
    private AccountController _controller;
    private Mock<ITempDataDictionary> _tempDataMock;

    [SetUp]
    public void SetUp()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _controller = new AccountController(_userRepositoryMock.Object);

        // Mock TempData
        _tempDataMock = new Mock<ITempDataDictionary>();
        _controller.TempData = _tempDataMock.Object;
    }

    [Test]
    public async Task Register_EmptyFields_ReturnsViewWithErrors()
    {
        // Arrange
        var model = new RegisterViewModel
        {
            Fullname = "",
            Username = "",
            Email = "",
            Password = "",
            Phone = ""
        };

        // Act
        var result = await _controller.Register(model);

        // Assert
        Assert.That(result, Is.InstanceOf<ViewResult>());
        var viewResult = result as ViewResult;

        // Kiểm tra lỗi trong ModelState
        Assert.That(viewResult.ViewData.ModelState.ContainsKey("Fullname"));
        Assert.That(viewResult.ViewData.ModelState["Fullname"].Errors[0].ErrorMessage, Is.EqualTo("Tên đầy đủ là bắt buộc."));

        Assert.That(viewResult.ViewData.ModelState.ContainsKey("Username"));
        Assert.That(viewResult.ViewData.ModelState["Username"].Errors[0].ErrorMessage, Is.EqualTo("Tên đăng nhập là bắt buộc."));

        Assert.That(viewResult.ViewData.ModelState.ContainsKey("Email"));
        Assert.That(viewResult.ViewData.ModelState["Email"].Errors[0].ErrorMessage, Is.EqualTo("Email là bắt buộc."));

        Assert.That(viewResult.ViewData.ModelState.ContainsKey("Password"));
        Assert.That(viewResult.ViewData.ModelState["Password"].Errors[0].ErrorMessage, Is.EqualTo("Mật khẩu là bắt buộc."));

        // Kiểm tra TempData
        _tempDataMock.VerifySet(tempData => tempData["ErrorMessage"] = "Tên đầy đủ là bắt buộc.", Times.Once);
        _tempDataMock.VerifySet(tempData => tempData["ErrorMessage"] = "Tên đăng nhập là bắt buộc.", Times.Once);
        _tempDataMock.VerifySet(tempData => tempData["ErrorMessage"] = "Email là bắt buộc.", Times.Once);
        _tempDataMock.VerifySet(tempData => tempData["ErrorMessage"] = "Mật khẩu là bắt buộc.", Times.Once);
    }


    [Test]
    public async Task Register_UsernameExists_ReturnsViewWithError()
    {
        // Arrange
        var model = new RegisterViewModel
        {
            Fullname = "Test User",
            Username = "existinguser",
            Email = "newemail@example.com",
            Password = "password123",
            Phone = "0123456789"
        };

        var existingUser = new User { Username = "existinguser" };

        _userRepositoryMock
            .Setup(repo => repo.GetByUsername(It.IsAny<string>()))
            .ReturnsAsync(existingUser);

        // Act
        var result = await _controller.Register(model);

        // Assert
        Assert.That(result, Is.InstanceOf<ViewResult>());
        var viewResult = result as ViewResult;
        Assert.That(viewResult.ViewData.ModelState.ContainsKey("Username"));
        _tempDataMock.VerifySet(tempData => tempData["ErrorMessage"] = "Tên đăng nhập đã tồn tại.", Times.Once);
    }

    [Test]
    public async Task Register_EmailExists_ReturnsViewWithError()
    {
        // Arrange
        var model = new RegisterViewModel
        {
            Fullname = "Test User",
            Username = "newuser",
            Email = "existingemail@example.com",
            Password = "password123",
            Phone = "0123456789"
        };

        var existingUser = new User { Email = "existingemail@example.com" };

        _userRepositoryMock
            .Setup(repo => repo.GetByEmail(It.IsAny<string>()))
            .ReturnsAsync(existingUser);

        // Act
        var result = await _controller.Register(model);

        // Assert
        Assert.That(result, Is.InstanceOf<ViewResult>());
        var viewResult = result as ViewResult;
        Assert.That(viewResult.ViewData.ModelState.ContainsKey("Email"));
        _tempDataMock.VerifySet(tempData => tempData["ErrorMessage"] = "Email đã được sử dụng.", Times.Once);
    }

    [Test]
    public async Task Register_ValidInputs_RedirectsToLogin()
    {
        // Arrange
        var model = new RegisterViewModel
        {
            Fullname = "Test User",
            Username = "newuser",
            Email = "newemail@example.com",
            Password = "password123",
            Phone = "0123456789"
        };

        _userRepositoryMock
            .Setup(repo => repo.GetByUsername(It.IsAny<string>()))
            .ReturnsAsync((User)null);

        _userRepositoryMock
            .Setup(repo => repo.GetByEmail(It.IsAny<string>()))
            .ReturnsAsync((User)null);

        // Act
        var result = await _controller.Register(model);

        // Assert
        Assert.That(result, Is.InstanceOf<RedirectToActionResult>());
        var redirectResult = result as RedirectToActionResult;
        Assert.That(redirectResult.ActionName, Is.EqualTo("Login"));
        Assert.That(redirectResult.ControllerName, Is.EqualTo("Account"));
        _tempDataMock.VerifySet(tempData => tempData["SuccessMessage"] = "Đăng ký thành công! Bạn có thể đăng nhập ngay bây giờ.", Times.Once);
    }
}
