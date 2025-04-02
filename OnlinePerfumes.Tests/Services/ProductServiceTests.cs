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
    public class ProductServiceTests
    {
        private Mock<IRepository<Product>> _mockRepo;
        private ProductService _productService;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<IRepository<Product>>();
            _productService = new ProductService(_mockRepo.Object);
        }

        [Test]
        public async Task AddAsync_CallsRepositoryAddAsync()
        {
            // Arrange
            var product = new Product
            {
                Id = 1,
                Name = "Test Perfume",
                Aroma = "Floral",
                Description = "A test perfume",
                Price = 99.99m
            };

            // Act
            await _productService.AddAsync(product);

            // Assert
            _mockRepo.Verify(r => r.AddAsync(product), Times.Once());
        }

        [TestCase(1)]
        [TestCase(5)]
        public async Task DeleteAsync_ExistingId_CallsRepositoryDelete(int id)
        {
            // Arrange
            var product = new Product
            {
                Id = id,
                Name = "Test Perfume",
                Aroma = "Floral",
                Description = "A test perfume",
                Price = 99.99m
            };
            _mockRepo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(product);

            // Act
            await _productService.DeleteAsync(id);

            // Assert
            _mockRepo.Verify(r => r.GetByIdAsync(id), Times.Once());
            _mockRepo.Verify(r => r.DeleteAsync(product), Times.Once());
        }

        [Test]
        public async Task Find_WithFilter_ReturnsFilteredProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Perfume A", Aroma = "Floral", Description = "Desc A", Price = 100.00m },
                new Product { Id = 2, Name = "Perfume B", Aroma = "Woody", Description = "Desc B", Price = 200.00m }
            };
            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(products);
            Expression<Func<Product, bool>> filter = p => p.Price > 150.00m;

            // Act
            var result = await _productService.Find(filter);

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().Name, Is.EqualTo("Perfume B"));
            Assert.That(result.First().Aroma, Is.EqualTo("Woody"));
        }

        [Test]
        public void GetAll_ReturnsQueryable()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Perfume A", Aroma = "Floral", Description = "Desc A", Price = 100.00m }
            }.AsQueryable();
            _mockRepo.Setup(r => r.GetAll()).Returns(products);

            // Act
            var result = _productService.GetAll();

            // Assert
            Assert.That(result, Is.InstanceOf<IQueryable<Product>>());
            Assert.That(result.Count(), Is.EqualTo(1));
            _mockRepo.Verify(r => r.GetAll(), Times.Once());
        }

        [Test]
        public async Task GetAllAsync_ReturnsAllProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Perfume A", Aroma = "Floral", Description = "Desc A", Price = 100.00m }
            };
            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(products);

            // Act
            var result = await _productService.GetAllAsync();

            // Assert
            Assert.That(result.Count(), Is.EqualTo(1));
            _mockRepo.Verify(r => r.GetAllAsync(), Times.Once());
        }

        [TestCase(1, "Perfume A", "Floral")]
        [TestCase(2, "Perfume B", "Woody")]
        public void GetById_ExistingId_ReturnsProduct(int id, string name, string aroma)
        {
            // Arrange
            var product = new Product
            {
                Id = id,
                Name = name,
                Aroma = aroma,
                Description = "Test description",
                Price = 99.99m
            };
            _mockRepo.Setup(r => r.GetById(id)).Returns(product);

            // Act
            var result = _productService.GetById(id);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(id));
            Assert.That(result.Name, Is.EqualTo(name));
            Assert.That(result.Aroma, Is.EqualTo(aroma));
            _mockRepo.Verify(r => r.GetById(id), Times.Once());
        }

        [TestCase(1, "Perfume A", "Floral")]
        [TestCase(2, "Perfume B", "Woody")]
        public async Task GetByIdAsync_ExistingId_ReturnsProduct(int id, string name, string aroma)
        {
            // Arrange
            var product = new Product
            {
                Id = id,
                Name = name,
                Aroma = aroma,
                Description = "Test description",
                Price = 99.99m
            };
            _mockRepo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(product);

            // Act
            var result = await _productService.GetByIdAsync(id);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(id));
            Assert.That(result.Name, Is.EqualTo(name));
            Assert.That(result.Aroma, Is.EqualTo(aroma));
            _mockRepo.Verify(r => r.GetByIdAsync(id), Times.Once());
        }

        [Test]
        public async Task UpdateAsync_CallsRepositoryUpdate()
        {
            // Arrange
            var product = new Product
            {
                Id = 1,
                Name = "Updated Perfume",
                Aroma = "Citrus",
                Description = "Updated description",
                Price = 150.00m
            };

            // Act
            await _productService.UpdateAsync(product);

            // Assert
            _mockRepo.Verify(r => r.UpdateAsync(product), Times.Once());
        }

        [Test]
        public async Task Find_NoMatchingProducts_ReturnsEmptyList()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Perfume A", Aroma = "Floral", Description = "Desc A", Price = 100.00m }
            };
            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(products);
            Expression<Func<Product, bool>> filter = p => p.Price > 150.00m;

            // Act
            var result = await _productService.Find(filter);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }
    }
}