using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataflowICB.Database;
using Moq;
using Dataflow.Services.Contracts;
using DataflowICB.Database.Models;
using System.Collections.Generic;
using System.Data.Entity;
using Dataflow.DataServices.Models;
using System.Linq;
using DataflowICB.App_Start.Contracts;

namespace DataflowICB.UnitTests.DataServices.SensorService
{
    [TestClass]
    public class GetAllSensors_Should
    {
        [TestMethod]
        public void ReturnListOfAllSensorDataModels_WhenIsAdminIsTrue()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var httpClientMock = new Mock<IHttpClientProvider>();
            var emailServiceMock = new Mock<IEmailService>();
            var userMock = new Mock<ApplicationUser>();
            bool IsAdmin = true;

            var termometer = new ValueTypeSensor()
            {
                MinValue = 15,
                Maxvalue = 30,
                CurrentValue = 20,
                MeasurementType = "Temperatura"
            };

            var door = new BoolTypeSensor()
            {
                CurrentValue = true,
                MeasurementType = "Open/Close"
            };

            List<Sensor> sensors = new List<Sensor>()
            {
                new Sensor()
                {
                    Id = 5,
                    Name = "termometur",
                    IsBoolType = false,
                    URL = "theGreatUrl",
                    PollingInterval = 20,
                    ValueTypeSensor = termometer,
                    IsPublic = true,
                    IsShared = false,
                    OwnerId = "stringId",
                    Owner = userMock.Object,
                    IsDeleted = true                 
                },

                new Sensor()
                {
                    Id = 7,
                    Name = "Door",
                    IsBoolType = true,
                    URL = "theGreatUrlPart2",
                    PollingInterval = 25,
                    BoolTypeSensor = door,
                    IsPublic = false,
                    IsShared = false,
                    OwnerId = "stringId",
                    Owner = userMock.Object,
                    IsDeleted = false
                },
            };

            userMock.SetupGet(u => u.UserName).Returns("OwnerOfSensor");

            var sensorsSetMock = new Mock<DbSet<Sensor>>().SetupData(sensors);

            dbContextMock.SetupGet(m => m.Sensors).Returns(sensorsSetMock.Object);

            var sensorServices = new Dataflow.DataServices.SensorService(dbContextMock.Object, httpClientMock.Object, emailServiceMock.Object);

            //Act
            List<SensorDataModel> result = sensorServices.GetAllSensors(IsAdmin).ToList();

            //Assert
            Assert.AreEqual(2, result.Count());
            dbContextMock.Verify(d => d.Sensors, Times.Once());
        }

        [TestMethod]
        public void ReturnListOfAllPublicSensorDataModels_WhenIsAdminIsFalse()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var httpClientMock = new Mock<IHttpClientProvider>();
            var emailServiceMock = new Mock<IEmailService>();
            var userMock = new Mock<ApplicationUser>();
            bool IsAdmin = false;

            var termometer = new ValueTypeSensor()
            {
                MinValue = 15,
                Maxvalue = 30,
                CurrentValue = 20,
                MeasurementType = "Temperatura"
            };

            var door = new BoolTypeSensor()
            {
                CurrentValue = true,
                MeasurementType = "Open/Close"
            };

            List<Sensor> sensors = new List<Sensor>()
            {
                new Sensor()
                {
                    Id = 5,
                    Name = "termometur",
                    IsBoolType = false,
                    URL = "theGreatUrl",
                    PollingInterval = 20,
                    ValueTypeSensor = termometer,
                    IsPublic = true,
                    IsShared = false,
                    OwnerId = "stringId",
                    Owner = userMock.Object,
                    IsDeleted = true
                },

                new Sensor()
                {
                    Id = 7,
                    Name = "Door",
                    IsBoolType = true,
                    URL = "theGreatUrlPart2",
                    PollingInterval = 25,
                    BoolTypeSensor = door,
                    IsPublic = false,
                    IsShared = false,
                    OwnerId = "stringId",
                    Owner = userMock.Object,
                    IsDeleted = false
                },
            };

            userMock.SetupGet(u => u.UserName).Returns("OwnerOfSensor");

            var sensorsSetMock = new Mock<DbSet<Sensor>>().SetupData(sensors);

            dbContextMock.SetupGet(m => m.Sensors).Returns(sensorsSetMock.Object);

            var sensorServices = new Dataflow.DataServices.SensorService(dbContextMock.Object, httpClientMock.Object, emailServiceMock.Object);

            //Act
            List<SensorDataModel> result = sensorServices.GetAllSensors(IsAdmin).ToList();

            //Assert
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(sensors[1].Name, result[0].Name);
            dbContextMock.Verify(d => d.Sensors, Times.Once());
        }
    }
}
