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
    public class ChangeManagementTest
    {
        private Mock<IChangeRepository> _changeRepositoryMock;
        private ChangeManagement _changeManagement;

        [SetUp]
        public void Setup()
        {
            _changeRepositoryMock = new Mock<IChangeRepository>();

            // Setup initial data
            var initialChangeMoney = new List<MoneyModel>
            {
                new MoneyModel { Money = 1000, Quantity = 10, Type = "billete" },
                new MoneyModel { Money = 500,  Quantity = 10, Type = "moneda" },
                new MoneyModel { Money = 100,  Quantity = 10, Type = "moneda" },
                new MoneyModel { Money = 50,   Quantity = 10, Type = "moneda" },
                new MoneyModel { Money = 25,   Quantity = 10, Type = "moneda" }
            };

            var initialUsedChange = new List<MoneyModel>
            {
                new MoneyModel { Money = 1000, Quantity = 0, Type = "billete" },
                new MoneyModel { Money = 500,  Quantity = 0, Type = "moneda" },
                new MoneyModel { Money = 100,  Quantity = 0, Type = "moneda" },
                new MoneyModel { Money = 50,   Quantity = 0, Type = "moneda" },
                new MoneyModel { Money = 25,   Quantity = 0, Type = "moneda" }
            };

            _changeRepositoryMock.SetupGet(x => x.changeMoney).Returns(initialChangeMoney);
            _changeRepositoryMock.SetupGet(x => x.usedChangeMoney).Returns(initialUsedChange);

            _changeManagement = new ChangeManagement(_changeRepositoryMock.Object);
        }

        [Test]
        public void CanGiveChange_ShouldReturnTrue_WhenExactChangeCanBeGiven()
        {
            // Arrange
            int requestedMoney = 3000;
            var newMoneyQuantities = new List<int> { 0, 0, 0, 0, 0 };

            // Act
            bool result = _changeManagement.CanGiveChange(requestedMoney, newMoneyQuantities);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(3, _changeRepositoryMock.Object.usedChangeMoney.First(x => x.Money == 1000).Quantity);
            Assert.AreEqual(7, _changeRepositoryMock.Object.changeMoney.First(x => x.Money == 1000).Quantity);
        }

        [Test]
        public void CanGiveChange_ShouldReturnFalse_WhenExactChangeCannotBeGiven()
        {
            // Arrange
            int requestedMoney = 1;
            var newMoneyQuantities = new List<int> { 0, 0, 0, 0, 0 };

            // Act
            bool result = _changeManagement.CanGiveChange(requestedMoney, newMoneyQuantities);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void CanGiveChange_ShouldUseNewMoneyQuantities_WhenAdded()
        {
            // Arrange
            int requestedMoney = 100;
            var newMoneyQuantities = new List<int> { 0, 0, 1, 0, 0 };

            // Act
            bool result = _changeManagement.CanGiveChange(requestedMoney, newMoneyQuantities);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, _changeRepositoryMock.Object.usedChangeMoney.First(x => x.Money == 100).Quantity);
            Assert.AreEqual(10, _changeRepositoryMock.Object.changeMoney.First(x => x.Money == 100).Quantity);
        }

        [Test]
        public void GiveChange_ShouldReturnUsedChange_WhenChangeIsPossible()
        {
            // Arrange
            int requestedMoney = 2000;
            var newMoneyQuantities = new List<int> { 0, 0, 0, 0, 0 };

            // Act
            var result = _changeManagement.GiveChange(requestedMoney, newMoneyQuantities);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.First(x => x.Money == 1000).Quantity);
        }

        [Test]
        public void GiveChange_ShouldReturnNull_WhenChangeIsNotPossible()
        {
            // Arrange
            int requestedMoney = 1;
            var newMoneyQuantities = new List<int> { 0, 0, 0, 0, 0 };

            // Act
            var result = _changeManagement.GiveChange(requestedMoney, newMoneyQuantities);

            // Assert
            Assert.IsNull(result);
        }
    }
}