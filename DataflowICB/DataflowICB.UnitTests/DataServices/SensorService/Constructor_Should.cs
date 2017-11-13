using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataflowICB.Database;
using Moq;
using Dataflow.DataServices;
using Dataflow.DataServices.Contracts;
using Dataflow.Services.Contracts;

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

            //Act
            var sensorServices = new Dataflow.DataServices.SensorService(dbContextMock.Object, httpClientMock.Object);

            //Assert
            Assert.IsInstanceOfType(sensorServices, typeof(ISensorService));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenApplicationDbContextIsNull()
        {
            //Arrange
            var httpClientMock = new Mock<IHttpClientProvider>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new Dataflow.DataServices.SensorService(null, httpClientMock.Object));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenHttpClientIsNull()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new Dataflow.DataServices.SensorService(dbContextMock.Object, null));
        }
    }


}
