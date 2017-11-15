using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataflowICB.Database;
using Moq;
using Dataflow.DataServices;
using Dataflow.DataServices.Contracts;
using Dataflow.Services.Contracts;
using DataflowICB.App_Start.Contracts;

namespace DataflowICB.UnitTests.DataServices.SensorService
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void CreateObjectOfTypeApplicationDbContext_WhenParamsAreValid()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var httpClientMock = new Mock<IHttpClientProvider>();
            var emailServiceMock = new Mock<IEmailService>();

            //Act
            var sensorServices = new Dataflow.DataServices.SensorService(dbContextMock.Object, httpClientMock.Object, emailServiceMock.Object);

            //Assert
            Assert.IsInstanceOfType(sensorServices, typeof(ISensorService));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenApplicationDbContextIsNull()
        {
            //Arrange
            var httpClientMock = new Mock<IHttpClientProvider>();
            var emailServiceMock = new Mock<IEmailService>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new Dataflow.DataServices.SensorService(null, httpClientMock.Object, emailServiceMock.Object));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenHttpClientIsNull()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var emailServiceMock = new Mock<IEmailService>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new Dataflow.DataServices.SensorService(dbContextMock.Object, null, emailServiceMock.Object));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenHttpEmailServiceIsNull()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var httpClientMock = new Mock<IHttpClientProvider>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new Dataflow.DataServices.SensorService(dbContextMock.Object, httpClientMock.Object, null));
        }
    }


}
