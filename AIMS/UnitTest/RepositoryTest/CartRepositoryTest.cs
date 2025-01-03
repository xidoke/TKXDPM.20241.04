//namespace AIMS.UnitTest.RepositoryTest
//{
//    using NUnit.Framework;
//    using Moq;
//    using Microsoft.EntityFrameworkCore;
//    using System.Collections.Generic;
//    using System.Linq;
//    using System.Threading.Tasks;
//    using AIMS.Data.Contexts;
//    using AIMS.Repositories;
//    using AIMS.Repositories.Impl;
//    using AIMS.Data.Entities;

//    [TestFixture]
//    public class CartRepositoryTest
//    {
//        private ApplicationDbContext _context;
//        private Mock<IUserRepository> _mockUserRepository;
//        private CartRepository _cartRepository;

//        [SetUp]
//        public void Setup()
//        {
//            // Setup InMemoryDatabase for ApplicationDbContext
//            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
//                .UseInMemoryDatabase(databaseName: "TestDatabase")
//                .Options;

//            _context = new ApplicationDbContext(options);
//            _mockUserRepository = new Mock<IUserRepository>();

//            // Mock user email
//            _mockUserRepository.Setup(repo => repo.GetCurrentUserEmail()).Returns("test@example.com");

//            // Initialize CartRepository
//            _cartRepository = new CartRepository(_context, _mockUserRepository.Object);
//        }

//        [TearDown]
//        public void TearDown()
//        {
//            _context.Database.EnsureDeleted(); // Clean up database after each test
//            _context.Dispose();
//        }

//        [Test]
//        public async Task GetCartItemsAsync_ReturnsCorrectItems_ForValidUser()
//        {
//            // Arrange
//            var email = "test@example.com";
//            var cartItems = new List<CartItem>
//        {
//            new CartItem { MediaID = 1, Email = email, MediaName = "Media1" },
//            new CartItem { MediaID = 2, Email = email, MediaName = "Media2" }
//        };

//            await _context.CartItems.AddRangeAsync(cartItems);
//            await _context.SaveChangesAsync();

//            // Act
//            var result = await _cartRepository.GetCartItemsAsync(email);

//            // Assert
//            Assert.IsNotNull(result);
//            Assert.AreEqual(2, result.Count);
//            Assert.IsTrue(result.Any(c => c.MediaName == "Media1"));
//        }

//        [Test]
//        public async Task AddCartItemAsync_AddsNewItem_ForCurrentUser()
//        {
//            // Arrange
//            var newItem = new CartItem { MediaID = 3, MediaName = "NewMedia", Price = 100 };

//            // Act
//            var result = await _cartRepository.AddCartItemAsync(newItem);

//            // Assert
//            Assert.IsNotNull(result);
//            Assert.AreEqual("test@example.com", result.Email);
//            Assert.AreEqual(100, result.Price);
//        }

//        [Test]
//        public async Task GetCartItemByIdAsync_ReturnsCorrectItem_ForValidMediaId()
//        {
//            // Arrange
//            var email = "test@example.com";
//            var cartItem = new CartItem { MediaID = 4, Email = email, MediaName = "SpecificMedia" };

//            await _context.CartItems.AddAsync(cartItem);
//            await _context.SaveChangesAsync();

//            // Act
//            var result = await _cartRepository.GetCartItemByIdAsync(4);

//            // Assert
//            Assert.IsNotNull(result);
//            Assert.AreEqual("SpecificMedia", result.MediaName);
//        }
//    }
//}




