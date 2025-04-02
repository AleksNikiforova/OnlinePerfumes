using NUnit.Framework;
using OnlinePerfumes.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlinePerfumes.Tests.Models
{
    [TestFixture]
    public class OrderProductTests
    {
        private OrderProduct CreateValidOrderProduct()
        {
            return new OrderProduct
            {
                OrderId = 1,
                ProductId = 1,
                Quantity = 2,
                Price = 50.00m
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
        public void OrderProduct_ValidModel_PassesValidation()
        {
            // Arrange
            var orderProduct = CreateValidOrderProduct();

            // Act
            var results = ValidateModel(orderProduct);

            // Assert
            Assert.That(results.Count, Is.EqualTo(0), "Valid OrderProduct should have no validation errors.");
        }

        [Test]
        public void OrderProduct_DefaultValues_AreZero()
        {
            // Arrange
            var orderProduct = new OrderProduct();

            // Act & Assert
            Assert.That(orderProduct.OrderId, Is.EqualTo(0), "Default OrderId should be 0.");
            Assert.That(orderProduct.ProductId, Is.EqualTo(0), "Default ProductId should be 0.");
            Assert.That(orderProduct.Quantity, Is.EqualTo(0), "Default Quantity should be 0.");
            Assert.That(orderProduct.Price, Is.EqualTo(0m), "Default Price should be 0.");
        }

        [Test]
        public void OrderProduct_NavigationProperties_AreNullByDefault()
        {
            // Arrange
            var orderProduct = new OrderProduct();

            // Act & Assert
            Assert.That(orderProduct.Order, Is.Null, "Order navigation property should be null by default.");
            Assert.That(orderProduct.Product, Is.Null, "Product navigation property should be null by default.");
        }

        [TestCase(1, 1, 2, 50.00)]
        [TestCase(2, 3, 1, 25.50)]
        public void OrderProduct_SetProperties_ValuesAreCorrectlyStored(int orderId, int productId, int quantity, decimal price)
        {
            // Arrange
            var orderProduct = new OrderProduct();

            // Act
            orderProduct.OrderId = orderId;
            orderProduct.ProductId = productId;
            orderProduct.Quantity = quantity;
            orderProduct.Price = (decimal)price;

            // Assert
            Assert.That(orderProduct.OrderId, Is.EqualTo(orderId), "OrderId should match the set value.");
            Assert.That(orderProduct.ProductId, Is.EqualTo(productId), "ProductId should match the set value.");
            Assert.That(orderProduct.Quantity, Is.EqualTo(quantity), "Quantity should match the set value.");
            Assert.That(orderProduct.Price, Is.EqualTo(price), "Price should match the set value.");
        }

        [Test]
        public void OrderProduct_ZeroQuantity_PassesValidation()
        {
            // Arrange
            var orderProduct = CreateValidOrderProduct();
            orderProduct.Quantity = 0;

            // Act
            var results = ValidateModel(orderProduct);

            // Assert
            Assert.That(results.Count, Is.EqualTo(0), "Zero Quantity is allowed since no validation restricts it.");
        }

        [Test]
        public void OrderProduct_NegativeQuantity_PassesValidation()
        {
            // Arrange
            var orderProduct = CreateValidOrderProduct();
            orderProduct.Quantity = -1;

            // Act
            var results = ValidateModel(orderProduct);

            // Assert
            Assert.That(results.Count, Is.EqualTo(0), "Negative Quantity is allowed since no validation restricts it.");
        }

        [Test]
        public void OrderProduct_ZeroPrice_PassesValidation()
        {
            // Arrange
            var orderProduct = CreateValidOrderProduct();
            orderProduct.Price = 0m;

            // Act
            var results = ValidateModel(orderProduct);

            // Assert
            Assert.That(results.Count, Is.EqualTo(0), "Zero Price is allowed since no validation restricts it.");
        }

        [Test]
        public void OrderProduct_NegativePrice_PassesValidation()
        {
            // Arrange
            var orderProduct = CreateValidOrderProduct();
            orderProduct.Price = -10.00m;

            // Act
            var results = ValidateModel(orderProduct);

            // Assert
            Assert.That(results.Count, Is.EqualTo(0), "Negative Price is allowed since no validation restricts it.");
        }

        [Test]
        public void OrderProduct_SetNavigationProperties_ValuesAreStored()
        {
            // Arrange
            var orderProduct = CreateValidOrderProduct();
            var order = new Order { Id = 1 };
            var product = new Product { Id = 1 };

            // Act
            orderProduct.Order = order;
            orderProduct.Product = product;

            // Assert
            Assert.That(orderProduct.Order, Is.EqualTo(order), "Order navigation property should store the set value.");
            Assert.That(orderProduct.Product, Is.EqualTo(product), "Product navigation property should store the set value.");
        }

        [Test]
        public void OrderProduct_ZeroOrderIdAndProductId_PassesValidation()
        {
            // Arrange
            var orderProduct = CreateValidOrderProduct();
            orderProduct.OrderId = 0;
            orderProduct.ProductId = 0;

            // Act
            var results = ValidateModel(orderProduct);

            // Assert
            Assert.That(results.Count, Is.EqualTo(0), "Zero OrderId and ProductId are allowed since no validation restricts them.");
        }
    }
}