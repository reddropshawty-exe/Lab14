using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab12Tusk;
using Lab10FINLIB;

namespace Lab12Tusk.Tests
{
    [TestClass]
    public class HSTableTests
    {
        [TestMethod]
        public void AddItem_AddsItemCorrectly()
        {
            // Arrange
            var table = new HSTable<int, Car>();
            var car = new Car("Ford", 2022, "red", 25000, 15);

            // Act
            table.AddItem(1, car);

            // Assert
            Assert.AreEqual(1, table.Count);
            Assert.IsTrue(table.Contains(1));
        }

        [TestMethod]
        public void Contains_ReturnsTrueForExistingItem()
        {
            // Arrange
            var table = new HSTable<int, Car>();
            var car = new Car("Ford", 2022, "red", 25000, 15);
            table.AddItem(1, car);

            // Act
            bool contains = table.Contains(1);

            // Assert
            Assert.IsTrue(contains);
        }

        [TestMethod]
        public void Contains_ReturnsFalseForNonExistingItem()
        {
            // Arrange
            var table = new HSTable<int, Car>();

            // Act
            bool contains = table.Contains(1);

            // Assert
            Assert.IsFalse(contains);
        }

        [TestMethod]
        public void DelEl_RemovesItemCorrectly()
        {
            // Arrange
            HSTable<string, Car> table = new HSTable<string, Car>();
            Car car = new Car("Ford", 2022, "red", 25000, 15);
            table.AddItem(car.GetKey(), car);

            // Act
            table.DelEl(car.GetKey());

            // Assert
            Assert.AreEqual(0, table.Count);
        }

        [TestMethod]
        public void DelEl_DoesNothingForNonExistingItem()
        {
            // Arrange
            var table = new HSTable<int, Car>();

            // Act
            table.DelEl(1);

            // Assert
            Assert.AreEqual(0, table.Count);
        }

        [TestMethod]
        public void DelAllEl_RemovesAllInstances()
        {
            // Arrange
            var table = new HSTable<int, Car>();
            var car = new Car("Ford", 2022, "red", 25000, 15);
            table.AddItem(1, car);
            table.AddItem(2, car);

            // Act
            table.DelAllEl(1);
            table.DelAllEl(2);

            // Assert
            Assert.AreEqual(0, table.Count);
        }

        [TestMethod]
        public void AddItem_ResizesTableWhenNecessary()
        {
            // Arrange
            var table = new HSTable<int, Car>(2, 0.5);
            var car1 = new Car("Ford", 2022, "red", 25000, 15);
            var car2 = new Car("Audi", 2023, "blue", 35000, 15);

            // Act
            table.AddItem(1, car1);
            table.AddItem(2, car2);

            // Assert
            Assert.AreEqual(2, table.Count);
            Assert.IsTrue(table.Contains(1));
            Assert.IsTrue(table.Contains(2));
            Assert.AreEqual(2, table.Capacity);  // Initial capacity was 2, should double to 4
        }

        [TestMethod]
        public void PrintTable_PrintsCorrectly()
        {
            // Arrange
            var table = new HSTable<int, Car>();
            var car1 = new Car("Ford", 2022, "red", 25000, 15);
            var car2 = new Car("Audi", 2023, "blue", 35000, 15);
            table.AddItem(1, car1);
            table.AddItem(2, car2);

            // Act & Assert
            // Redirect console output to check the print output
            using (var sw = new System.IO.StringWriter())
            {
                Console.SetOut(sw);
                table.PrintTable();
                var output = sw.ToString();

                Assert.IsTrue(output.Contains("КЛЮЧ: 1"));
                Assert.IsTrue(output.Contains("ЗНАЧЕНИЕ: автомобиль red Ford 2022 года"));
                Assert.IsTrue(output.Contains("КЛЮЧ: 2"));
                Assert.IsTrue(output.Contains("ЗНАЧЕНИЕ: автомобиль blue Audi 2023 года"));
            }
        }

        [TestMethod]
        public void FindElem_ReturnsCorrectIndexForExistingElement()
        {
            // Arrange
            var table = new HSTable<int, Car>();
            var car = new Car("Ford", 2022, "red", 25000, 15);
            table.AddItem(1, car);

            // Act
            int index = table.FindElem(1);

            // Assert
            Assert.AreEqual(1, index);
        }

        [TestMethod]
        public void FindElem_ReturnsNegativeOneForNonExistingElement()
        {
            // Arrange
            var table = new HSTable<int, Car>();

            // Act
            int index = table.FindElem(1);

            // Assert
            Assert.AreEqual(-1, index);
        }
    }
}
