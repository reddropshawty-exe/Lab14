using System;
using System.Collections.Generic;
using System.Linq;
using Lab14_;
using Lab10FINLIB;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lab14_Tests
{
    [TestClass]
    public class CarFabricTests
    {
        private static List<Showroom> GetSampleShowrooms()
        {
            return new List<Showroom>
            {
                new Showroom("Bugatti"),
                new Showroom("Lamborgini"),
                new Showroom("Audi"),
                new Showroom("Skoda")
            };
        }

        [TestMethod]
        public void TestPrintFab()
        {
            // Arrange
            var fabric = new CarFabric(1);

            // Act
            var result = fabric.PrintFab();

            // Assert
            Assert.IsTrue(result.Contains("Все машины завода:"));
        }

        [TestMethod]
        public void TestFindPremiumLinq()
        {
            // Arrange
            var fabric = new CarFabric(1);

            // Act
            var result = fabric.FindPremiumLinq();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestFindPremiumUsingExtensions()
        {
            // Arrange
            var fabric = new CarFabric(1);

            // Act
            var result = fabric.FindPremiumUsingExtensions();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestFindExclusiveUsingExtensions()
        {
            // Arrange
            var fabric1 = new CarFabric(1);
            var fabric2 = new CarFabric(1);

            // Act
            var result = fabric1.FindExclusiveUsingExtensions(fabric2);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestFindExclusiveLinq()
        {
            // Arrange
            var fabric1 = new CarFabric(1);
            var fabric2 = new CarFabric(1);

            // Act
            var result = fabric1.FindExclusiveLinq(fabric2);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestGetAverageTruckPriceUsingExtensions()
        {
            // Arrange
            var fabric = new CarFabric(1);

            // Act
            var result = fabric.GetAverageTruckPriceUsingExtensions();

            // Assert
            Assert.IsTrue(result.Contains("Cредняя цена грузовика"));
        }

        [TestMethod]
        public void TestGetAverageTruckPriceLinq()
        {
            // Arrange
            var fabric = new CarFabric(1);

            // Act
            var result = fabric.GetAverageTruckPriceLinq();

            // Assert
            Assert.IsTrue(result.Contains("Cредняя цена грузовика"));
        }

        [TestMethod]
        public void TestGetCodeLinq()
        {
            // Arrange
            var fabric = new CarFabric(1);

            // Act
            var result = fabric.GetCodeLinq();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestGetCodeUsingExtensions()
        {
            // Arrange
            var fabric = new CarFabric(1);

            // Act
            var result = fabric.GetCodeUsingExtensions();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestGetShowroomsLinq()
        {
            // Arrange
            var fabric = new CarFabric(1);
            var company = GetSampleShowrooms();

            // Act
            var result = fabric.GetShowroomsLinq(company);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestGetShowroomsUsingExtensions()
        {
            // Arrange
            var fabric = new CarFabric(1);
            var company = GetSampleShowrooms();

            // Act
            var result = fabric.GetShowroomsUsingExtensions(company);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestGroupByBrand()
        {
            // Arrange
            var fabric = new CarFabric(1);

            // Act
            var result = fabric.GroupByBrand();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestGroupByBrandLinq()
        {
            // Arrange
            var fabric = new CarFabric(1);

            // Act
            var result = fabric.GroupByBrandLinq();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestCarFabricInitialization()
        {
            // Arrange
            var fabric = new CarFabric(3);

            // Act
            var fabricLength = fabric.fabric.Count;

            // Assert
            Assert.AreEqual(3, fabricLength);
        }

        [TestMethod]
        public void TestCreateRandomCar()
        {
            // Arrange
            var fabric = new CarFabric(1);
            var carTypes = new Type[] { typeof(PassengerCar), typeof(SUV), typeof(Truck), typeof(Car) };

            // Act
            var car = fabric.GetType().GetMethod("CreateRandomCar", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(fabric, null);

            // Assert
            Assert.IsTrue(carTypes.Contains(car.GetType()));
        }

        [TestMethod]
        public void TestGetAverageTruckPriceWithNoTrucks()
        {
            // Arrange
            var fabric = new CarFabric(1);

            // Act & Assert
            try
            {
                var result = fabric.GetAverageTruckPriceLinq();
                Assert.Fail("Expected an InvalidOperationException to be thrown.");
            }
            catch (InvalidOperationException)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void TestFindPremiumLinqNoPremiumCars()
        {
            // Arrange
            var fabric = new CarFabric(1);
            foreach (var workshop in fabric.fabric)
            {
                foreach (var car in workshop)
                {
                    car.Brand = "NotPremiumBrand";
                }
            }

            // Act
            var result = fabric.FindPremiumLinq();

            // Assert
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void TestFindExclusiveWithSameCars()
        {
            // Arrange
            var fabric1 = new CarFabric(1);
            var fabric2 = new CarFabric(1);
            fabric2.fabric = fabric1.fabric;

            // Act
            var result = fabric1.FindExclusiveUsingExtensions(fabric2);

            // Assert
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void TestGetShowroomsUsingExtensionsNoMatchingShowrooms()
        {
            // Arrange
            var fabric = new CarFabric(1);
            var company = new List<Showroom> { new Showroom("NotExistingBrand") };

            // Act
            var result = fabric.GetShowroomsUsingExtensions(company);

            // Assert
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void TestFindPremiumUsingExtensionsNoPremiumCars()
        {
            // Arrange
            var fabric = new CarFabric(1);
            foreach (var workshop in fabric.fabric)
            {
                foreach (var car in workshop)
                {
                    car.Brand = "NotPremiumBrand";
                }
            }

            // Act
            var result = fabric.FindPremiumUsingExtensions();

            // Assert
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void TestGroupByBrandWithMultipleCarsOfSameBrand()
        {
            // Arrange
            var fabric = new CarFabric(1);
            foreach (var workshop in fabric.fabric)
            {
                foreach (var car in workshop)
                {
                    car.Brand = "SameBrand";
                }
            }

            // Act
            var result = fabric.GroupByBrand();

            // Assert
            Assert.IsTrue(result.Contains("Марка: SameBrand"));
        }
    }
}
