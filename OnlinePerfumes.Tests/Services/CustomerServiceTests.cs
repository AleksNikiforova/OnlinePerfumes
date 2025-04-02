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
using Microsoft.AspNetCore.Identity;

namespace OnlinePerfumes.Tests.Services
{
    [TestFixture]
    public class CustomerServiceTests
    {
        private Mock<IRepository<Customer>> _mockRepo;
        private CustomerService _customerService;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<IRepository<Customer>>();
            _customerService = new CustomerService(_mockRepo.Object);
        }

        [Test]
        public async Task AddAsync_ValidCustomer_CallsRepositoryAddAsync()
        {
            // Arrange
            var customer = new Customer
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Address = "123 Main St",
                City = "Sofia",
                UserId = "user123"
            };

            // Act
            await _customerService.AddAsync(customer);

            // Assert
            _mockRepo.Verify(r => r.AddAsync(customer), Times.Once());
        }

        [Test]
        public void AddAsync_NullCustomer_ThrowsArgumentNullException()
        {
            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentNullException>(async () => await _customerService.AddAsync(null));
            Assert.That(ex.ParamName, Is.EqualTo("customer"));
        }

        [TestCase(1)]
        [TestCase(5)]
        public async Task DeleteAsync_ExistingId_CallsRepositoryDelete(int id)
        {
            // Arrange
            var customer = new Customer
            {
                Id = id,
                FirstName = "Jane",
                LastName = "Smith",
                Address = "456 Oak St",
                City = "Plovdiv",
                UserId = "user456"
            };
            _mockRepo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(customer);

            // Act
            await _customerService.DeleteAsync(id);

            // Assert
            _mockRepo.Verify(r => r.GetByIdAsync(id), Times.Once());
            _mockRepo.Verify(r => r.DeleteAsync(customer), Times.Once());
        }

        [Test]
        public async Task Find_WithFilter_ReturnsFilteredCustomers()
        {
            // Arrange
            var customers = new List<Customer>
            {
                new Customer { Id = 1, FirstName = "John", LastName = "Doe", Address = "123 Main St", City = "Sofia", UserId = "user123" },
                new Customer { Id = 2, FirstName = "Jane", LastName = "Smith", Address = "456 Oak St", City = "Plovdiv", UserId = "user456" }
            };
            _mockRepo.Setup(r => r.Find(It.IsAny<Expression<Func<Customer, bool>>>())).ReturnsAsync(customers.Where(c => c.City == "Plovdiv").ToList());

            Expression<Func<Customer, bool>> filter = c => c.City == "Plovdiv";

            // Act
            var result = await _customerService.Find(filter);

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().Id, Is.EqualTo(2));
            Assert.That(result.First().City, Is.EqualTo("Plovdiv"));
        }

        [Test]
        public void GetAll_ReturnsQueryable()
        {
            // Arrange
            var customers = new List<Customer>
            {
                new Customer { Id = 1, FirstName = "John", LastName = "Doe", Address = "123 Main St", City = "Sofia", UserId = "user123" }
            }.AsQueryable();
            _mockRepo.Setup(r => r.GetAll()).Returns(customers);

            // Act
            var result = _customerService.GetAll();

            // Assert
            Assert.That(result, Is.InstanceOf<IQueryable<Customer>>());
            Assert.That(result.Count(), Is.EqualTo(1));
            _mockRepo.Verify(r => r.GetAll(), Times.Once());
        }

        [Test]
        public async Task GetAllAsync_ReturnsAllCustomers()
        {
            // Arrange
            var customers = new List<Customer>
            {
                new Customer { Id = 1, FirstName = "John", LastName = "Doe", Address = "123 Main St", City = "Sofia", UserId = "user123" }
            };
            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(customers);

            // Act
            var result = await _customerService.GetAllAsync();

            // Assert
            Assert.That(result.Count(), Is.EqualTo(1));
            _mockRepo.Verify(r => r.GetAllAsync(), Times.Once());
        }

        [TestCase(1, "John", "Doe")]
        [TestCase(2, "Jane", "Smith")]
        public void GetById_ExistingId_ReturnsCustomer(int id, string firstName, string lastName)
        {
            // Arrange
            var customer = new Customer
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                Address = "123 Main St",
                City = "Sofia",
                UserId = "user123"
            };
            _mockRepo.Setup(r => r.GetById(id)).Returns(customer);

            // Act
            var result = _customerService.GetById(id);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(id));
            Assert.That(result.FirstName, Is.EqualTo(firstName));
            Assert.That(result.LastName, Is.EqualTo(lastName));
            _mockRepo.Verify(r => r.GetById(id), Times.Once());
        }

        [TestCase(1, "John", "Doe")]
        [TestCase(2, "Jane", "Smith")]
        public async Task GetByIdAsync_ExistingId_ReturnsCustomer(int id, string firstName, string lastName)
        {
            // Arrange
            var customer = new Customer
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                Address = "123 Main St",
                City = "Sofia",
                UserId = "user123"
            };
            _mockRepo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(customer);

            // Act
            var result = await _customerService.GetByIdAsync(id);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(id));
            Assert.That(result.FirstName, Is.EqualTo(firstName));
            Assert.That(result.LastName, Is.EqualTo(lastName));
            _mockRepo.Verify(r => r.GetByIdAsync(id), Times.Once());
        }

        [Test]
        public async Task GetByIdAsync_NonExistingId_ReturnsNull()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Customer)null);

            // Act
            var result = await _customerService.GetByIdAsync(999);

            // Assert
            Assert.That(result, Is.Null);
            _mockRepo.Verify(r => r.GetByIdAsync(999), Times.Once());
        }

        [Test]
        public async Task UpdateAsync_CallsRepositoryUpdate()
        {
            // Arrange
            var customer = new Customer
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Address = "456 New St",
                City = "Varna",
                UserId = "user123"
            };

            // Act
            await _customerService.UpdateAsync(customer);

            // Assert
            _mockRepo.Verify(r => r.UpdateAsync(customer), Times.Once());
        }

        [Test]
        public async Task Find_NoMatchingCustomers_ReturnsEmptyList()
        {
            // Arrange
            var customers = new List<Customer>
            {
                new Customer { Id = 1, FirstName = "John", LastName = "Doe", Address = "123 Main St", City = "Sofia", UserId = "user123" }
            };
            _mockRepo.Setup(r => r.Find(It.IsAny<Expression<Func<Customer, bool>>>())).ReturnsAsync(new List<Customer>());

            Expression<Func<Customer, bool>> filter = c => c.City == "Varna";

            // Act
            var result = await _customerService.Find(filter);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }
    }
}