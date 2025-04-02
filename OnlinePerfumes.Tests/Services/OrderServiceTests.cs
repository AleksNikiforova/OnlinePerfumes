using Microsoft.EntityFrameworkCore;
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
    public class OrderServiceTests
    {
        private Mock<IRepository<Order>> _mockRepo;
        private OrderService _orderService;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<IRepository<Order>>();
            _orderService = new OrderService(_mockRepo.Object);
        }

        [Test]
        public async Task AddAsync_CallsRepositoryAddAsync()
        {
            // Arrange
            var order = new Order
            {
                Id = 1,
                OrderDate = DateTime.UtcNow,
                TotalAmount = 150.00m,
                CustomerId = 1,
                Status = OrderStatus.Потвърдена
            };

            // Act
            await _orderService.AddAsync(order);

            // Assert
            _mockRepo.Verify(r => r.AddAsync(order), Times.Once());
        }

        [TestCase(1)]
        [TestCase(5)]
        public async Task DeleteAsync_ExistingId_CallsRepositoryDelete(int id)
        {
            // Arrange
            var order = new Order
            {
                Id = id,
                OrderDate = DateTime.UtcNow,
                TotalAmount = 200.00m,
                CustomerId = 1,
                Status = OrderStatus.Oбработка
            };
            _mockRepo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(order);

            // Act
            await _orderService.DeleteAsync(id);

            // Assert
            _mockRepo.Verify(r => r.GetByIdAsync(id), Times.Once());
            _mockRepo.Verify(r => r.DeleteAsync(order), Times.Once());
        }

        [Test]
        public async Task Find_WithFilter_ReturnsFilteredOrders()
        {
            // Arrange
            var orders = new List<Order>
            {
                new Order { Id = 1, OrderDate = DateTime.UtcNow, TotalAmount = 100.00m, CustomerId = 1, Status = OrderStatus.Oбработка },
                new Order { Id = 2, OrderDate = DateTime.UtcNow, TotalAmount = 200.00m, CustomerId = 2, Status = OrderStatus.Потвърдена }
            };
            _mockRepo.Setup(r => r.Find(It.IsAny<Expression<Func<Order, bool>>>())).ReturnsAsync(orders.Where(o => o.TotalAmount > 150).ToList());

            Expression<Func<Order, bool>> filter = o => o.TotalAmount > 150;

            // Act
            var result = await _orderService.Find(filter);

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().Id, Is.EqualTo(2));
            Assert.That(result.First().Status, Is.EqualTo(OrderStatus.Потвърдена));
        }

        [Test]
        public async Task FindWithIncludesAsync_ReturnsOrdersWithRelatedData()
        {
            // Arrange
            var orders = new List<Order>
            {
                new Order
                {
                    Id = 1,
                    OrderDate = DateTime.UtcNow,
                    TotalAmount = 100.00m,
                    CustomerId = 1,
                    Status = OrderStatus.Oбработка,
                    OrderProducts = new List<OrderProduct> { new OrderProduct { OrderId = 1, ProductId = 1 } }
                }
            }.AsQueryable();
            var mockQueryable = orders.AsQueryable();
            _mockRepo.Setup(r => r.GetAll()).Returns(mockQueryable);

            Expression<Func<Order, bool>> filter = o => o.Id == 1;

            // Act
            var result = await _orderService.FindWithIncludesAsync(filter);

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            _mockRepo.Verify(r => r.GetAll(), Times.Once());
        }

        [Test]
        public void GetAll_ReturnsQueryable()
        {
            // Arrange
            var orders = new List<Order>
            {
                new Order { Id = 1, OrderDate = DateTime.UtcNow, TotalAmount = 100.00m, CustomerId = 1, Status = OrderStatus.Oбработка }
            }.AsQueryable();
            _mockRepo.Setup(r => r.GetAll()).Returns(orders);

            // Act
            var result = _orderService.GetAll();

            // Assert
            Assert.That(result, Is.InstanceOf<IQueryable<Order>>());
            Assert.That(result.Count(), Is.EqualTo(1));
            _mockRepo.Verify(r => r.GetAll(), Times.Once());
        }

        [Test]
        public async Task GetAllAsync_ReturnsAllOrders()
        {
            // Arrange
            var orders = new List<Order>
            {
                new Order { Id = 1, OrderDate = DateTime.UtcNow, TotalAmount = 100.00m, CustomerId = 1, Status = OrderStatus.Oбработка }
            };
            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(orders);

            // Act
            var result = await _orderService.GetAllAsync();

            // Assert
            Assert.That(result.Count(), Is.EqualTo(1));
            _mockRepo.Verify(r => r.GetAllAsync(), Times.Once());
        }

        [TestCase(1, OrderStatus.Потвърдена)]
        [TestCase(2, OrderStatus.Отказана)]
        public void GetById_ExistingId_ReturnsOrder(int id, OrderStatus status)
        {
            // Arrange
            var order = new Order
            {
                Id = id,
                OrderDate = DateTime.UtcNow,
                TotalAmount = 150.00m,
                CustomerId = 1,
                Status = status
            };
            _mockRepo.Setup(r => r.GetById(id)).Returns(order);

            // Act
            var result = _orderService.GetById(id);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(id));
            Assert.That(result.Status, Is.EqualTo(status));
            _mockRepo.Verify(r => r.GetById(id), Times.Once());
        }

        [TestCase(1, OrderStatus.Потвърдена)]
        [TestCase(2, OrderStatus.Отказана)]
        public async Task GetByIdAsync_ExistingId_ReturnsOrderWithIncludes(int id, OrderStatus status)
        {
            // Arrange
            var order = new Order
            {
                Id = id,
                OrderDate = DateTime.UtcNow,
                TotalAmount = 150.00m,
                CustomerId = 1,
                Status = status,
                OrderProducts = new List<OrderProduct> { new OrderProduct { OrderId = id, ProductId = 1 } }
            };
            var ordersQueryable = new List<Order> { order }.AsQueryable();
            _mockRepo.Setup(r => r.GetAll()).Returns(ordersQueryable);

            // Act
            var result = await _orderService.GetByIdAsync(id);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(id));
            Assert.That(result.Status, Is.EqualTo(status));
            _mockRepo.Verify(r => r.GetAll(), Times.Once());
        }

        [Test]
        public async Task GetByIdAsync_NonExistingId_ReturnsNull()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetAll()).Returns(new List<Order>().AsQueryable());

            // Act
            var result = await _orderService.GetByIdAsync(999);

            // Assert
            Assert.That(result, Is.Null);
        }

        [TestCase(1, OrderStatus.Потвърдена)]
        [TestCase(2, OrderStatus.Отказана)]
        public async Task GetOrderById_ExistingId_ReturnsOrderWithIncludes(int? id, OrderStatus status)
        {
            // Arrange
            var order = new Order
            {
                Id = id.Value,
                OrderDate = DateTime.UtcNow,
                TotalAmount = 150.00m,
                CustomerId = 1,
                Status = status,
                OrderProducts = new List<OrderProduct> { new OrderProduct { OrderId = id.Value, ProductId = 1 } }
            };
            var ordersQueryable = new List<Order> { order }.AsQueryable();
            _mockRepo.Setup(r => r.GetAll()).Returns(ordersQueryable);

            // Act
            var result = await _orderService.GetOrderById(id);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(id));
            Assert.That(result.Status, Is.EqualTo(status));
            _mockRepo.Verify(r => r.GetAll(), Times.Once());
        }

        [Test]
        public async Task UpdateAsync_CallsRepositoryUpdate()
        {
            // Arrange
            var order = new Order
            {
                Id = 1,
                OrderDate = DateTime.UtcNow,
                TotalAmount = 200.00m,
                CustomerId = 1,
                Status = OrderStatus.Потвърдена
            };

            // Act
            await _orderService.UpdateAsync(order);

            // Assert
            _mockRepo.Verify(r => r.UpdateAsync(order), Times.Once());
        }
    }
}
