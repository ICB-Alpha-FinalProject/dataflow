using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataflowICB.Database;
using Dataflow.Services.Contracts;
using Moq;
using DataflowICB.Database.Models;
using System.Collections.Generic;
using System.Globalization;
using System.Data.Entity;
using System.Linq;
using DataflowICB.Models.DataApi;
using DataflowICB.App_Start.Contracts;

namespace DataflowICB.UnitTests.DataServices.SensorService
{
    [TestClass]
    public class HistoryDataForValueSensorsById_Should
    {
        [TestMethod]
        public void ReturnListOfValues()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var httpClientMock = new Mock<IHttpClientProvider>();
            var emailServiceMock = new Mock<IEmailService>();
            int id = 1;

            var sensorServices = new Dataflow.DataServices.SensorService(dbContextMock.Object, httpClientMock.Object, emailServiceMock.Object);

            var valueHistory = new List<ValueHistory>()
            {
                new ValueHistory
                {
                    ValueSensorId = id,
                    Date = DateTime.ParseExact("03/03/2017 11:21:45", "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                    Value = 21.1
                },

                new ValueHistory
                {
                    ValueSensorId = id,
                    Date = DateTime.ParseExact("03/03/2017 11:21:50", "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                    Value = 23.2
                },

                new ValueHistory
                {
                    ValueSensorId = id,
                    Date = DateTime.ParseExact("03/03/2017 11:21:55", "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                    Value = 12.2
                }
            };

            var valueSensor = new ValueTypeSensor()
            {
                Id = id,
                CurrentValue = 15.1,
                Maxvalue = 30,
                MinValue = 10,
                MeasurementType = "temperature",
                ValueHistory = valueHistory
            };

            var historySetMock = new Mock<DbSet<ValueHistory>>().SetupData(valueHistory);

            dbContextMock.SetupGet(m => m.ValueHistory).Returns(historySetMock.Object);

            //Act
            List<SensorApiUpdate> result = sensorServices.HistoryDataForValueSensorsById(id).ToList();

            //Assert
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual(valueHistory[0].Value.ToString(), result[0].Value);
        }

        [TestMethod]
        public void CallValueHistoryOnce_WhenSensorIsExistent()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var httpClientMock = new Mock<IHttpClientProvider>();
            var emailServiceMock = new Mock<IEmailService>();
            int id = 1;

            var sensorServices = new Dataflow.DataServices.SensorService(dbContextMock.Object, httpClientMock.Object, emailServiceMock.Object);

            var valueHistory = new List<ValueHistory>()
            {
                new ValueHistory
                {
                    ValueSensorId = id,
                    Date = DateTime.ParseExact("03/03/2017 11:21:45", "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                    Value = 1.0
                },

                new ValueHistory
                {
                    ValueSensorId = id,
                    Date = DateTime.ParseExact("03/03/2017 11:21:50", "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                    Value = 0.0
                },

                new ValueHistory
                {
                    ValueSensorId = id,
                    Date = DateTime.ParseExact("03/03/2017 11:21:55", "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                    Value = 0.0
                }
            };

            var valueSensor = new ValueTypeSensor()
            {
                Id = id,
                CurrentValue = 15.1,
                Maxvalue = 30,
                MinValue = 10,
                MeasurementType = "temperature",
                ValueHistory = valueHistory
            };

            var historySetMock = new Mock<DbSet<ValueHistory>>().SetupData(valueHistory);

            dbContextMock.SetupGet(m => m.ValueHistory).Returns(historySetMock.Object);

            //Act
            sensorServices.HistoryDataForValueSensorsById(id).ToList();

            //Assert
            dbContextMock.Verify(d => d.ValueHistory, Times.Once());
        }
    }
}
