using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataflowICB.Database;
using Moq;
using Dataflow.Services.Contracts;
using Dataflow.DataServices.Contracts;
using DataflowICB.Database.Models;
using System.Collections.Generic;
using System.Data.Entity;
using Dataflow.DataServices.Models;
using System.Linq;

namespace DataflowICB.UnitTests.DataServices.SensorService
{
    [TestClass]
    public class GetAllSensorsForUser_Should
    {
        [TestMethod]
        public void CallSensorsOnce_WhenSensorIsExistent()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var httpClientMock = new Mock<IHttpClientProvider>();
            var sensorMock = new Mock<ISensorDataModel>();
            int Id = 2;
            sensorMock.Setup(x => x.Id).Returns(Id);

            var userMock = new Mock<ApplicationUser>();

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
                    Id = Id,
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

            var sensorsSetMock = new Mock<DbSet<Sensor>>().SetupData(sensors);

            string username = "Test";
            userMock.SetupGet(u => u.UserName).Returns(username);

            dbContextMock.SetupGet(m => m.Sensors).Returns(sensorsSetMock.Object);

            var sensorServices = new Dataflow.DataServices.SensorService(dbContextMock.Object, httpClientMock.Object);

            //Act
            sensorServices.GetAllSensorsForUser(username);

            //Assert
            dbContextMock.Verify(d => d.Sensors, Times.Once());
        }

        [TestMethod]
        public void ReturnListOfAllSensorForUser()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var httpClientMock = new Mock<IHttpClientProvider>();
            var sensorMock = new Mock<ISensorDataModel>();
            int Id = 2;
            sensorMock.Setup(x => x.Id).Returns(Id);

            var userMock = new Mock<ApplicationUser>();

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
                    Id = Id,
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

            var sensorsSetMock = new Mock<DbSet<Sensor>>().SetupData(sensors);

            string username = "Test";
            userMock.SetupGet(u => u.UserName).Returns(username);

            dbContextMock.SetupGet(m => m.Sensors).Returns(sensorsSetMock.Object);

            var sensorServices = new Dataflow.DataServices.SensorService(dbContextMock.Object, httpClientMock.Object);

            //Act
            List<SensorDataModel> result = sensorServices.GetAllSensorsForUser(username).ToList();

            //Assert
            Assert.AreEqual(2, result.Count());
        }
    }
}
