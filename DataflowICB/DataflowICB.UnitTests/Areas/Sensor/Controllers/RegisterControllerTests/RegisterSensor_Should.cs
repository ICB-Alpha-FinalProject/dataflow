//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using DataflowICB.Areas.Sensor.Controllers;
//using Dataflow.DataServices.Contracts;
//using Moq;
//using Dataflow.Services.Contracts;
//using System.Net.Http;
//using System.Threading.Tasks;
//using System.Net;
//using DataflowICB.Models.DataApi;
//using System.Collections.Generic;
//using TestStack.FluentMVCTesting;

//namespace DataflowICB.UnitTests.Areas.Sensor.Controllers.RegisterControllerTests
//{
//    [TestClass]
//    public class RegisterSensor_Should
//    {
//        [TestMethod]
//        public void ReturnViewModel_WhenApiDataIsFetchedAndProcessed()
//        {
//            // Arrange

//            var sensorServiceMock = new Mock<ISensorService>();
//            var httpClientMock = new Mock<IHttpClientProvider>();

//            var responseMsg = Task.FromResult(new HttpResponseMessage()
//            {
//                StatusCode = HttpStatusCode.OK
//            });

//            httpClientMock.Setup(h => h.GetAsync("someUrl.com"))
//                .Returns(responseMsg);

//            var viewModel = new List<SensorApiData>()
//            {
//                new SensorApiData(){ Id = "1", IsValueType = true },
//                new SensorApiData(){Id = "2", IsValueType = false }
//            };

//            var registerController = new RegistrationController(sensorServiceMock.Object,
//                httpClientMock.Object);
//            // Act

//            //var regController = registerController.RegisterSensor();




//            // Assert
//        }
//    }
//}
