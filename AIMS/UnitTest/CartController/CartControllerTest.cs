using System.Linq;
using AIMS.Controllers;
using AIMS.Models.Entities;
using AIMS.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;


namespace AIMS.UnitTest.CartController
{
    [TestFixture]
    public class CartControllerTest
    {
        private Mock<IMediaRepository> _mediaRepositoryMock;
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private Mock<ICartRepository> _cartRepositoryMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private Controllers.CartController _cartController;

        [SetUp]
        public void Setup()
        {
            _mediaRepositoryMock = new Mock<IMediaRepository>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _cartRepositoryMock = new Mock<ICartRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();

            _cartController = new Controllers.CartController(
                _mediaRepositoryMock.Object,
                _httpContextAccessorMock.Object,
                _cartRepositoryMock.Object,
                _userRepositoryMock.Object);

            // Mock HttpContext for session handling
            var httpContext = new DefaultHttpContext();
            httpContext.Session = new Mock<ISession>().Object;
            _httpContextAccessorMock.Setup(a => a.HttpContext).Returns(httpContext);
        }

        [Test]
        public async Task CheckStock_ReturnsSufficientStockStatus_WhenStockIsAvailable()
        {
            // Arrange
            var productId = 1;
            var quantity = 5;
            _mediaRepositoryMock.Setup(repo => repo.GetByIdAsync(productId))
                .ReturnsAsync(new Media { Id = productId, Quantity = 10 });

            // Act
            var result = await _cartController.CheckStock(productId, quantity) as JsonResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            dynamic response = result.Value;
            Assert.That(response.status, Is.EqualTo("Đủ số lượng"));
        }

        [Test]
        public async Task CheckStock_ReturnsInsufficientStockStatus_WhenStockIsNotAvailable()
        {
            // Arrange
            var productId = 1;
            var quantity = 5;
            _mediaRepositoryMock.Setup(repo => repo.GetByIdAsync(productId))
                .ReturnsAsync(new Media { Id = productId, Quantity = 3 });

            // Act
            var result = await _cartController.CheckStock(productId, quantity) as JsonResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            dynamic response = result.Value;
            Assert.That(response.status, Is.EqualTo("Không đủ số lượng. Hiện có: 3"));
        }
    }
}
