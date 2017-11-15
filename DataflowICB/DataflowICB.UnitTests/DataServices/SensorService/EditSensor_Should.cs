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

        [TestMethod]
        public void SaveChangedValues_ValueType_WhenParametersAreCorrect()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var httpClientMock = new Mock<IHttpClientProvider>();
            var emailServiceMock = new Mock<IEmailService>();
            var sensorMock = new Mock<ISensorDataModel>();

            int sensorId = 1;
            string changedName = "vlajnost";
            string changedUrl = "TheGreatURLPart4";
            int changedPolling = 40;
            bool changedIsPublic = false;
            string changedMeasureType = "W";
            bool changedIsBool = false;
            bool changedIsDeleted = true;

            sensorMock.Setup(x => x.Id).Returns(sensorId);
            sensorMock.Setup(x => x.Name).Returns(changedName);
            sensorMock.Setup(x => x.URL).Returns(changedUrl);
            sensorMock.Setup(x => x.PollingInterval).Returns(changedPolling);
            sensorMock.Setup(x => x.IsPublic).Returns(changedIsPublic);
            sensorMock.Setup(x => x.MeasurementType).Returns(changedMeasureType);
            sensorMock.Setup(x => x.IsBoolType).Returns(changedIsBool);
            sensorMock.Setup(x => x.IsDeleted).Returns(changedIsDeleted);

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
                    Id = sensorId,
                    Name = "termometur",
                    URL = "theGreatUrl",
                    PollingInterval = 20,
                    IsPublic = true,
                    OwnerId = "stringId",
                    ValueTypeSensor = termometer,
                    IsBoolType = false,
                    IsDeleted = false
                }
            };

            var sensorsSetMock = new Mock<DbSet<Sensor>>().SetupData(sensors);

            dbContextMock.SetupGet(m => m.Sensors).Returns(sensorsSetMock.Object);

            var sensorServices = new Dataflow.DataServices.SensorService(dbContextMock.Object, httpClientMock.Object, emailServiceMock.Object);

            //Act
            sensorServices.EditSensor(sensorMock.Object);

            //Assert
            Assert.AreEqual(changedName, sensors[0].Name);
            Assert.AreEqual(changedUrl, sensors[0].URL);
            Assert.AreEqual(changedPolling, sensors[0].PollingInterval);
            Assert.AreEqual(changedIsPublic, sensors[0].IsPublic);
            Assert.AreEqual(changedMeasureType, sensors[0].ValueTypeSensor.MeasurementType);
            Assert.AreEqual(changedIsBool, sensors[0].IsBoolType);
            Assert.AreEqual(changedIsDeleted, sensors[0].IsDeleted);
        }

        [TestMethod]
        public void SaveChangedValues_BoolType_WhenParametersAreCorrect()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var httpClientMock = new Mock<IHttpClientProvider>();
            var emailServiceMock = new Mock<IEmailService>();
            var sensorMock = new Mock<ISensorDataModel>();

            int sensorId = 1;
            string changedName = "Occupancy";
            string changedUrl = "TheGreatURLPart4";
            int changedPolling = 40;
            bool changedIsPublic = false;
            string changedMeasureType = "IsAnyoneIn";
            bool changedIsBool = true;
            bool changedIsDeleted = true;

            sensorMock.Setup(x => x.Id).Returns(sensorId);
            sensorMock.Setup(x => x.Name).Returns(changedName);
            sensorMock.Setup(x => x.URL).Returns(changedUrl);
            sensorMock.Setup(x => x.PollingInterval).Returns(changedPolling);
            sensorMock.Setup(x => x.IsPublic).Returns(changedIsPublic);
            sensorMock.Setup(x => x.MeasurementType).Returns(changedMeasureType);
            sensorMock.Setup(x => x.IsBoolType).Returns(changedIsBool);
            sensorMock.Setup(x => x.IsDeleted).Returns(changedIsDeleted);

            var door = new BoolTypeSensor()
            {
                CurrentValue = true,
                MeasurementType = "Open/Close"
            };

            List<Sensor> sensors = new List<Sensor>()
            {
                new Sensor()
                {
                    Id = sensorId,
                    Name = "vrata",
                    URL = "theGreatUrl",
                    PollingInterval = 20,
                    IsPublic = true,
                    OwnerId = "stringId",
                    BoolTypeSensor = door,
                    IsBoolType = true,
                    IsDeleted = false
                }
            };

            var sensorsSetMock = new Mock<DbSet<Sensor>>().SetupData(sensors);

            dbContextMock.SetupGet(m => m.Sensors).Returns(sensorsSetMock.Object);

            var sensorServices = new Dataflow.DataServices.SensorService(dbContextMock.Object, httpClientMock.Object, emailServiceMock.Object);

            //Act
            sensorServices.EditSensor(sensorMock.Object);

            //Assert
            Assert.AreEqual(changedName, sensors[0].Name);
            Assert.AreEqual(changedUrl, sensors[0].URL);
            Assert.AreEqual(changedPolling, sensors[0].PollingInterval);
            Assert.AreEqual(changedIsPublic, sensors[0].IsPublic);
            Assert.AreEqual(changedMeasureType, sensors[0].BoolTypeSensor.MeasurementType);
            Assert.AreEqual(changedIsBool, sensors[0].IsBoolType);
            Assert.AreEqual(changedIsDeleted, sensors[0].IsDeleted);
        }
    }
}
