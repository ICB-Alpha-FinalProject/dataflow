using Dataflow.DataServices.Contracts;
using Dataflow.Services;
using DataflowICB.Areas.Sensor.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DataflowICB.UnitTests.Helpers;
using Dataflow.DataServices.Models;
using TestStack.FluentMVCTesting;
using DataflowICB.Areas.Sensor.Models;

namespace DataflowICB.UnitTests.Areas.Sensor.Controllers.SensorControllerTests
{
    [TestClass]
    public class UserSensors_Should
    {
        [TestMethod]
        public void ReturnDefaultView_WithUserSensors()
        {
            // Arrange
            var sensorServiceMock = new Mock<ISensorService>();
            var httpClientMock = new Mock<HttpClientProvider>();

            var controller = new SensorController(sensorServiceMock.Object,
                httpClientMock.Object);

            controller.UserMocking("test");

            var sensors = new List<SensorDataModel>
            {
                new SensorDataModel
                {
                    Id = 1
                },
                new SensorDataModel
                {
                    Id = 2
                }
            };

            sensorServiceMock
                .Setup(s => s.GetAllSensorsForUser("test"))
                .Returns(sensors);

            // Act
            controller.UserSensors();

            // Assert
            controller.WithCallTo(c => c.UserSensors())
                .ShouldRenderDefaultView()
                .WithModel<List<SensorViewModel>>(vm =>
                {
                    for (int i = 0; i < vm.Count; i++)
                    {
                        Assert.AreEqual(vm[i].Id, sensors[i].Id);
                    }
                });
        }


        [TestMethod]
        public void ReturnNotAuthenticatedView_WhenSensorByIdIsNull()
        {
            // Arrange
            var sensorServiceMock = new Mock<ISensorService>();
            var httpClientMock = new Mock<HttpClientProvider>();

            var controller = new SensorController(sensorServiceMock.Object,
                httpClientMock.Object);
            
            int someInvalidid = -1;
            
            sensorServiceMock.Setup(s => s.GetUserSensorById(someInvalidid))
                .Returns<SensorDataModel>(null);

            // Act
            controller.EditSensor(someInvalidid);

            // Assert
            controller.WithCallTo(c => c.EditSensor(someInvalidid))
                .ShouldRenderView("NotAuthenticated");
        }

        [TestMethod]
        public void ReturnEditSensorView_WhenSensorByIdIsValid()
        {
            // Arrange
            var sensorServiceMock = new Mock<ISensorService>();
            var httpClientMock = new Mock<HttpClientProvider>();

            var controller = new SensorController(sensorServiceMock.Object,
                httpClientMock.Object);

            controller.UserMocking("test");

            var sensor = new SensorDataModel()
            {
                OwnerId = It.IsAny<string>()
            };

            sensorServiceMock.Setup(s => s.GetUserSensorById(It.IsAny<int>()))
                .Returns(sensor);

            // Act
            controller.EditSensor(It.IsAny<int>());

            // Assert
            controller.WithCallTo(c => c.EditSensor(It.IsAny<int>()))
                .ShouldRenderView("EditSensor");
        }
    }
}

