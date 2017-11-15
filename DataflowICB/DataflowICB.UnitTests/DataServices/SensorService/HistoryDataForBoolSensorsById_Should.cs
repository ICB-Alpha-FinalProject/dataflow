using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataflowICB.Models;
using System.Collections.Generic;
using Moq;
using DataflowICB.Database;
using Dataflow.Services.Contracts;
using DataflowICB.Database.Models;
using System.Globalization;
using System.Data.Entity;
using System.Linq;
using DataflowICB.Models.DataApi;

namespace DataflowICB.UnitTests.DataServices.SensorService
{
    [TestClass]
    public class HistoryDataForBoolSensorsById_Should
    {
        [TestMethod]
        public void ReturnListOfValues()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var httpClientMock = new Mock<IHttpClientProvider>();
            int id = 1;

            var sensorServices = new Dataflow.DataServices.SensorService(dbContextMock.Object, httpClientMock.Object);

            var valueHistory = new List<ValueHistory>()
            {
                new ValueHistory
                {
                    BoolSensorId = id,
                    Date = DateTime.ParseExact("03/03/2017 11:21:45", "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                    Value = 1.0
                },

                new ValueHistory
                {
                    BoolSensorId = id,
                    Date = DateTime.ParseExact("03/03/2017 11:21:50", "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                    Value = 0.0
                },

                new ValueHistory
                {
                    BoolSensorId = id,
                    Date = DateTime.ParseExact("03/03/2017 11:21:55", "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                    Value = 0.0
                }
            };

            var boolSensor = new BoolTypeSensor()
            {
                Id = id,
                CurrentValue = true,
                MeasurementType = "Open/Close",  
                BoolHistory = valueHistory
            };

            var historySetMock = new Mock<DbSet<ValueHistory>>().SetupData(valueHistory);

            dbContextMock.SetupGet(m => m.ValueHistory).Returns(historySetMock.Object);

            //Act
            List<SensorApiUpdate> result = sensorServices.HistoryDataForBoolSensorsById(id).ToList();

            //Assert
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public void CallValueHistoryOnce_WhenSensorIsExistent()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var httpClientMock = new Mock<IHttpClientProvider>();
            int id = 1;

            var sensorServices = new Dataflow.DataServices.SensorService(dbContextMock.Object, httpClientMock.Object);

            var valueHistory = new List<ValueHistory>()
            {
                new ValueHistory
                {
                    BoolSensorId = id,
                    Date = DateTime.ParseExact("03/03/2017 11:21:45", "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                    Value = 1.0
                },

                new ValueHistory
                {
                    BoolSensorId = id,
                    Date = DateTime.ParseExact("03/03/2017 11:21:50", "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                    Value = 0.0
                },

                new ValueHistory
                {
                    BoolSensorId = id,
                    Date = DateTime.ParseExact("03/03/2017 11:21:55", "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                    Value = 0.0
                }
            };

            var boolSensor = new BoolTypeSensor()
            {
                Id = id,
                CurrentValue = true,
                MeasurementType = "Open/Close",
                BoolHistory = valueHistory
            };

            var historySetMock = new Mock<DbSet<ValueHistory>>().SetupData(valueHistory);

            dbContextMock.SetupGet(m => m.ValueHistory).Returns(historySetMock.Object);

            //Act
            sensorServices.HistoryDataForBoolSensorsById(id).ToList();

            //Assert
            dbContextMock.Verify(d => d.ValueHistory, Times.Once());
        }
    }
}

