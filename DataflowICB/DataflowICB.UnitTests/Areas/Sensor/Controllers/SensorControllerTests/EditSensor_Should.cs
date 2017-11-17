using Dataflow.DataServices.Contracts;
using Dataflow.DataServices.Models;
using Dataflow.Services;
using DataflowICB.Areas.Sensor.Controllers;
using DataflowICB.UnitTests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;

namespace DataflowICB.UnitTests.Areas.Sensor.Controllers.SensorControllerTests
{
    [TestClass]
    public class EditSensor_Should
    {
        [TestMethod]
        public void ReturnNotAuthenticatedView_WhenSensorByIdIsNull()
        {
            // Arrange
            var sensorServiceMock = new Mock<ISensorService>();
            var httpClientMock = new Mock<HttpClientProvider>();

            int someInvalidid = -1;

            sensorServiceMock.Setup(s => s.GetUserSensorById(someInvalidid))
                .Returns<SensorDataModel>(null);

            var controller = new SensorController(sensorServiceMock.Object,
                httpClientMock.Object);


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
            
            sensorServiceMock.Setup(s => s.GetUserSensorById(It.IsAny<int>()))
                .Returns<SensorDataModel>(null);
                
            var controller = new SensorController(sensorServiceMock.Object,
                httpClientMock.Object);



            // Act
            controller.EditSensor(It.IsAny<int>());

            // Assert
        }
    }
}
