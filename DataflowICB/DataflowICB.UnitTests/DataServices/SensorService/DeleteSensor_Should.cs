using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataflowICB.Database;
using Moq;
using Dataflow.Services.Contracts;
using DataflowICB.App_Start.Contracts;
using System.Collections.Generic;
using DataflowICB.Database.Models;
using System.Data.Entity;
using System.Linq;

namespace DataflowICB.UnitTests.DataServices.SensorService
{
    [TestClass]
    public class DeleteSensor_Should
    {
        [TestMethod]
        public void MarkPropertyIsDeletedAsTrue_WhenItWasFalse()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var httpClientMock = new Mock<IHttpClientProvider>();
            var emailServiceMock = new Mock<IEmailService>();

            var sensorServices = new Dataflow.DataServices.SensorService(dbContextMock.Object, httpClientMock.Object, emailServiceMock.Object);

            int sensorId = 2;

            List<Sensor> sensors = new List<Sensor>()
            {
                new Sensor()
                {
                    Id = sensorId,
                    Name = "termometur",
                    URL = "theGreatUrl",
                    PollingInterval = 20,
                    IsPublic = true,
                    OwnerId = "stringId",
                    IsDeleted = false
                }
            };

            var sensorsSetMock = new Mock<DbSet<Sensor>>().SetupData(sensors);

            dbContextMock.SetupGet(m => m.Sensors).Returns(sensorsSetMock.Object);

            //Act
            sensorServices.DeleteSensor(sensorId);

            //Assert
            Assert.AreEqual(true, sensors.First().IsDeleted);
        }

        [TestMethod]
        public void PropertyIsDeletedRemainAsTrue_WhenItWasTrue()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var httpClientMock = new Mock<IHttpClientProvider>();
            var emailServiceMock = new Mock<IEmailService>();

            var sensorServices = new Dataflow.DataServices.SensorService(dbContextMock.Object, httpClientMock.Object, emailServiceMock.Object);

            int sensorId = 2;

            List<Sensor> sensors = new List<Sensor>()
            {
                new Sensor()
                {
                    Id = sensorId,
                    Name = "termometur",
                    URL = "theGreatUrl",
                    PollingInterval = 20,
                    IsPublic = true,
                    OwnerId = "stringId",
                    IsDeleted = true
                }
            };

            var sensorsSetMock = new Mock<DbSet<Sensor>>().SetupData(sensors);

            dbContextMock.SetupGet(m => m.Sensors).Returns(sensorsSetMock.Object);

            //Act
            sensorServices.DeleteSensor(sensorId);

            //Assert
            Assert.AreEqual(true, sensors.First().IsDeleted);
        }
    }
}