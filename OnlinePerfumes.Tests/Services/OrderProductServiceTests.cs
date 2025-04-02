using Moq;
using NUnit.Framework;
using OnlinePerfumes.Core.IServices;
using OnlinePerfumes.Core.Service;
using OnlinePerfumes.DataAccess.Repository;
using OnlinePerfumes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OnlinePerfumes.Tests.Services
{
    [TestFixture]
    public class OrderProductServiceTests
    {
        private Mock<IRepository<OrderProduct>> _mockRepo;
        private OrderProductService _orderProductService;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<IRepository<OrderProduct>>();
            _orderProductService = new OrderProductService(_mockRepo.Object);
        }

        [Test]
        public async Task AddAsync_CallsRepositoryAddAsync()
        {
            // Arrange
            var orderProduct = new OrderProduct
            {
                OrderId = 1,
                ProductId = 1,
                Quantity = 2,
                Price = 50.00m
            };

            // Act
            await _orderProductService.AddAsync(orderProduct);

            // Assert
            _mockRepo.Verify(r => r.AddAsync(orderProduct), Times.Once());
        }

        [TestCase(1)]
        [TestCase(5)]
        public async Task DeleteAsync_ExistingId_CallsRepositoryDelete(int id)
        {
            // Arrange
            var orderProduct = new OrderProduct
            {
                OrderId = id,
                ProductId = 1,
                Quantity = 1,
                Price = 25.00m
            };
            _mockRepo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(orderProduct);

            // Act
            await _orderProductService.DeleteAsync(id);

            // Assert
            _mockRepo.Verify(r => r.GetByIdAsync(id), Times.Once());
            _mockRepo.Verify(r => r.DeleteAsync(orderProduct), Times.Once());
        }

        [Test]
        public async Task Find_WithFilter_ReturnsFilteredOrderProducts()
        {
            // Arrange
            var orderProducts = new List<OrderProduct>
            {
                new OrderProduct { OrderId = 1, ProductId = 1, Quantity = 1, Price = 30.00m },
                new OrderProduct { OrderId = 2, ProductId = 2, Quantity = 2, Price = 60.00m }
            };
            _mockRepo.Setup(r => r.Find(It.IsAny<Expression<Func<OrderProduct, bool>>>())).ReturnsAsync(orderProducts.Where(op => op.Price > 50).ToList());

            Expression<Func<OrderProduct, bool>> filter = op => op.Price > 50;

            // Act
            var result = await _orderProductService.Find(filter);

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().OrderId, Is.EqualTo(2));
            Assert.That(result.First().Price, Is.EqualTo(60.00m));
        }

        [Test]
        public void GetAll_ReturnsQueryable()
        {
            // Arrange
            var orderProducts = new List<OrderProduct>
            {
                new OrderProduct { OrderId = 1, ProductId = 1, Quantity = 1, Price = 30.00m }
            }.AsQueryable();
            _mockRepo.Setup(r => r.GetAll()).Returns(orderProducts);

            // Act
            var result = _orderProductService.GetAll();

            // Assert
            Assert.That(result, Is.InstanceOf<IQueryable<OrderProduct>>());
            Assert.That(result.Count(), Is.EqualTo(1));
            _mockRepo.Verify(r => r.GetAll(), Times.Once());
        }

        [Test]
        public async Task GetAllAsync_ReturnsAllOrderProducts()
        {
            // Arrange
            var orderProducts = new List<OrderProduct>
            {
                new OrderProduct { OrderId = 1, ProductId = 1, Quantity = 1, Price = 30.00m }
            };
            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(orderProducts);

            // Act
            var result = await _orderProductService.GetAllAsync();

            // Assert
            Assert.That(result.Count(), Is.EqualTo(1));
            _mockRepo.Verify(r => r.GetAllAsync(), Times.Once());
        }

        [TestCase(1, 1, 1)]
        [TestCase(2, 2, 3)]
        public void GetById_ExistingId_ReturnsOrderProduct(int orderId, int productId, int quantity)
        {
            // Arrange
            var orderProduct = new OrderProduct
            {
                OrderId = orderId,
                ProductId = productId,
                Quantity = quantity,
                Price = 40.00m
            };
            _mockRepo.Setup(r => r.GetById(orderId)).Returns(orderProduct);

            // Act
            var result = _orderProductService.GetById(orderId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.OrderId, Is.EqualTo(orderId));
            Assert.That(result.ProductId, Is.EqualTo(productId));
            Assert.That(result.Quantity, Is.EqualTo(quantity));
            _mockRepo.Verify(r => r.GetById(orderId), Times.Once());
        }

        [TestCase(1, 1, 1)]
        [TestCase(2, 2, 3)]
        public async Task GetByIdAsync_ExistingId_ReturnsOrderProduct(int orderId, int productId, int quantity)
        {
            // Arrange
            var orderProduct = new OrderProduct
            {
                OrderId = orderId,
                ProductId = productId,
                Quantity = quantity,
                Price = 40.00m
            };
            _mockRepo.Setup(r => r.GetByIdAsync(orderId)).ReturnsAsync(orderProduct);

            // Act
            var result = await _orderProductService.GetByIdAsync(orderId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.OrderId, Is.EqualTo(orderId));
            Assert.That(result.ProductId, Is.EqualTo(productId));
            Assert.That(result.Quantity, Is.EqualTo(quantity));
            _mockRepo.Verify(r => r.GetByIdAsync(orderId), Times.Once());
        }

        [Test]
        public async Task GetByIdAsync_NonExistingId_ReturnsNull()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((OrderProduct)null);

            // Act
            var result = await _orderProductService.GetByIdAsync(999);

            // Assert
            Assert.That(result, Is.Null);
            _mockRepo.Verify(r => r.GetByIdAsync(999), Times.Once());
        }

        [Test]
        public async Task UpdateAsync_CallsRepositoryUpdate()
        {
            // Arrange
            var orderProduct = new OrderProduct
            {
                OrderId = 1,
                ProductId = 1,
                Quantity = 2,
                Price = 50.00m
            };

            // Act
            await _orderProductService.UpdateAsync(orderProduct);

            // Assert
            _mockRepo.Verify(r => r.UpdateAsync(orderProduct), Times.Once());
        }

        [Test]
        public async Task Find_NoMatchingOrderProducts_ReturnsEmptyList()
        {
            // Arrange
            var orderProducts = new List<OrderProduct>
            {
                new OrderProduct { OrderId = 1, ProductId = 1, Quantity = 1, Price = 30.00m }
            };
            _mockRepo.Setup(r => r.Find(It.IsAny<Expression<Func<OrderProduct, bool>>>())).ReturnsAsync(new List<OrderProduct>());

            Expression<Func<OrderProduct, bool>> filter = op => op.Price > 50;

            // Act
            var result = await _orderProductService.Find(filter);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }
    }
}
