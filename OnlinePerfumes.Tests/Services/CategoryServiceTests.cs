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
    public class CategoryServiceTests
    {
        private Mock<IRepository<Category>> _mockCategoryRepo;
        private Mock<IRepository<Product>> _mockProductRepo;
        private CategoryService _categoryService;

        [SetUp]
        public void Setup()
        {
            _mockCategoryRepo = new Mock<IRepository<Category>>();
            _mockProductRepo = new Mock<IRepository<Product>>();
            _categoryService = new CategoryService(_mockCategoryRepo.Object, _mockProductRepo.Object);
        }

        [Test]
        public async Task AddAsync_CallsRepositoryAddAsync()
        {
            // Arrange
            var category = new Category { Id = 1, Name = "Fragrance" };

            // Act
            await _categoryService.AddAsync(category);

            // Assert
            _mockCategoryRepo.Verify(r => r.AddAsync(category), Times.Once());
        }

        [TestCase(1)]
        [TestCase(5)]
        public async Task DeleteAsync_ExistingId_NullifiesProductsAndDeletesCategory(int id)
        {
            // Arrange
            var category = new Category { Id = id, Name = "Floral" };
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Perfume A", CategoryId = id },
                new Product { Id = 2, Name = "Perfume B", CategoryId = id }
            };
            _mockCategoryRepo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(category);
            _mockProductRepo.Setup(r => r.Find(It.IsAny<Expression<Func<Product, bool>>>())).ReturnsAsync(products);

            // Act
            await _categoryService.DeleteAsync(id);

            // Assert
            _mockCategoryRepo.Verify(r => r.GetByIdAsync(id), Times.Once());
            _mockProductRepo.Verify(r => r.Find(It.IsAny<Expression<Func<Product, bool>>>()), Times.Once());
            _mockProductRepo.Verify(r => r.UpdateAsync(It.Is<Product>(p => p.Id == 1 && p.CategoryId == null)), Times.Once());
            _mockProductRepo.Verify(r => r.UpdateAsync(It.Is<Product>(p => p.Id == 2 && p.CategoryId == null)), Times.Once());
            _mockCategoryRepo.Verify(r => r.DeleteAsync(category), Times.Once());
        }

        [Test]
        public void DeleteAsync_NullProductRepo_ThrowsException()
        {
            // Arrange
            var categoryServiceWithNullProductRepo = new CategoryService(_mockCategoryRepo.Object, null);
            var category = new Category { Id = 1, Name = "Floral" };
            _mockCategoryRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(category);

            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(async () => await categoryServiceWithNullProductRepo.DeleteAsync(1));
            Assert.That(ex.Message, Is.EqualTo("_productrepo е NULL! Увери се, че е правилно инжектиран в конструктора."));
        }

        [Test]
        public async Task Find_WithFilter_ReturnsFilteredCategories()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Floral" },
                new Category { Id = 2, Name = "Woody" }
            };
            _mockCategoryRepo.Setup(r => r.Find(It.IsAny<Expression<Func<Category, bool>>>())).ReturnsAsync(categories.Where(c => c.Name == "Woody").ToList());

            Expression<Func<Category, bool>> filter = c => c.Name == "Woody";

            // Act
            var result = await _categoryService.Find(filter);

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().Id, Is.EqualTo(2));
            Assert.That(result.First().Name, Is.EqualTo("Woody"));
        }

        [Test]
        public void GetAll_ReturnsQueryable()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Floral" }
            }.AsQueryable();
            _mockCategoryRepo.Setup(r => r.GetAll()).Returns(categories);

            // Act
            var result = _categoryService.GetAll();

            // Assert
            Assert.That(result, Is.InstanceOf<IQueryable<Category>>());
            Assert.That(result.Count(), Is.EqualTo(1));
            _mockCategoryRepo.Verify(r => r.GetAll(), Times.Once());
        }

        [Test]
        public async Task GetAllAsync_ReturnsAllCategories()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Floral" }
            };
            _mockCategoryRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(categories);

            // Act
            var result = await _categoryService.GetAllAsync();

            // Assert
            Assert.That(result.Count(), Is.EqualTo(1));
            _mockCategoryRepo.Verify(r => r.GetAllAsync(), Times.Once());
        }

        [TestCase(1, "Floral")]
        [TestCase(2, "Woody")]
        public void GetById_ExistingId_ReturnsCategory(int id, string name)
        {
            // Arrange
            var category = new Category { Id = id, Name = name };
            _mockCategoryRepo.Setup(r => r.GetById(id)).Returns(category);

            // Act
            var result = _categoryService.GetById(id);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(id));
            Assert.That(result.Name, Is.EqualTo(name));
            _mockCategoryRepo.Verify(r => r.GetById(id), Times.Once());
        }

        [TestCase(1, "Floral")]
        [TestCase(2, "Woody")]
        public async Task GetByIdAsync_NonNullable_ExistingId_ReturnsCategory(int id, string name)
        {
            // Arrange
            var category = new Category { Id = id, Name = name };
            _mockCategoryRepo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(category);

            // Act
            var result = await _categoryService.GetByIdAsync(id);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(id));
            Assert.That(result.Name, Is.EqualTo(name));
            _mockCategoryRepo.Verify(r => r.GetByIdAsync(id), Times.Once());
        }

        [TestCase(1, "Floral")]
        [TestCase(2, "Woody")]
        public async Task GetByIdAsync_Nullable_ExistingId_ReturnsCategory(int id, string name)
        {
            // Arrange
            var category = new Category { Id = id, Name = name };
            _mockCategoryRepo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(category);

            // Act
            var result = await _categoryService.GetByIdAsync((int?)id);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(id));
            Assert.That(result.Name, Is.EqualTo(name));
            _mockCategoryRepo.Verify(r => r.GetByIdAsync(id), Times.Once());
        }

        [Test]
        public async Task GetByIdAsync_Nullable_NullId_ReturnsDefault()
        {
            // Arrange
            var defaultCategory = new Category { Id = 0, Name = null };
            _mockCategoryRepo.Setup(r => r.GetByIdAsync(0)).ReturnsAsync(defaultCategory);

            // Act
            var result = await _categoryService.GetByIdAsync(null);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(0));
            _mockCategoryRepo.Verify(r => r.GetByIdAsync(0), Times.Once());
        }

        [TestCase("Floral")]
        [TestCase("Woody")]
        public async Task GetCategoryByName_ExistingName_ReturnsCategory(string name)
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Floral" },
                new Category { Id = 2, Name = "Woody" }
            };
            _mockCategoryRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(categories);

            // Act
            var result = await _categoryService.GetCategoryByName(name);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo(name));
            _mockCategoryRepo.Verify(r => r.GetAllAsync(), Times.Once());
        }

        [Test]
        public async Task GetCategoryByName_NonExistingName_ReturnsNull()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Floral" }
            };
            _mockCategoryRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(categories);

            // Act
            var result = await _categoryService.GetCategoryByName("Woody");

            // Assert
            Assert.That(result, Is.Null);
            _mockCategoryRepo.Verify(r => r.GetAllAsync(), Times.Once());
        }

        [Test]
        public async Task UpdateAsync_CallsRepositoryUpdate()
        {
            // Arrange
            var category = new Category { Id = 1, Name = "Updated Fragrance" };

            // Act
            await _categoryService.UpdateAsync(category);

            // Assert
            _mockCategoryRepo.Verify(r => r.UpdateAsync(category), Times.Once());
        }
    }
}