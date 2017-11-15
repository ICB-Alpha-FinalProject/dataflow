using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataflowICB.Database;
using Moq;
using Dataflow.Services.Contracts;
using DataflowICB.App_Start.Contracts;
using DataflowICB.Database.Models;
using System.Collections.Generic;
using System.Data.Entity;
using Dataflow.DataServices.Models;
using System.Linq;

namespace DataflowICB.UnitTests.DataServices.SensorService
{
    [TestClass]
    public class GetSharedWithUserSensors_Should
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
            Assert.ThrowsException<ArgumentNullException>(() => sensorServices.GetSharedWithUserSensors(null));
        }

        [TestMethod]
        public void ShouldGetUsersSharedSensors()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var httpClientMock = new Mock<IHttpClientProvider>();
            var emailServiceMock = new Mock<IEmailService>();
            var ownerMock = new Mock<ApplicationUser>();
            var userToShareWithMock = new Mock<ApplicationUser>();
            string username = "Petur";
            string email = "petur@petur.com";
            ownerMock.SetupGet(u => u.UserName).Returns(username);
            ownerMock.SetupGet(u => u.Email).Returns(email);

            var sensorServices = new Dataflow.DataServices.SensorService(dbContextMock.Object, httpClientMock.Object, emailServiceMock.Object);

            List<ApplicationUser> users = new List<ApplicationUser>
            {
                new ApplicationUser { UserName = "Ivan", Email = "ivan@ivan.com" },
                new ApplicationUser { UserName = "Mariq", Email = "mariq@mariq.com" },
                new ApplicationUser { UserName = "Prolet", Email = "prolet@prolet.com" },
                ownerMock.Object
            };

            List<string> usernames = new List<string>
            {
                "Ivan",
                "Mariq",
                "Prolet"
            };

            var termometer = new ValueTypeSensor()
            {
                MinValue = 15,
                Maxvalue = 30,
                CurrentValue = 20,
                MeasurementType = "Temperatura",
                IsConnected = true
            };

            var door = new BoolTypeSensor()
            {
                CurrentValue = true,
                MeasurementType = "Open/Close",
                IsConnected = true
            };

            List<Sensor> sensors = new List<Sensor>()
            {
                new Sensor()
                {
                    Id = 4,
                    Name = "termometur",
                    Description = "meresht temperatura",
                    IsBoolType = false,
                    URL = "theGreatUrl",
                    PollingInterval = 20,
                    IsPublic = true,
                    IsShared = true,
                    OwnerId = "stringId",
                    Owner = ownerMock.Object,
                    IsDeleted = false,
                    SharedWithUsers = users,
                    ValueTypeSensor = termometer,
                },

                new Sensor()
                {
                    Id = 7,
                    Name = "Door",
                    Description = "meresht otvarqne i zatvarqne",
                    IsBoolType = true,
                    URL = "theGreatUrlPart2",
                    PollingInterval = 25,
                    IsPublic = false,
                    IsShared = false,
                    OwnerId = "stringId",
                    Owner = ownerMock.Object,
                    IsDeleted = false,
                    BoolTypeSensor = door
                },
            };
            List<Sensor> sharedSensors = new List<Sensor>();
            sharedSensors.Add(sensors[0]);

            ownerMock.SetupGet(u => u.SharedSensors).Returns(sharedSensors);

            var sensorsSetMock = new Mock<DbSet<Sensor>>().SetupData(sensors);

            var usersSetMock = new Mock<DbSet<ApplicationUser>>().SetupData(users);

            dbContextMock.SetupGet(m => m.Sensors).Returns(sensorsSetMock.Object);

            dbContextMock.SetupGet(m => m.Users).Returns(usersSetMock.Object);

            //Act
            List<SensorDataModel> result = sensorServices.GetSharedWithUserSensors(username).ToList();

            //Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(sensors[0].IsShared, result[0].IsShared);
            dbContextMock.Verify(d => d.Users, Times.Once);
        }
    }
}