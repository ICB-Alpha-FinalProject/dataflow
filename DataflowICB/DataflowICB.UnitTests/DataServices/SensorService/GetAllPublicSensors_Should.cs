using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataflowICB.Database;
using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using DataflowICB.Database.Models;
using Dataflow.Services.Contracts;
using Dataflow.DataServices.Models;
using System.Linq;

namespace DataflowICB.UnitTests.DataServices.SensorService
{
    [TestClass]
    public class GetAllPublicSensors_Should
    {
        [TestMethod]
        public void CallSensorsOnce()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var httpClientMock = new Mock<IHttpClientProvider>();
            var userMock = new Mock<ApplicationUser>();

            List<Sensor> sensors = new List<Sensor>()
            {
                new Sensor()
                {
                    Id = 5,
                    Owner = userMock.Object,
                    Name = "termometur",
                    URL = "theGreatUrl",
                    PollingInterval = 20,
                    IsPublic = true,
                    OwnerId = "stringId"
                }
            };

            var sensorsSetMock = new Mock<DbSet<Sensor>>().SetupData(sensors);

            dbContextMock.SetupGet(m => m.Sensors).Returns(sensorsSetMock.Object);

            var sensorServices = new Dataflow.DataServices.SensorService(dbContextMock.Object, httpClientMock.Object);

            //Act
            sensorServices.GetAllPublicSensors();

            //Assert
            dbContextMock.Verify(d => d.Sensors, Times.Once());
        }

        [TestMethod]
        public void ReturnListOfSensorDataModels()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var httpClientMock = new Mock<IHttpClientProvider>();
            var userMock = new Mock<ApplicationUser>();

            List<Sensor> sensors = new List<Sensor>()
            {
                new Sensor()
                {
                    Id = 5,
                    Owner = userMock.Object,
                    Name = "termometur",
                    URL = "theGreatUrl",
                    PollingInterval = 20,
                    IsPublic = true,
                    OwnerId = "stringId"
                },

                new Sensor()
                {
                    Id = 7,
                    Owner = userMock.Object,
                    Name = "vlajnost",
                    URL = "theGreatUrlPart2",
                    PollingInterval = 25,
                    IsPublic = false,
                    OwnerId = "stringId"
                },
            };

            var sensorsSetMock = new Mock<DbSet<Sensor>>().SetupData(sensors);

            dbContextMock.SetupGet(m => m.Sensors).Returns(sensorsSetMock.Object);

            var sensorServices = new Dataflow.DataServices.SensorService(dbContextMock.Object, httpClientMock.Object);

            //Act
            List<SensorDataModel> result = sensorServices.GetAllPublicSensors().ToList();

            //Assert
            Assert.AreEqual(1, result.Count());
            dbContextMock.Verify(d => d.Sensors, Times.Once());
        }
    }
}
