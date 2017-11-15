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
    public class GetUsersSharedSensor_Should
    {
        [TestMethod]
        public void ReturnSensorDataModelWithSharedUsersName()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var httpClientMock = new Mock<IHttpClientProvider>();
            var emailServiceMock = new Mock<IEmailService>();
            var ownerMock = new Mock<ApplicationUser>();
            var userToShareWithMock = new Mock<ApplicationUser>();
            int Id = 4;

            var sensorServices = new Dataflow.DataServices.SensorService(dbContextMock.Object, httpClientMock.Object, emailServiceMock.Object);

            List<ApplicationUser> users = new List<ApplicationUser>
            {
                new ApplicationUser { UserName = "Ivan", Email = "ivan@ivan.com" },
                new ApplicationUser { UserName = "Mariq", Email = "mariq@mariq.com" },
                new ApplicationUser { UserName = "Prolet", Email = "prolet@prolet.com" }
            };

            List<string> usernames = new List<string>
            {
                "Ivan",
                "Mariq",
                "Prolet"
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
                    IsPublic = true,
                    IsShared = true,
                    OwnerId = "stringId",
                    Owner = ownerMock.Object,
                    IsDeleted = false,
                    SharedWithUsers = users
                },

                new Sensor()
                {
                    Id = 7,
                    Name = "Door",
                    IsBoolType = true,
                    URL = "theGreatUrlPart2",
                    PollingInterval = 25,
                    IsPublic = false,
                    IsShared = false,
                    OwnerId = "stringId",
                    Owner = ownerMock.Object,
                    IsDeleted = false
                },
            };

            var sensorsSetMock = new Mock<DbSet<Sensor>>().SetupData(sensors);

            dbContextMock.SetupGet(m => m.Sensors).Returns(sensorsSetMock.Object);

            //Act
            SensorDataModel result = sensorServices.GetUsersSharedSensor(Id);

            //Assert
            Assert.AreEqual(usernames.Count, result.SharedWithUsers.Count);
            Assert.AreEqual(usernames.First(), result.SharedWithUsers.First());
            dbContextMock.Verify(d => d.Sensors, Times.Once);
        }
    }
}