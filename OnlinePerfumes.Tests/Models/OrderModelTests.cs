using NUnit.Framework;
using OnlinePerfumes.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace OnlinePerfumes.Tests.Models
{
    [TestFixture]
    public class OrderTests
    {
        private Order CreateValidOrder()
        {
            return new Order
            {
                Id = 1,
                OrderDate = DateTime.UtcNow,
                TotalAmount = 150.00m,
                CustomerId = 1,
                Status = OrderStatus.Потвърдена
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
        public void Order_ValidModel_PassesValidation()
        {
            // Arrange
            var order = CreateValidOrder();

            // Act
            var results = ValidateModel(order);

            // Assert
            Assert.That(results.Count, Is.EqualTo(0), "Valid order should have no validation errors.");
        }

        [Test]
        public void Order_DefaultOrderDate_IsUtcNow()
        {
            // Arrange
            var order = new Order();

            // Act & Assert
            Assert.That(order.OrderDate, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)), "Default OrderDate should be close to UTC now.");
        }

        [Test]
        public void Order_NullOrderDate_FailsValidation()
        {
            // Arrange
            var order = CreateValidOrder();
            order.OrderDate = default(DateTime); // Null equivalent for DateTime

            // Act
            var results = ValidateModel(order);

            // Assert
            Assert.That(results.Count, Is.GreaterThan(0), "OrderDate is required.");
            Assert.That(results.Any(r => r.ErrorMessage.Contains("required") || r.ErrorMessage.Contains("OrderDate")), "Expected error related to OrderDate being required.");
        }

        [Test]
        public void Order_ZeroTotalAmount_FailsValidation()
        {
            // Arrange
            var order = CreateValidOrder();
            order.TotalAmount = 0m;

            // Act
            var results = ValidateModel(order);

            // Assert
            Assert.That(results.Count, Is.GreaterThan(0), "TotalAmount is required and cannot be zero.");
            Assert.That(results.Any(r => r.ErrorMessage.Contains("required") || r.ErrorMessage.Contains("TotalAmount")), "Expected error related to TotalAmount being required.");
        }

        [Test]
        public void Order_NegativeTotalAmount_PassesValidation()
        {
            // Arrange
            var order = CreateValidOrder();
            order.TotalAmount = -50.00m; // No range restriction in model

            // Act
            var results = ValidateModel(order);

            // Assert
            Assert.That(results.Count, Is.EqualTo(0), "Negative TotalAmount is allowed since no range is specified.");
        }

        [Test]
        public void Order_OrderProducts_IsInitialized()
        {
            // Arrange
            var order = new Order();

            // Act & Assert
            Assert.That(order.OrderProducts, Is.Not.Null, "OrderProducts should be initialized.");
            Assert.That(order.OrderProducts, Is.InstanceOf<ICollection<OrderProduct>>(), "OrderProducts should be a collection.");
            Assert.That(order.OrderProducts.Count, Is.EqualTo(0), "OrderProducts should be empty by default.");
        }

        [TestCase(OrderStatus.Oбработка, 0)]
        [TestCase(OrderStatus.Потвърдена, 1)]
        [TestCase(OrderStatus.Отказана, 2)]
        public void OrderStatusId_Get_ReturnsCorrectValue(OrderStatus status, int expectedValue)
        {
            // Arrange
            var order = CreateValidOrder();
            order.Status = status;

            // Act
            var result = order.OrderStatusId;

            // Assert
            Assert.That(result, Is.EqualTo(expectedValue), $"OrderStatusId should return {expectedValue} for {status}.");
        }

        [TestCase(0, OrderStatus.Oбработка)]
        [TestCase(1, OrderStatus.Потвърдена)]
        [TestCase(2, OrderStatus.Отказана)]
        public void OrderStatusId_Set_UpdatesStatusCorrectly(int value, OrderStatus expectedStatus)
        {
            // Arrange
            var order = CreateValidOrder();

            // Act
            order.OrderStatusId = value;

            // Assert
            Assert.That(order.Status, Is.EqualTo(expectedStatus), $"Setting OrderStatusId to {value} should update Status to {expectedStatus}.");
        }

        [Test]
        public void Order_InvalidStatusValue_DoesNotThrow()
        {
            // Arrange
            var order = CreateValidOrder();

            // Act & Assert
            Assert.DoesNotThrow(() => order.OrderStatusId = 999, "Setting an invalid enum value should not throw an exception.");
            Assert.That(order.Status, Is.EqualTo((OrderStatus)999), "Status should accept the raw value even if not defined in enum.");
        }

        [Test]
        public void Order_EnumDataType_ValidStatus_PassesValidation()
        {
            // Arrange
            var order = CreateValidOrder();
            order.Status = OrderStatus.Потвърдена;

            // Act
            var results = ValidateModel(order);

            // Assert
            Assert.That(results.Count, Is.EqualTo(0), "Valid OrderStatus should pass validation.");
        }

        [Test]
        public void Order_CustomerId_DefaultZero_PassesValidation()
        {
            // Arrange
            var order = CreateValidOrder();
            order.CustomerId = 0; // No [Required] on CustomerId

            // Act
            var results = ValidateModel(order);

            // Assert
            Assert.That(results.Count, Is.EqualTo(0), "CustomerId can be 0 since it’s not required.");
        }
    }
}