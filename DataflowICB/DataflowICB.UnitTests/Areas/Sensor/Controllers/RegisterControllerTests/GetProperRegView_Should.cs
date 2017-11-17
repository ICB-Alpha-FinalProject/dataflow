using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataflowICB.Areas.Sensor.Controllers;
using Dataflow.DataServices.Contracts;
using Moq;
using Dataflow.Services.Contracts;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using DataflowICB.Models.DataApi;
using System.Collections.Generic;
using TestStack.FluentMVCTesting;
using DataflowICB.Areas.Sensor.Models;
using Microsoft.AspNet.Identity;
using System.Web;
using DataflowICB.Database.Models;
using System.Security.Principal;
using System.Web.Mvc;
using System.Web.Routing;
using DataflowICB.UnitTests.Helpers;

namespace DataflowICB.UnitTests.Areas.Sensor.Controllers.RegisterControllerTests
{
    [TestClass]
    public class GetProperRegView_Should
    {
        [TestMethod]
        public void ReturnRegisterValueSensorView_WithViewModel_WhenParameterIsValueType()
        {
            // Arrange
            var sensorServiceMock = new Mock<ISensorService>();
            var httpClientMock = new Mock<IHttpClientProvider>();
 
            var controller = new RegistrationController(sensorServiceMock.Object,
                httpClientMock.Object);

            controller.UserMocking("test");

            var apiParameter = new SensorApiData
            {
                IsValueType = true,
                Highestvalue = 10,
                LowestValue = 5,
                Id = "21312312",
                MeasureType = "W",
                MinPollingIntervalInSeconds = 1
            };
            // Act
            controller.GetProperRegView(apiParameter);
            
            // Assert
            controller
                .WithCallTo(r => r.GetProperRegView(apiParameter))
                .ShouldRenderView("RegisterValueSensor")
                .WithModel<SensorViewModel>(s =>
                {
                    Assert.IsTrue(apiParameter.IsValueType);
                    Assert.AreEqual(s.HighestValue, apiParameter.Highestvalue);
                    Assert.AreEqual(s.LowestValue, apiParameter.LowestValue);
                });
        }

        [TestMethod]
        public void ReturnRegisterBoolensorView_WithViewModel_WhenParameterIBoolType()
        {
            // Arrange
            var sensorServiceMock = new Mock<ISensorService>();
            var httpClientMock = new Mock<IHttpClientProvider>();

            var controller = new RegistrationController(sensorServiceMock.Object,
                httpClientMock.Object);

            controller.UserMocking("test");

            var apiParameter = new SensorApiData
            {
                IsValueType = false,
                Id = "21312312",
                MeasureType = "true/false",
                MinPollingIntervalInSeconds = 1
            };
            // Act
            controller.GetProperRegView(apiParameter);

            // Assert
            controller
                .WithCallTo(r => r.GetProperRegView(apiParameter))
                .ShouldRenderView("RegisterBoolSensor")
                .WithModel<SensorViewModel>(s =>
                {
                    Assert.IsFalse(apiParameter.IsValueType);
                });
        }
    }
}
