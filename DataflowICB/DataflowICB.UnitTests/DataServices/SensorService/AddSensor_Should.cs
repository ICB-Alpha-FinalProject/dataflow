using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dataflow.DataServices;
using DataflowICB.Database;
using Moq;
using Dataflow.Services.Contracts;
using DataflowICB.Database.Models;
using System.Collections.Generic;
using System.Data.Entity;
using DataflowICB.App_Start.Contracts;

namespace DataflowICB.UnitTests.DataServices.SensorService
{
    [TestClass]
    public class AddSensor_Should
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenParameterIsNull()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var httpClientMock = new Mock<IHttpClientProvider>();
            var emailServiceMock = new Mock<IEmailService>();

            var sensorServices = new Dataflow.DataServices.SensorService(dbContextMock.Object, httpClientMock.Object, emailServiceMock.Object);

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => sensorServices.AddSensor(null));
        }

        [TestMethod]
        public void CallSensorsOnce_WhenSensorIsNotNull()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var httpClientMock = new Mock<IHttpClientProvider>();
            var emailServiceMock = new Mock<IEmailService>();

            List<Sensor> sensors = new List<Sensor>();
            Sensor sensor = new Sensor()
            {
                Id = 5,
                Name = "termometur",
                URL = "theGreatUrl",
                PollingInterval = 20,
                IsPublic = true,
                OwnerId = "stringId"
            };

            var sensorSetMock = new Mock<DbSet<Sensor>>().SetupData(sensors);

            dbContextMock.SetupGet(m => m.Sensors).Returns(sensorSetMock.Object);

            var sensorServices = new Dataflow.DataServices.SensorService(dbContextMock.Object, httpClientMock.Object, emailServiceMock.Object);

            //Act
            sensorServices.AddSensor(sensor);

            //Assert
            dbContextMock.Verify(d => d.Sensors, Times.Once());
        }

        [TestMethod]
        public void AddToDatabaseSensor_WhenSensorIsNotNull()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var httpClientMock = new Mock<IHttpClientProvider>();
            var emailServiceMock = new Mock<IEmailService>();

            List<Sensor> sensors = new List<Sensor>();
            Sensor sensor = new Sensor()
            {
                Id = 5,
                Name = "termometur",
                URL = "theGreatUrl",
                PollingInterval = 20,
                IsPublic = true,
                OwnerId = "stringId"
            };

            var sensorSetMock = new Mock<DbSet<Sensor>>().SetupData(sensors);

            dbContextMock.SetupGet(m => m.Sensors).Returns(sensorSetMock.Object);

            var sensorServices = new Dataflow.DataServices.SensorService(dbContextMock.Object, httpClientMock.Object, emailServiceMock.Object);

            //Act
            sensorServices.AddSensor(sensor);

            //Assert
            Assert.AreSame(sensor, sensors[0]);
        }
    }
}
