using examen2_backend.Application.Interfaces;
using examen2_backend.Application.Managers;
using examen2_backend.Domain;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace examen2_unit_testing
{
    [TestFixture]
    public class ModifyCoffeeStockTest
    {
        private Mock<ICoffeeRepository> _coffeeRepositoryMock;
        private ModifyCoffeeStock _modifyCoffeeStock;

        [SetUp]
        public void Setup()
        {
            _coffeeRepositoryMock = new Mock<ICoffeeRepository>();

            var initialCoffees = new List<CoffeeModel>
            {
                new CoffeeModel { Name = "Americano",  Stock = 100, Price = 950},
                new CoffeeModel { Name = "Capuchino",  Stock = 100, Price = 1200},
                new CoffeeModel { Name = "Late",       Stock = 100, Price = 1350},
                new CoffeeModel { Name = "Mocachino",  Stock = 100, Price = 1500}
            };

            // Setup mock to return our initial coffees list
            _coffeeRepositoryMock.SetupGet(x => x.Coffees).Returns(initialCoffees);

            _modifyCoffeeStock = new ModifyCoffeeStock(_coffeeRepositoryMock.Object);
        }

        [Test]
        public void UpdateStock_ShouldReturnTrue_AndReduceStock_WhenAllRequestsAreWithinLimits()
        {
            // Arrange
            var requestedStocks = new List<int> { 10, 20, 30, 40 };

            // Act
            bool result = _modifyCoffeeStock.UpdateStock(requestedStocks);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(90, _coffeeRepositoryMock.Object.Coffees[0].Stock);
            Assert.AreEqual(80, _coffeeRepositoryMock.Object.Coffees[1].Stock);
            Assert.AreEqual(70, _coffeeRepositoryMock.Object.Coffees[2].Stock);
            Assert.AreEqual(60, _coffeeRepositoryMock.Object.Coffees[3].Stock);
        }

        [Test]
        public void UpdateStock_ShouldReturnFalse_WhenAnyRequestExceedsAvailableStock()
        {
            // Arrange
            var requestedStocks = new List<int> { 101, 0, 0, 0 };

            // Act
            bool result = _modifyCoffeeStock.UpdateStock(requestedStocks);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(100, _coffeeRepositoryMock.Object.Coffees[0].Stock);
            Assert.AreEqual(100, _coffeeRepositoryMock.Object.Coffees[1].Stock);
            Assert.AreEqual(100, _coffeeRepositoryMock.Object.Coffees[2].Stock);
            Assert.AreEqual(100, _coffeeRepositoryMock.Object.Coffees[3].Stock);
        }

        [Test]
        public void UpdateStock_ShouldReturnTrue_AndSetStockToZero_WhenRequestedEqualsAvailable()
        {
            // Arrange
            // All coffees have 100 stock, we request exactly 100 for each
            var requestedStocks = new List<int> { 100, 100, 100, 100 };

            // Act
            bool result = _modifyCoffeeStock.UpdateStock(requestedStocks);

            // Assert
            Assert.IsTrue(result);

            // Verify that each stock is now zero
            Assert.AreEqual(0, _coffeeRepositoryMock.Object.Coffees[0].Stock);
            Assert.AreEqual(0, _coffeeRepositoryMock.Object.Coffees[1].Stock);
            Assert.AreEqual(0, _coffeeRepositoryMock.Object.Coffees[2].Stock);
            Assert.AreEqual(0, _coffeeRepositoryMock.Object.Coffees[3].Stock);
        }

        [Test]
        public void UpdateStock_ShouldReturnTrue_WhenNoCoffeesRequested()
        {
            // Arrange
            var requestedStocks = new List<int>();

            // Act
            bool result = _modifyCoffeeStock.UpdateStock(requestedStocks);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(100, _coffeeRepositoryMock.Object.Coffees[0].Stock);
            Assert.AreEqual(100, _coffeeRepositoryMock.Object.Coffees[1].Stock);
            Assert.AreEqual(100, _coffeeRepositoryMock.Object.Coffees[2].Stock);
            Assert.AreEqual(100, _coffeeRepositoryMock.Object.Coffees[3].Stock);
        }
    }
}
