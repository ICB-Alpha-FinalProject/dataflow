using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataflowICB.Database;
using Dataflow.Services.Contracts;
using Moq;
using DataflowICB.App_Start.Contracts;
using DataflowICB.Database.Models;
using System.Collections.Generic;
using Dataflow.DataServices.Contracts;

namespace DataflowICB.UnitTests.DataServices.SensorService
{
    [TestClass]
    public class SendEmailIfNeeded_Should
    {
        [TestMethod]
        public void MakeValueHasAlreadyBeenChangedForThisInvalidValueToFalse_WhenCurrentValueIsInRange()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var httpClientMock = new Mock<IHttpClientProvider>();
            var emailServiceMock = new Mock<IEmailService>();
            var userMock = new Mock<ApplicationUser>();
            string email = "mock@mock.com1";
            string username = "useraHasAName";
            userMock.SetupGet(u => u.Email).Returns(email);
            userMock.SetupGet(u => u.UserName).Returns(username);

            var sensorServices = new Dataflow.DataServices.SensorService(dbContextMock.Object, httpClientMock.Object, emailServiceMock.Object);

            var termometer = new ValueTypeSensor()
            {
                MinValue = 15,
                Maxvalue = 30,
                CurrentValue = 20,
                MeasurementType = "Temperatura"
            };

            Sensor sensor = new Sensor()
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
            };

            //Act
            sensorServices.SendEmailIfNeeded(sensor);

            //Assert
            Assert.AreEqual(false, sensorServices.ValueHasAlreadyBeenChangedForThisInvalidValue);
        }       
    }
}
