using NUnit.Framework;
using OnlinePerfumes.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace OnlinePerfumes.Tests.Models
{
    [TestFixture]
    public class CustomerTests
    {
        private Customer CreateValidCustomer()
        {
            return new Customer
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Address = "123 Main St",
                City = "Sofia",
                UserId = "user123"
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
        public void Customer_ValidModel_PassesValidation()
        {
            // Arrange
            var customer = CreateValidCustomer();

            // Act
            var results = ValidateModel(customer);

            // Assert
            Assert.That(results.Count, Is.EqualTo(0), "Valid customer should have no validation errors.");
        }

        [Test]
        public void Customer_NullFirstName_FailsValidation()
        {
            // Arrange
            var customer = CreateValidCustomer();
            customer.FirstName = null;

            // Act
            var results = ValidateModel(customer);

            // Assert
            Assert.That(results.Count, Is.GreaterThan(0), "FirstName is required.");
            Assert.That(results.Any(r => r.ErrorMessage.Contains("required") || r.ErrorMessage.Contains("FirstName")), "Expected error related to FirstName being required.");
        }

        [Test]
        public void Customer_NullLastName_FailsValidation()
        {
            // Arrange
            var customer = CreateValidCustomer();
            customer.LastName = null;

            // Act
            var results = ValidateModel(customer);

            // Assert
            Assert.That(results.Count, Is.GreaterThan(0), "LastName is required.");
            Assert.That(results.Any(r => r.ErrorMessage.Contains("required") || r.ErrorMessage.Contains("LastName")), "Expected error related to LastName being required.");
        }

        [Test]
        public void Customer_NullAddress_FailsValidation()
        {
            // Arrange
            var customer = CreateValidCustomer();
            customer.Address = null;

            // Act
            var results = ValidateModel(customer);

            // Assert
            Assert.That(results.Count, Is.GreaterThan(0), "Address is required.");
            Assert.That(results.Any(r => r.ErrorMessage.Contains("required") || r.ErrorMessage.Contains("Address")), "Expected error related to Address being required.");
        }

        [Test]
        public void Customer_NullCity_PassesValidation()
        {
            // Arrange
            var customer = CreateValidCustomer();
            customer.City = null;

            // Act
            var results = ValidateModel(customer);

            // Assert
            Assert.That(results.Count, Is.EqualTo(0), "City is not required and should pass validation when null.");
        }

        [Test]
        public void Customer_NullUserId_PassesValidation()
        {
            // Arrange
            var customer = CreateValidCustomer();
            customer.UserId = null;

            // Act
            var results = ValidateModel(customer);

            // Assert
            Assert.That(results.Count, Is.EqualTo(0), "UserId is not required and should pass validation when null.");
        }

        [Test]
        public void Customer_Orders_IsNullByDefault()
        {
            // Arrange
            var customer = new Customer();

            // Act & Assert
            Assert.That(customer.Orders, Is.Null, "Orders should be null by default since it’s not initialized.");
        }

        [Test]
        public void Customer_CartItems_IsInitialized()
        {
            // Arrange
            var customer = new Customer();

            // Act & Assert
            Assert.That(customer.CartItems, Is.Not.Null, "CartItems should be initialized.");
            Assert.That(customer.CartItems, Is.InstanceOf<ICollection<CartItem>>(), "CartItems should be a collection.");
            Assert.That(customer.CartItems.Count, Is.EqualTo(0), "CartItems should be empty by default.");
        }

        [Test]
        public void Customer_User_IsNullByDefault()
        {
            // Arrange
            var customer = new Customer();

            // Act & Assert
            Assert.That(customer.User, Is.Null, "User navigation property should be null by default.");
        }

        [Test]
        public void Customer_SetNavigationProperties_ValuesAreStored()
        {
            // Arrange
            var customer = CreateValidCustomer();
            var orders = new List<Order> { new Order { Id = 1 } };
            var user = new IdentityUser { Id = "user123" };

            // Act
            customer.Orders = orders;
            customer.User = user;

            // Assert
            Assert.That(customer.Orders, Is.EqualTo(orders), "Orders should store the set value.");
            Assert.That(customer.User, Is.EqualTo(user), "User should store the set value.");
        }

        [TestCase("John", "Doe", "123 Main St")]
        [TestCase("Jane", "Smith", "456 Oak Ave")]
        public void Customer_SetProperties_ValuesAreCorrectlyStored(string firstName, string lastName, string address)
        {
            // Arrange
            var customer = new Customer();

            // Act
            customer.FirstName = firstName;
            customer.LastName = lastName;
            customer.Address = address;
            customer.City = "Sofia";
            customer.UserId = "user123";

            // Assert
            Assert.That(customer.FirstName, Is.EqualTo(firstName), "FirstName should match the set value.");
            Assert.That(customer.LastName, Is.EqualTo(lastName), "LastName should match the set value.");
            Assert.That(customer.Address, Is.EqualTo(address), "Address should match the set value.");
            Assert.That(customer.City, Is.EqualTo("Sofia"), "City should match the set value.");
            Assert.That(customer.UserId, Is.EqualTo("user123"), "UserId should match the set value.");
        }
    }
}