using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dataflow.Services.Contracts;
using DataflowICB.Database;
using Moq;
using Dataflow.DataServices.Contracts;
using DataflowICB.Database.Models;
using System.Data.Entity;
using System.Collections.Generic;
using Dataflow.DataServices;
using DataflowICB.App_Start.Contracts;

namespace DataflowICB.UnitTests.DataServices.SensorService
{
    [TestClass]
    public class EditSensor_Should
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
            Assert.ThrowsException<ArgumentNullException>(() => sensorServices.EditSensor(null));
        }

        [TestMethod]
        public void CallSensorsOnce_WhenSensorIsExistent()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var httpClientMock = new Mock<IHttpClientProvider>();
            var emailServiceMock = new Mock<IEmailService>();
            var sensorMock = new Mock<ISensorDataModel>();
            int sensorId = 1;
            sensorMock.Setup(x => x.Id).Returns(sensorId);

            List<Sensor> sensors = new List<Sensor>()
            {
                new Sensor()
                {
                    Id = sensorId,
                    Name = "termometur",
                    URL = "theGreatUrl",
                    PollingInterval = 20,
                    IsPublic = true,
                    OwnerId = "stringId"
                }
            };

            var sensorsSetMock = new Mock<DbSet<Sensor>>().SetupData(sensors);

            dbContextMock.SetupGet(m => m.Sensors).Returns(sensorsSetMock.Object);

            var sensorServices = new Dataflow.DataServices.SensorService(dbContextMock.Object, httpClientMock.Object, emailServiceMock.Object);

            //Act
            sensorServices.EditSensor(sensorMock.Object);

            //Assert
            dbContextMock.Verify(d => d.Sensors, Times.Once());
        }
    }
}
