using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataflowICB.Database;
using Dataflow.Services.Contracts;
using Moq;
using Dataflow.DataServices.Contracts;
using DataflowICB.Database.Models;
using System.Collections.Generic;
using System.Data.Entity;
using DataflowICB.App_Start.Contracts;
using System.Linq;

namespace DataflowICB.UnitTests.DataServices.SensorService
{
    [TestClass]
    public class ShareSensorWithUser_Should
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenParameterIsNull()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var httpClientMock = new Mock<IHttpClientProvider>();
            var emailServiceMock = new Mock<IEmailService>();

            var sensorServices = new Dataflow.DataServices.SensorService(dbContextMock.Object, httpClientMock.Object, emailServiceMock.Object);
            int id = 2;

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => sensorServices.ShareSensorWithUser(id, null));
        }

        [TestMethod]
        public void CallSensorsOnce_WhenSensorIsExistent()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var httpClientMock = new Mock<IHttpClientProvider>();
            var emailServiceMock = new Mock<IEmailService>();
            var sensorMock = new Mock<ISensorDataModel>();
            int firstSensorId = 2;
            int secondSensorId = 7;

            string firstUsername = "FirstUser";
            string firstUserId = "firstUserId";
            
            string secondUsername = "SecondUser";
            string secondUserId = "secondUserId";

            sensorMock.Setup(x => x.Id).Returns(firstSensorId);

            var firstUserMock = new Mock<ApplicationUser>();
            firstUserMock.Setup(u => u.UserName).Returns(firstUsername);
            firstUserMock.Setup(u => u.Id).Returns(firstUserId);

            var secondUserMock = new Mock<ApplicationUser>();
            secondUserMock.Setup(u => u.UserName).Returns(secondUsername);
            secondUserMock.Setup(u => u.Id).Returns(secondUserId);

            List<ApplicationUser> users = new List<ApplicationUser>()
            {
                firstUserMock.Object,
                secondUserMock.Object
            };

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
                    Id = firstSensorId,
                    Name = "termometur",
                    IsBoolType = false,
                    URL = "theGreatUrl",
                    PollingInterval = 20,
                    ValueTypeSensor = termometer,
                    IsPublic = true,
                    IsShared = true,
                    OwnerId = "stringId",
                    Owner = firstUserMock.Object,
                    IsDeleted = false
                },

                new Sensor()
                {
                    Id = secondSensorId,
                    Name = "Door",
                    IsBoolType = true,
                    URL = "theGreatUrlPart2",
                    PollingInterval = 25,
                    BoolTypeSensor = door,
                    IsPublic = false,
                    IsShared = false,
                    OwnerId = "stringId",
                    Owner = firstUserMock.Object,
                    IsDeleted = false
                },
            };

            var sensorsSetMock = new Mock<DbSet<Sensor>>().SetupData(sensors);

            var usersSetMock = new Mock<DbSet<ApplicationUser>>().SetupData(users);

            dbContextMock.SetupGet(m => m.Sensors).Returns(sensorsSetMock.Object);

            dbContextMock.SetupGet(m => m.Users).Returns(usersSetMock.Object);

            var sensorServices = new Dataflow.DataServices.SensorService(dbContextMock.Object, httpClientMock.Object, emailServiceMock.Object);

            //Act
            sensorServices.ShareSensorWithUser(firstSensorId, secondUsername);

            //Assert
            dbContextMock.Verify(d => d.Users, Times.Once());
            dbContextMock.Verify(d => d.Sensors, Times.Once());
        }

        [TestMethod]
        public void ShouldBeShared_WhenSensorIsExistent()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var httpClientMock = new Mock<IHttpClientProvider>();
            var emailServiceMock = new Mock<IEmailService>();
            var sensorMock = new Mock<ISensorDataModel>();
            int firstSensorId = 2;
            int secondSensorId = 7;

            string firstUsername = "FirstUser";
            string firstUserId = "firstUserId";

            string secondUsername = "SecondUser";
            string secondUserId = "secondUserId";

            sensorMock.Setup(x => x.Id).Returns(firstSensorId);

            var firstUserMock = new Mock<ApplicationUser>();
            firstUserMock.Setup(u => u.UserName).Returns(firstUsername);
            firstUserMock.Setup(u => u.Id).Returns(firstUserId);

            var secondUserMock = new Mock<ApplicationUser>();
            secondUserMock.Setup(u => u.UserName).Returns(secondUsername);
            secondUserMock.Setup(u => u.Id).Returns(secondUserId);

            List<ApplicationUser> users = new List<ApplicationUser>()
            {
                firstUserMock.Object,
                secondUserMock.Object
            };

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
                    Id = firstSensorId,
                    Name = "termometur",
                    IsBoolType = false,
                    URL = "theGreatUrl",
                    PollingInterval = 20,
                    ValueTypeSensor = termometer,
                    IsPublic = true,
                    IsShared = true,
                    OwnerId = "stringId",
                    Owner = firstUserMock.Object,
                    IsDeleted = false
                },

                new Sensor()
                {
                    Id = secondSensorId,
                    Name = "Door",
                    IsBoolType = true,
                    URL = "theGreatUrlPart2",
                    PollingInterval = 25,
                    BoolTypeSensor = door,
                    IsPublic = false,
                    IsShared = false,
                    OwnerId = "stringId",
                    Owner = firstUserMock.Object,
                    IsDeleted = false
                },
            };

            var sensorsSetMock = new Mock<DbSet<Sensor>>().SetupData(sensors);

            var usersSetMock = new Mock<DbSet<ApplicationUser>>().SetupData(users);

            dbContextMock.SetupGet(m => m.Sensors).Returns(sensorsSetMock.Object);

            dbContextMock.SetupGet(m => m.Users).Returns(usersSetMock.Object);

            var sensorServices = new Dataflow.DataServices.SensorService(dbContextMock.Object, httpClientMock.Object, emailServiceMock.Object);

            //Act
            sensorServices.ShareSensorWithUser(firstSensorId, secondUsername);

            //Assert
            Assert.AreEqual(secondUsername, sensors[0].SharedWithUsers.First().UserName);
        }
    }
}
