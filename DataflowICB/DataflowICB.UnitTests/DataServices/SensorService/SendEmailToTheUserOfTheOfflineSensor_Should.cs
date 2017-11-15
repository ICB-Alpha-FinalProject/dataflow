using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataflowICB.Database;
using Moq;
using Dataflow.Services.Contracts;
using DataflowICB.App_Start.Contracts;
using DataflowICB.App_Start.Models;
using DataflowICB.Database.Models;

namespace DataflowICB.UnitTests.DataServices.SensorService
{
    [TestClass]
    public class SendEmailToTheUserOfTheOfflineSensor_Should
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
            Assert.ThrowsException<ArgumentNullException>(() => sensorServices.SendEmailToTheUserOfTheOfflineSensor(null));
        }
    }
}
