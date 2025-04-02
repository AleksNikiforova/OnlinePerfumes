using NUnit.Framework;
using OnlinePerfumes.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace OnlinePerfumes.Tests.Models
{
    [TestFixture]
    public class CategoryTests
    {
        private Category CreateValidCategory()
        {
            return new Category
            {
                Id = 1,
                Name = "Fragrance"
            };
        }

        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, context, validationResults, true);
            return validationResults;
        }

        [Test]
        public void Category_ValidModel_PassesValidation()
        {
            // Arrange
            var category = CreateValidCategory();

            // Act
            var results = ValidateModel(category);

            // Assert
            Assert.That(results.Count, Is.EqualTo(0), "Valid category should have no validation errors.");
        }

        [Test]
        public void Category_NullName_FailsValidation()
        {
            // Arrange
            var category = CreateValidCategory();
            category.Name = null;

            // Act
            var results = ValidateModel(category);

            // Assert
            Assert.That(results.Count, Is.GreaterThan(0), "Name is required.");
            Assert.That(results.Any(r => r.ErrorMessage == "Category name is mandatory"), "Expected 'Category name is mandatory' error.");
        }

        [Test]
        public void Category_NameExceedsLength_FailsValidation()
        {
            // Arrange
            var category = CreateValidCategory();
            category.Name = new string('A', 101); // 101 characters exceeds 100

            // Act
            var results = ValidateModel(category);

            // Assert
            Assert.That(results.Count, Is.GreaterThan(0), "Name exceeding 100 characters should fail.");
            Assert.That(results.Any(r => r.ErrorMessage == "Category name must be 100 words"), "Expected 'Category name must be 100 words' error.");
        }

        [Test]
        public void Category_NameAtMaxLength_PassesValidation()
        {
            // Arrange
            var category = CreateValidCategory();
            category.Name = new string('A', 100); // Exactly 100 characters

            // Act
            var results = ValidateModel(category);

            // Assert
            Assert.That(results.Count, Is.EqualTo(0), "Name at 100 characters should pass validation.");
        }

        [Test]
        public void Category_Products_IsInitialized()
        {
            // Arrange
            var category = new Category();

            // Act & Assert
            Assert.That(category.Products, Is.Not.Null, "Products should be initialized.");
            Assert.That(category.Products, Is.InstanceOf<ICollection<Product>>(), "Products should be a collection.");
            Assert.That(category.Products.Count, Is.EqualTo(0), "Products should be empty by default.");
        }

        [Test]
        public void Category_SetProducts_ValuesAreStored()
        {
            // Arrange
            var category = CreateValidCategory();
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Perfume A", Aroma = "Floral", Description = "Test", Price = 50.00m },
                new Product { Id = 2, Name = "Perfume B", Aroma = "Woody", Description = "Test", Price = 75.00m }
            };

            // Act
            category.Products = products;

            // Assert
            Assert.That(category.Products, Is.EqualTo(products), "Products should store the set value.");
            Assert.That(category.Products.Count, Is.EqualTo(2), "Products collection should contain 2 items.");
        }

        [TestCase(1, "Floral")]
        [TestCase(2, "Woody")]
        public void Category_SetProperties_ValuesAreCorrectlyStored(int id, string name)
        {
            // Arrange
            var category = new Category();

            // Act
            category.Id = id;
            category.Name = name;

            // Assert
            Assert.That(category.Id, Is.EqualTo(id), "Id should match the set value.");
            Assert.That(category.Name, Is.EqualTo(name), "Name should match the set value.");
        }

        [Test]
        public void Category_EmptyName_FailsValidation()
        {
            // Arrange
            var category = CreateValidCategory();
            category.Name = string.Empty;

            // Act
            var results = ValidateModel(category);

            // Assert
            Assert.That(results.Count, Is.GreaterThan(0), "Empty Name should fail validation.");
            Assert.That(results.Any(r => r.ErrorMessage == "Category name is mandatory"), "Expected 'Category name is mandatory' error.");
        }
    }
}