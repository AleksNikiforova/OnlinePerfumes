using NUnit.Framework;
using OnlinePerfumes.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace OnlinePerfumes.Tests.Models
{
    [TestFixture]
    public class ProductTests
    {
        private Product CreateValidProduct()
        {
            return new Product
            {
                Id = 1,
                Name = "Test Perfume",
                Aroma = "Floral",
                Description = "A lovely fragrance",
                Price = 99.99m,
                StockQuantity = 10,
                Countity = 1,
                CategoryId = 1
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
        public void Product_ValidModel_PassesValidation()
        {
            // Arrange
            var product = CreateValidProduct();

            // Act
            var results = ValidateModel(product);

            // Assert
            Assert.That(results.Count, Is.EqualTo(0), "Valid product should have no validation errors.");
        }

        [Test]
        public void Product_NullName_FailsValidation()
        {
            // Arrange
            var product = CreateValidProduct();
            product.Name = null;

            // Act
            var results = ValidateModel(product);

            // Assert
            Assert.That(results.Count, Is.GreaterThan(0), "Name is required.");
            Assert.That(results.Any(r => r.ErrorMessage == "Name is mandatory"), "Expected 'Name is mandatory' error.");
        }

        [Test]
        public void Product_NameExceedsLength_FailsValidation()
        {
            // Arrange
            var product = CreateValidProduct();
            product.Name = new string('A', 51); // 51 characters exceeds 50

            // Act
            var results = ValidateModel(product);

            // Assert
            Assert.That(results.Count, Is.GreaterThan(0), "Name exceeding 50 characters should fail.");
            Assert.That(results.Any(r => r.ErrorMessage == "Name must be 50 letters"), "Expected 'Name must be 50 letters' error.");
        }

        [Test]
        public void Product_NullAroma_FailsValidation()
        {
            // Arrange
            var product = CreateValidProduct();
            product.Aroma = null;

            // Act
            var results = ValidateModel(product);

            // Assert
            Assert.That(results.Count, Is.GreaterThan(0), "Aroma is required.");
            Assert.That(results.Any(r => r.ErrorMessage == "Description is mandatory"), "Expected 'Description is mandatory' error for Aroma.");
        }

        [Test]
        public void Product_AromaExceedsLength_FailsValidation()
        {
            // Arrange
            var product = CreateValidProduct();
            product.Aroma = new string('A', 101); // 101 characters exceeds 100

            // Act
            var results = ValidateModel(product);

            // Assert
            Assert.That(results.Count, Is.GreaterThan(0), "Aroma exceeding 100 characters should fail.");
            Assert.That(results.Any(r => r.ErrorMessage == "Description must be 100 words"), "Expected 'Description must be 100 words' error for Aroma.");
        }

        [Test]
        public void Product_NullDescription_FailsValidation()
        {
            // Arrange
            var product = CreateValidProduct();
            product.Description = null;

            // Act
            var results = ValidateModel(product);

            // Assert
            Assert.That(results.Count, Is.GreaterThan(0), "Description is required.");
            Assert.That(results.Any(r => r.ErrorMessage == "Description is mandatory"), "Expected 'Description is mandatory' error for Description.");
        }

        [Test]
        public void Product_PriceZero_FailsValidation()
        {
            // Arrange
            var product = CreateValidProduct();
            product.Price = 0m;

            // Act
            var results = ValidateModel(product);

            // Assert
            Assert.That(results.Count, Is.GreaterThan(0), "Price below range should fail.");
            Assert.That(results.Any(r => r.ErrorMessage == "Price must be between 0.01 and 100000"), "Expected 'Price must be between 0.01 and 100000' error.");
        }

        [Test]
        public void Product_PriceExceedsMax_FailsValidation()
        {
            // Arrange
            var product = CreateValidProduct();
            product.Price = 100001m;

            // Act
            var results = ValidateModel(product);

            // Assert
            Assert.That(results.Count, Is.GreaterThan(0), "Price above range should fail.");
            Assert.That(results.Any(r => r.ErrorMessage == "Price must be between 0.01 and 100000"), "Expected 'Price must be between 0.01 and 100000' error.");
        }

        [Test]
        public void Product_DefaultStockQuantity_IsZero()
        {
            // Arrange
            var product = new Product();

            // Act & Assert
            Assert.That(product.StockQuantity, Is.EqualTo(0), "Default StockQuantity should be 0.");
        }

        [Test]
        public void Product_DefaultCountity_IsOne()
        {
            // Arrange
            var product = new Product();

            // Act & Assert
            Assert.That(product.Countity, Is.EqualTo(1), "Default Countity should be 1.");
        }

        [Test]
        public void Product_ImagePath_CanBeNull()
        {
            // Arrange
            var product = CreateValidProduct();
            product.ImagePath = null;

            // Act
            var results = ValidateModel(product);

            // Assert
            Assert.That(results.Count, Is.EqualTo(0), "ImagePath should be nullable and not cause validation errors.");
        }

        [Test]
        public void Product_CategoryId_CanBeNull()
        {
            // Arrange
            var product = CreateValidProduct();
            product.CategoryId = null;

            // Act
            var results = ValidateModel(product);

            // Assert
            Assert.That(results.Count, Is.EqualTo(0), "CategoryId should be nullable and not cause validation errors.");
        }

        [Test]
        public void Product_Collections_AreInitialized()
        {
            // Arrange
            var product = new Product();

            // Act & Assert
            Assert.That(product.OrderProducts, Is.Not.Null, "OrderProducts should be initialized.");
            Assert.That(product.OrderProducts, Is.InstanceOf<ICollection<OrderProduct>>(), "OrderProducts should be a collection.");
            Assert.That(product.CartItems, Is.Not.Null, "CartItems should be initialized.");
            Assert.That(product.CartItems, Is.InstanceOf<ICollection<CartItem>>(), "CartItems should be a collection.");
        }

        [Test]
        public void Product_ImageFile_IsNotMapped()
        {
            // Arrange
            var product = CreateValidProduct();
            product.ImageFile = null; // Simulate file upload

            // Act
            var results = ValidateModel(product);

            // Assert
            Assert.That(results.Count, Is.EqualTo(0), "ImageFile is NotMapped and should not affect validation.");
        }
    }
}