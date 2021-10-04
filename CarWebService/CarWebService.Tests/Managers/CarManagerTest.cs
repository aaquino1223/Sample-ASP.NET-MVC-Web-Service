using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarWebService.Managers;
using CarWebService.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CarWebService.Tests.Managers
{
    [TestClass]
    public class CarManagerTest
    {
        private IQueryable<Car> CreateDataSet() => new List<Car>()
            {
                new Car { Id = Guid.NewGuid(), Name = "TestCar1", Color = "Red", YearMade = "1999" },
                new Car { Id = Guid.NewGuid(), Name = "TestCar2", Color = "Red", YearMade = "1999" },
                new Car { Id = Guid.NewGuid(), Name = "TestCar3", Color = "Red", YearMade = "2000" },
                new Car { Id = Guid.NewGuid(), Name = "TestCar4", Color = "Red", YearMade = "2001" },
                new Car { Id = Guid.NewGuid(), Name = "TestCar5", Color = "Red", YearMade = "2011" },
                new Car { Id = Guid.NewGuid(), Name = "TestCar6", Color = "Red", YearMade = "2011" },
            }.AsQueryable();

        [DataTestMethod]
        [DataRow("1999")]
        [DataRow("2000")]
        [DataRow("2001")]
        [DataRow("2011")]
        public void GetCarsByYearMade_IfHasMatchingRecords_DtosShouldAllHaveValueUsedForYearMade(string yearMade)
        {
            //Arange
            var data = CreateDataSet();

            var mockSet = new Mock<DbSet<Car>>();
            mockSet.As<IQueryable<Car>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Car>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Car>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Car>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<CarDbContext>();
            mockContext.Setup(context => context.Cars).Returns(mockSet.Object);

            var carManager = new CarManager(mockContext.Object);

            //Act
            var cars = carManager.GetCarsByYearMade(yearMade);

            //Assert
            Assert.IsTrue(cars.All(x => x.YearMade == yearMade));
        }

        [DataTestMethod]
        [DataRow("1999")]
        [DataRow("2000")]
        [DataRow("2001")]
        [DataRow("2011")]
        public void GetCarsByYearMade_IfHasMatchingRecords_ReturnsSameCount(string yearMade)
        {
            //Arange
            var data = CreateDataSet();

            var dataSetCountByYearsMade = data.Count(x => x.YearMade == yearMade);

            var mockSet = new Mock<DbSet<Car>>();
            mockSet.As<IQueryable<Car>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Car>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Car>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Car>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<CarDbContext>();
            mockContext.Setup(context => context.Cars).Returns(mockSet.Object);

            var carManager = new CarManager(mockContext.Object);

            //Act
            var cars = carManager.GetCarsByYearMade(yearMade);

            //Assert
            Assert.AreEqual(dataSetCountByYearsMade, cars.Count);
        }

        [DataTestMethod]
        [DataRow("1999")]
        [DataRow("2000")]
        [DataRow("2001")]
        [DataRow("2011")]
        public void GetCarsByYearMade_IfHasMatchingRecords_DtosShouldMatchData(string yearMade)
        {
            //Arange
            var data = CreateDataSet();

            var mockSet = new Mock<DbSet<Car>>();
            mockSet.As<IQueryable<Car>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Car>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Car>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Car>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<CarDbContext>();
            mockContext.Setup(context => context.Cars).Returns(mockSet.Object);

            var carManager = new CarManager(mockContext.Object);

            //Act
            var cars = carManager.GetCarsByYearMade(yearMade);

            //Assert

            var dataToMatch = data.Where(x => x.YearMade == yearMade).ToList();

            foreach (var carDto in cars)
            {
                var matchedData = dataToMatch.FirstOrDefault(x => x.Name == carDto.Name && x.Color == carDto.Color && x.YearMade == carDto.YearMade);
                if (matchedData != null)
                    dataToMatch.Remove(matchedData);
            }
            Assert.IsTrue(!dataToMatch.Any());
        }

        [DataTestMethod]
        [DataRow("9999")]
        public void GetCarsByYearMade_IfHasNoMatchingRecords_ReturnsAnEmptyList(string yearMade)
        {
            //Arange
            var data = CreateDataSet();

            var mockSet = new Mock<DbSet<Car>>();
            mockSet.As<IQueryable<Car>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Car>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Car>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Car>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<CarDbContext>();
            mockContext.Setup(context => context.Cars).Returns(mockSet.Object);

            var carManager = new CarManager(mockContext.Object);

            //Act
            var cars = carManager.GetCarsByYearMade(yearMade);

            //Assert
            Assert.IsTrue(!cars.Any());
        }
    }
}
