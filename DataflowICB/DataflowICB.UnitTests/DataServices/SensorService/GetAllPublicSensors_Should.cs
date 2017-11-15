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
using DataflowICB.App_Start.Contracts;

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
            var emailServiceMock = new Mock<IEmailService>();
            var userMock = new Mock<ApplicationUser>();
            string username = "Username";
            userMock.SetupGet(u => u.UserName).Returns(username);

            var termometer = new ValueTypeSensor()
            {
                MinValue = 15,
                Maxvalue = 30,
                CurrentValue = 20,
                MeasurementType = "Temperatura"
            };

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
                    IsDeleted = false,
                    OwnerId = "stringId",
                    ValueTypeSensor = termometer,
                    IsBoolType = false
                }
            };

            var sensorsSetMock = new Mock<DbSet<Sensor>>().SetupData(sensors);

            dbContextMock.SetupGet(m => m.Sensors).Returns(sensorsSetMock.Object);

            var sensorServices = new Dataflow.DataServices.SensorService(dbContextMock.Object, httpClientMock.Object, emailServiceMock.Object);

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
            var emailServiceMock = new Mock<IEmailService>();
            var userMock = new Mock<ApplicationUser>();
            string username = "Username";
            userMock.SetupGet(u => u.UserName).Returns(username);

            var termometer = new ValueTypeSensor()
            {
                MinValue = 15,
                Maxvalue = 30,
                CurrentValue = 20,
                MeasurementType = "Temperatura"
            };

            var wetness = new ValueTypeSensor()
            {
                MinValue = 15,
                Maxvalue = 30,
                CurrentValue = 20,
                MeasurementType = "Vlajnost"
            };

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
                    IsDeleted = false,
                    OwnerId = "stringId",
                    ValueTypeSensor = termometer,
                    IsBoolType = false
                },

                new Sensor()
                {
                    Id = 7,
                    Owner = userMock.Object,
                    Name = "vlajnost",
                    URL = "theGreatUrlPart2",
                    PollingInterval = 25,
                    IsPublic = false,
                    IsDeleted = false,
                    OwnerId = "stringId",
                    ValueTypeSensor = wetness,
                    IsBoolType = false
                },
            };

            var sensorsSetMock = new Mock<DbSet<Sensor>>().SetupData(sensors);

            dbContextMock.SetupGet(m => m.Sensors).Returns(sensorsSetMock.Object);

            var sensorServices = new Dataflow.DataServices.SensorService(dbContextMock.Object, httpClientMock.Object, emailServiceMock.Object);

            //Act
            List<SensorDataModel> result = sensorServices.GetAllPublicSensors().ToList();

            //Assert
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(sensors[0].Name, result[0].Name);
        }
    }
}
