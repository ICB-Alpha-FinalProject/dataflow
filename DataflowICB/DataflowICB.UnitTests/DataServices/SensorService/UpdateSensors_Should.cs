using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataflowICB.Database;
using Moq;
using Dataflow.Services.Contracts;
using DataflowICB.App_Start.Contracts;
using System.Threading.Tasks;

namespace DataflowICB.UnitTests.DataServices.SensorService
{
    [TestClass]
    public class UpdateSensors_Should
    {
        [TestMethod]
        public void CallSensorsOnce()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var httpClientMock = new Mock<IHttpClientProvider>();
            var emailServiceMock = new Mock<IEmailService>();

            var sensorServices = new Dataflow.DataServices.SensorService(dbContextMock.Object, httpClientMock.Object, emailServiceMock.Object);

            //Act
            sensorServices.UpdateSensors();

            //Assert
            dbContextMock.Verify(d => d.Sensors, Times.Once);
        }
    }
}
