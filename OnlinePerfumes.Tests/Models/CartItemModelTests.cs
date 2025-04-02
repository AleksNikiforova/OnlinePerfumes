using NUnit.Framework;
using OnlinePerfumes.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace OnlinePerfumes.Tests.Models
{
    [TestFixture]
    public class CartItemTests
    {
        private CartItem CreateValidCartItem()
        {
            return new CartItem
            {
                Id = 1,
                ProductId = 1,
                Quantity = 2,
                CustomerId = 1
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
        public void CartItem_ValidModel_PassesValidation()
        {
            // Arrange
            var cartItem = CreateValidCartItem();

            // Act
            var results = ValidateModel(cartItem);

            // Assert
            Assert.That(results.Count, Is.EqualTo(0), "Valid CartItem should have no validation errors.");
        }

        [Test]
        public void CartItem_DefaultValues_AreZero()
        {
            // Arrange
            var cartItem = new CartItem();

            // Act & Assert
            Assert.That(cartItem.Id, Is.EqualTo(0), "Default Id should be 0.");
            Assert.That(cartItem.ProductId, Is.EqualTo(0), "Default ProductId should be 0.");
            Assert.That(cartItem.Quantity, Is.EqualTo(0), "Default Quantity should be 0.");
            Assert.That(cartItem.CustomerId, Is.EqualTo(0), "Default CustomerId should be 0.");
        }

        [Test]
        public void CartItem_NavigationProperties_AreNullByDefault()
        {
            // Arrange
            var cartItem = new CartItem();

            // Act & Assert
            Assert.That(cartItem.Product, Is.Null, "Product navigation property should be null by default.");
            Assert.That(cartItem.Customer, Is.Null, "Customer navigation property should be null by default.");
        }

        [TestCase(1, 1, 2, 1)]
        [TestCase(2, 3, 1, 2)]
        public void CartItem_SetProperties_ValuesAreCorrectlyStored(int id, int productId, int quantity, int customerId)
        {
            // Arrange
            var cartItem = new CartItem();

            // Act
            cartItem.Id = id;
            cartItem.ProductId = productId;
            cartItem.Quantity = quantity;
            cartItem.CustomerId = customerId;

            // Assert
            Assert.That(cartItem.Id, Is.EqualTo(id), "Id should match the set value.");
            Assert.That(cartItem.ProductId, Is.EqualTo(productId), "ProductId should match the set value.");
            Assert.That(cartItem.Quantity, Is.EqualTo(quantity), "Quantity should match the set value.");
            Assert.That(cartItem.CustomerId, Is.EqualTo(customerId), "CustomerId should match the set value.");
        }

        [Test]
        public void CartItem_ZeroQuantity_PassesValidation()
        {
            // Arrange
            var cartItem = CreateValidCartItem();
            cartItem.Quantity = 0;

            // Act
            var results = ValidateModel(cartItem);

            // Assert
            Assert.That(results.Count, Is.EqualTo(0), "Zero Quantity is allowed since no validation restricts it.");
        }

        [Test]
        public void CartItem_NegativeQuantity_PassesValidation()
        {
            // Arrange
            var cartItem = CreateValidCartItem();
            cartItem.Quantity = -1;

            // Act
            var results = ValidateModel(cartItem);

            // Assert
            Assert.That(results.Count, Is.EqualTo(0), "Negative Quantity is allowed since no validation restricts it.");
        }

        [Test]
        public void CartItem_ZeroProductId_PassesValidation()
        {
            // Arrange
            var cartItem = CreateValidCartItem();
            cartItem.ProductId = 0;

            // Act
            var results = ValidateModel(cartItem);

            // Assert
            Assert.That(results.Count, Is.EqualTo(0), "Zero ProductId is allowed since no validation restricts it.");
        }

        [Test]
        public void CartItem_ZeroCustomerId_PassesValidation()
        {
            // Arrange
            var cartItem = CreateValidCartItem();
            cartItem.CustomerId = 0;

            // Act
            var results = ValidateModel(cartItem);

            // Assert
            Assert.That(results.Count, Is.EqualTo(0), "Zero CustomerId is allowed since no validation restricts it.");
        }

        [Test]
        public void CartItem_SetNavigationProperties_ValuesAreStored()
        {
            // Arrange
            var cartItem = CreateValidCartItem();
            var product = new Product { Id = 1, Name = "Perfume", Aroma = "Floral", Description = "Test", Price = 50.00m };
            var customer = new Customer { Id = 1, FirstName = "John", LastName = "Doe", Address = "123 Main St" };

            // Act
            cartItem.Product = product;
            cartItem.Customer = customer;

            // Assert
            Assert.That(cartItem.Product, Is.EqualTo(product), "Product navigation property should store the set value.");
            Assert.That(cartItem.Customer, Is.EqualTo(customer), "Customer navigation property should store the set value.");
        }

        [Test]
        public void CartItem_ZeroId_PassesValidation()
        {
            // Arrange
            var cartItem = CreateValidCartItem();
            cartItem.Id = 0;

            // Act
            var results = ValidateModel(cartItem);

            // Assert
            Assert.That(results.Count, Is.EqualTo(0), "Zero Id is allowed since no validation restricts it.");
        }
    }
}