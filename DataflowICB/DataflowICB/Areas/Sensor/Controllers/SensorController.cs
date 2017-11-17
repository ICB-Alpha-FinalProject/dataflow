using Dataflow.DataServices.Contracts;
using DataflowICB.Attributes;
using DataflowICB.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DataflowICB.Database.Models;
using DataflowICB.Areas.Sensor.Models;
using Microsoft.AspNet.Identity;
using DataflowICB.Models;
using Dataflow.Services.Contracts;
using Bytes2you.Validation;
using DataflowICB.App_Start;
using SensorApiModels;
using DataflowICB.Models.DataApi;

namespace DataflowICB.Areas.Sensor.Controllers
{

    public class SensorController : Controller
    {

        private readonly ISensorService sensorService;
        private readonly IHttpClientProvider httpClient;

        public SensorController(ISensorService sensorService, IHttpClientProvider httpClient)
        {
            Guard.WhenArgument(sensorService, "sensorService").IsNull().Throw();
            Guard.WhenArgument(httpClient, "httpClient").IsNull().Throw();

            this.httpClient = httpClient;
            this.sensorService = sensorService;
        }
        
        //http://blog.danielcorreia.net/asp-net-mvc-vary-by-current-user/
        // so it doesnt cache info for one user for all others
        //[OutputCache(Duration = 30, VaryByCustom = "User")]

        [Authorize]
        public ActionResult UserSensors()
        {
            var sensors = this.sensorService.GetAllSensorsForUser(this.User.Identity.Name)
            .Select(sensor => new SensorViewModel
            {
                Id = sensor.Id,
                Name = sensor.Name,
                Description = sensor.Description,
                CurrentValue = sensor.CurrentValue,
                IsValueType = !sensor.IsBoolType,
                IsPublic = sensor.IsPublic,
                IsShared = sensor.IsShared,
                IsConnected = sensor.IsConnected,
                MeasurementType = sensor.MeasurementType,
                MinValue = sensor.MinValue,
                MaxValue = sensor.MaxValue

            }).ToList();

            return View(sensors);
        }
        
        [Authorize]
        public ActionResult EditSensor(int id)
        {
            var sensor = this.sensorService.GetUserSensorById(id);
                
            if (sensor == null || this.User.Identity.GetUserId() != sensor.OwnerId)
            {
                return View("NotAuthenticated");
            }

            var sensorViewModel = new SensorViewModel()
            {
                Id = sensor.Id,
                Name = sensor.Name,
                Description = sensor.Description,
                Url = sensor.URL,
                PollingInterval = sensor.PollingInterval,
                MeasurementType = sensor.MeasurementType,
                IsValueType = !sensor.IsBoolType,
                IsPublic = sensor.IsPublic,
                IsShared = sensor.IsShared
            };

            return this.View("EditSensor", sensorViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult EditSensor(SensorViewModel viewModel)
        {

            this.sensorService.EditSensor(new Dataflow.DataServices.Models.SensorDataModel()
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Description = viewModel.Description,
                URL = viewModel.Url,
                PollingInterval = viewModel.PollingInterval,
                IsBoolType = !viewModel.IsValueType,
                MeasurementType = viewModel.MeasurementType,
                IsPublic = viewModel.IsPublic,
                IsShared = viewModel.IsShared,
                MaxValue = viewModel.MaxValue,
                MinValue = viewModel.MinValue
            });

            return this.RedirectToAction("UserSensors");

        }
        
        [Authorize]
        public ActionResult ShowDetails(int id)
        {

            var sensor = this.sensorService.GetUserSensorById(id);

            if (sensor == null || this.User.Identity.GetUserId() != sensor.OwnerId)
            {
                return View("NotAuthenticated");
            }

            var sensorViewModel = new SensorViewModel()
            {
                Id = sensor.Id,
                CurrentValue = sensor.CurrentValue,
                Name = sensor.Name,
                Description = sensor.Description,
                Url = sensor.URL,
                IsValueType = !sensor.IsBoolType,
                PollingInterval = sensor.PollingInterval,
                MeasurementType = sensor.MeasurementType,
                IsPublic = sensor.IsPublic,
                IsShared = sensor.IsShared
            };

            return this.View("ShowDetails", sensorViewModel);
        }

        public ActionResult DeleteUserSensor(int id)
        {
            this.sensorService.DeleteSensor(id);


            return this.RedirectToAction("UserSensors");
        }

        public ActionResult CheckLowerRange(double MinValue, double MaxValue, double? LowestValue = double.MinValue,
            double? HighestValue = double.MaxValue)
        {
            var inRange = MinValue <= HighestValue && MinValue >= LowestValue;
            if (!inRange)
            {
                return Json($"Max value should be between {LowestValue} and {HighestValue}", JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckUpperRange(double MaxValue, double MinValue,double? LowestValue = double.MinValue, 
            double? HighestValue = double.MaxValue)
        {
            var inRange = MaxValue >= LowestValue && MaxValue <= HighestValue;
            if (!inRange)
            {
                return Json($"Min value should be between {LowestValue} and {HighestValue}", JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckPollingInterval(int PollingInterval, int MinPollingInterval = 0)
        {
            var inRange = PollingInterval >= MinPollingInterval;
            if (!inRange)
            {
                return Json($"Polling interval should be greather than {MinPollingInterval}", JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

    }
}