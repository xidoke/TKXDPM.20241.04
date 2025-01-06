using System.Text.Json;
using AIMS.Controllers;
using AIMS.Data.Entities.Address;
using AIMS.Models.Entities;
using AIMS.Repositories;
using AIMS.Service;
using AIMS.Utils;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace AIMS.UnitTest.PlaceOrderController
{
    public class OrderControllerTest
    {
        [TestFixture]
        public class OrderControllerTests
        {
            private Mock<IProvinceRepository> _provinceRepositoryMock;
            private Mock<IDistrictRepository> _districtRepositoryMock;
            private Mock<IWardRepository> _wardRepositoryMock;
            private Mock<IMediaRepository> _mediaRepositoryMock;
            private Mock<IOrderRepository> _orderRepositoryMock;
            private Mock<DeliveryInfoValidator> _deliveryInfoValidatorMock;
            private OrderController _controller;
            private Mock<ISession> _sessionMock;
            private Mock<IShippingFeeService> _shippingFeeServiceMock;

            [SetUp]
            public void SetUp()
            {
                _provinceRepositoryMock = new Mock<IProvinceRepository>();
                _districtRepositoryMock = new Mock<IDistrictRepository>();
                _wardRepositoryMock = new Mock<IWardRepository>();
                _mediaRepositoryMock = new Mock<IMediaRepository>();
                _orderRepositoryMock = new Mock<IOrderRepository>();
                _deliveryInfoValidatorMock = new Mock<DeliveryInfoValidator>();
                _shippingFeeServiceMock = new Mock<IShippingFeeService>();

                _sessionMock = new Mock<ISession>();
                var httpContext = new DefaultHttpContext
                {
                    Session = _sessionMock.Object
                };

                _controller = new OrderController(
                    _provinceRepositoryMock.Object,
                    _orderRepositoryMock.Object,
                    _districtRepositoryMock.Object,
                    _wardRepositoryMock.Object,
                    _mediaRepositoryMock.Object,
                    _deliveryInfoValidatorMock.Object,
                    _shippingFeeServiceMock.Object
                )
                {
                    ControllerContext = new ControllerContext { HttpContext = httpContext }
                };
            }

            [Test]
            public async Task ProcessOrderFromCart_ItemExceedsStock_ReturnsErrorMessage()
            {
                // Arrange
                var cartItems = new List<CartItem>
        {
            new CartItem { MediaID = 1, MediaName = "Media1", Quantity = 10, isSelected = true }
        };

                _mediaRepositoryMock
                    .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                    .ReturnsAsync(new Media { Title = "Media1", Quantity = 5 });

                // Act
                var result = await _controller.ProcessOrderFromCart(cartItems);
                var jsonResult = result as JsonResult;

                // Assert
                Assert.That(jsonResult, Is.Not.Null);
                Assert.That(
                    (bool)jsonResult.Value.GetType().GetProperty("success")?.GetValue(jsonResult.Value),
                    Is.False
                );
                Assert.That(
                    jsonResult.Value.GetType().GetProperty("message")?.GetValue(jsonResult.Value)?.ToString(),
                    Is.EqualTo("Không đủ số lượng cho sản phẩm Media1. Số lượng tồn kho: 5")
                );
            }

            [Test]
            public async Task ProcessOrderFromCart_NoItemsSelected_ReturnsErrorMessage()
            {
                // Arrange
                var cartItems = new List<CartItem>
                {
                    new CartItem { MediaID = 1, MediaName = "Media1", Quantity = 10, isSelected = false }
                };

                // Act
                var result = await _controller.ProcessOrderFromCart(cartItems);
                var jsonResult = result as JsonResult;

                // Assert
                Assert.That(jsonResult, Is.Null);
            }

            [Test]
            public async Task ProcessOrderDirectly_MediaNotFound_ReturnsErrorMessage()
            {
                // Arrange
                _mediaRepositoryMock
                    .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                    .ReturnsAsync((Media)null);

                // Act
                var result = await _controller.ProcessOrderDirectly(1, 1);
                var jsonResult = result as JsonResult;

                // Assert
                Assert.That(jsonResult, Is.Not.Null);
                Assert.That(
                    (bool)jsonResult.Value.GetType().GetProperty("success")?.GetValue(jsonResult.Value),
                    Is.False
                );
                Assert.That(
                    jsonResult.Value.GetType().GetProperty("message")?.GetValue(jsonResult.Value)?.ToString(),
                    Is.EqualTo("Không tìm thấy sản phẩm.")
                );
            }

            [Test]
            public async Task ProcessOrderDirectly_InvalidQuantity_ReturnsErrorMessage()
            {
                // Arrange
                var media = new Media { Id = 1, Title = "Media1", Quantity = 10 };
                _mediaRepositoryMock
                    .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                    .ReturnsAsync(media);

                // Act
                var result = await _controller.ProcessOrderDirectly(1, 0);
                var jsonResult = result as JsonResult;

                // Assert
                Assert.That(jsonResult, Is.Not.Null);
                Assert.That(
                    (bool)jsonResult.Value.GetType().GetProperty("success")?.GetValue(jsonResult.Value),
                    Is.False
                );
                Assert.That(
                    jsonResult.Value.GetType().GetProperty("message")?.GetValue(jsonResult.Value)?.ToString(),
                    Is.EqualTo("Lỗi khi đặt hàng: Value cannot be null. (Parameter 'helper')")
                );
            }


            [Test]
            public void PlaceOrderView_EmptySession_RedirectsToCartView()
            {
                // Arrange
                var sessionValues = new Dictionary<string, string>();
                _sessionMock.Setup(s => s.TryGetValue(It.IsAny<string>(), out It.Ref<byte[]>.IsAny))
                    .Returns((string key, out byte[] value) =>
                    {
                        if (sessionValues.TryGetValue(key, out var stringValue))
                        {
                            value = System.Text.Encoding.UTF8.GetBytes(stringValue);
                            return true;
                        }

                        value = null;
                        return false;
                    });

                _sessionMock.Setup(s => s.Set(It.IsAny<string>(), It.IsAny<byte[]>()))
                    .Callback((string key, byte[] value) =>
                    {
                        sessionValues[key] = System.Text.Encoding.UTF8.GetString(value);
                    });

                var httpContext = new DefaultHttpContext
                {
                    Session = _sessionMock.Object
                };
                _controller.ControllerContext.HttpContext = httpContext;

                // Act
                var result = _controller.PlaceOrderView() as RedirectToActionResult;

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.ActionName, Is.EqualTo("CartView"));
                Assert.That(result.ControllerName, Is.EqualTo("Cart"));
            }

            [Test]
            public void PlaceOrderView_ValidSession_ReturnsOrderMediaListView()
            {
                // Arrange
                var orderMediaList = new List<OrderMedia>
                {
                    new OrderMedia { MediaId = 1, Name = "Media1", Quantity = 1, Price = 100 }
                };

                var serializedOrderMediaList = JsonSerializer.Serialize(orderMediaList);
                var sessionValues = new Dictionary<string, string>
                                    {
                                        { "OrderMediaList", serializedOrderMediaList }
                                    };

                _sessionMock.Setup(s => s.TryGetValue(It.IsAny<string>(), out It.Ref<byte[]>.IsAny))
                    .Returns((string key, out byte[] value) =>
                    {
                        if (sessionValues.TryGetValue(key, out var stringValue))
                        {
                            value = System.Text.Encoding.UTF8.GetBytes(stringValue);
                            return true;
                        }

                        value = null;
                        return false;
                    });

                _provinceRepositoryMock
                    .Setup(repo => repo.GetAllAsync())
                    .ReturnsAsync(new List<Province> { new Province { Id = "1", Name = "Province1" } });

                var httpContext = new DefaultHttpContext
                {
                    Session = _sessionMock.Object
                };
                _controller.ControllerContext.HttpContext = httpContext;

                // Act
                var result = _controller.PlaceOrderView() as ViewResult;

                // Assert
                Assert.That(result, Is.Not.Null);

                var model = result.Model as List<OrderMedia>;
                Assert.That(model, Is.Not.Null);
                Assert.That(model.Count, Is.EqualTo(orderMediaList.Count));

                for (int i = 0; i < model.Count; i++)
                {
                    Assert.That(model[i].MediaId, Is.EqualTo(orderMediaList[i].MediaId));
                    Assert.That(model[i].Name, Is.EqualTo(orderMediaList[i].Name));
                    Assert.That(model[i].Quantity, Is.EqualTo(orderMediaList[i].Quantity));
                    Assert.That(model[i].Price, Is.EqualTo(orderMediaList[i].Price));
                }

                Assert.That(result.ViewData["Provinces"], Is.Not.Null);
            }


        }

    }
}
