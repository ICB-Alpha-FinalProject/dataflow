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
using DataflowICB.Models.DataApi;
using Dataflow.Services.Contracts;
using Bytes2you.Validation;
using DataflowICB.App_Start;
using SensorApiModels;

namespace DataflowICB.Areas.Sensor.Controllers
{

    //TODO: Optimization of LINQ queries
    //TODO: Validation
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

        [Authorize]
        //[OutputCache(Duration = 10)]
        public async Task<ActionResult> RegisterSensor()
        {
            var resp = await httpClient.GetAsync(AppConstants.AllSensorsUrl);
            var content = await resp.Content.ReadAsStringAsync();
            var resViewModel = JsonConvert.DeserializeObject<List<SensorApiData>>(content);

            foreach (var sensor in resViewModel)
            {
                var isValueTYpe = !sensor.Description.Contains("false");
                sensor.IsValueType = isValueTYpe;
                if (isValueTYpe)
                {
                    var splitted = sensor.Description.
                        Split(new string[] { "and", }, StringSplitOptions.RemoveEmptyEntries);

                    var lowest = splitted[0].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).Last();
                    var highest = splitted[splitted.Length - 1];

                    sensor.LowestValue = double.Parse(lowest);
                    sensor.Highestvalue = double.Parse(highest);
                }
            }

            return this.View(resViewModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSensor(SensorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var sensor = new DataflowICB.Database.Models.Sensor()
                {
                    OwnerId = this.User.Identity.GetUserId(),
                    Description = model.Description,
                    IsPublic = model.IsPublic,
                    Name = model.Name,
                    URL = model.Url,
                    PollingInterval = model.PollingInterval,
                    LastUpdate = DateTime.Now
                };

                if (model.IsValueType)
                {
                    var valueType = new ValueTypeSensor()
                    {
                        MeasurementType = model.MeasurementType,
                        //IsInAcceptableRange = model.ValueTypeSensor.IsInAcceptableRange,
                        Maxvalue = model.MaxValue,
                        MinValue = model.MinValue
                    };
                    sensor.IsBoolType = false;
                    sensor.ValueTypeSensor = valueType;
                }
                else
                {
                    var boolType = new BoolTypeSensor()
                    {
                        MeasurementType = model.MeasurementType
                    };

                    sensor.IsBoolType = true;
                    sensor.BoolTypeSensor = boolType;
                }

                this.sensorService.AddSensor(sensor);

                return this.Json(Url.Action("Index", "Home", new { area = "" }));
            }
            else
            {
                if (model.IsValueType)
                {
                    return this.View("RegisterValueSensor", model);
                }
                else
                {
                    return this.View("RegisterBoolSensor", model);
                }
            }
        }

        [Authorize]
        [AjaxOnly]
        public ActionResult GetProperRegView(SensorApiData vm)
        {
            var sensorVm = new SensorViewModel()
            {
                Url = "http://telerikacademy.icb.bg/api/sensor/" + vm.Id,
                IsValueType = vm.IsValueType,
                CreatorUsername = this.HttpContext.User.Identity.Name,
                MeasurementType = vm.MeasureType,
                MinPollingInterval = vm.MinPollingIntervalInSeconds,
                LowestValue = vm.LowestValue,
                HighestValue = vm.Highestvalue
            };

            if (vm.IsValueType)
            {
                //sensorVm.ValueTypeSensor = new ValueTypeSensorViewModel();
                //sensorVm.ValueTypeSensor.LowestValue = vm.LowestValue;
                //sensorVm.ValueTypeSensor.HighestValue = vm.Highestvalue;
                return this.View("RegisterValueSensor", sensorVm);
            }
            else
            {
                return this.View("RegisterBoolSensor", sensorVm);
            }
        }

        public async Task<ActionResult> UpdateSensors()
        {
            await this.sensorService.UpdateSensors();
            //return this.RedirectToAction("Index", "Home", new { area = "" });
            return new EmptyResult();
        }


        [AjaxOnly]
        [Authorize]
        public JsonResult GetHistoryDataForSensor(int sensorId, bool isValueType)
        {
            IEnumerable<SensorApiUpdate> sensors;
            if (isValueType)
            {
                sensors = this.sensorService.HistoryDataForValueSensorsById(sensorId);
            }
            else
            {
                sensors = this.sensorService.HistoryDataForBoolSensorsById(sensorId);
            }
            var serialized = JsonConvert.SerializeObject(sensors);

            return this.Json(serialized, JsonRequestBehavior.AllowGet);
        }

        // http://blog.danielcorreia.net/asp-net-mvc-vary-by-current-user/
        // so it doesnt cache info for one user for all others
        //[OutputCache(Duration = 30, VaryByCustom = "User")]
        [Authorize]
        public ActionResult UserSensors()
        {
            //TODO REFACTORING getallsensorsForUSer method
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
            var sensor = this.sensorService.GetSensorById(id);


            if (this.User.Identity.GetUserId() != sensor.OwnerId)
            {
                return View("NotAutheticated");
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
            });

            return this.RedirectToAction("UserSensors");

        }

        [Authorize]
        public ActionResult ShareSensor(int id)
        {
            var sensor = this.sensorService.GetSensorById(id);

            if (this.User.Identity.GetUserId() != sensor.OwnerId)
            {
                return View("NotAutheticated");
            }

            var sensorViewModel = new SensorViewModel()
            {
                Id = sensor.Id,
                CurrentValue = sensor.CurrentValue,
                Name = sensor.Name,
                Description = sensor.Description,
                Url = sensor.URL,
                PollingInterval = sensor.PollingInterval,
                IsValueType = !sensor.IsBoolType,
                MeasurementType = sensor.MeasurementType,
                IsPublic = sensor.IsPublic,
                IsShared = sensor.IsShared
            };
            return View("ShareSensor", sensorViewModel);
        }

        [Authorize]
        public ActionResult SharedSensors()
        {

            //if (this.User.Identity.GetUserId() != sharedSensorUsers.OwnerId)
            //{
            //    return View("NotAutheticated");
            //}

            var sharedSensors = this.sensorService.GetSharedWithUserSensors(this.User.Identity.GetUserName())
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
                 MeasurementType = sensor.MeasurementType

             }).ToList();

            return View(sharedSensors);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult ShareSensor(SensorViewModel viewModel)
        {
            this.sensorService.ShareSensorWithUser(viewModel.Id, viewModel.ShareWithUser);

            return this.RedirectToAction("UserSensors");
        }

        //TODO: in detail view listing of who is the sensor shared with
        [Authorize]
        public ActionResult SharedWith(int id)
        {

            var sharedSensorUsers = this.sensorService.GetUsersSharedSensor(id);

            if (this.User.Identity.GetUserId() != sharedSensorUsers.OwnerId)
            {
                return View("NotAutheticated");
            }

            var sharedUsersViewModel = new SensorViewModel()
            {
                Id = sharedSensorUsers.Id,
                SharedWithUsers = sharedSensorUsers.SharedWithUsers
            };

            return this.View("SharedWith", sharedUsersViewModel);
        }

        [Authorize]
        public ActionResult ShowDetails(int id)
        {
                       
            var sensor = this.sensorService.GetUserSensorById(id);

            if (this.User.Identity.GetUserId() != sensor.OwnerId)
            {
                return View("NotAutheticated");
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


        public ActionResult PublicSensors()
        {
            var sensors = this.sensorService.GetAllPublicSensors()
             .Select(sensor => new SensorViewModel
             {
                 CurrentValue = sensor.CurrentValue,
                 Id = sensor.Id,
                 CreatorUsername = sensor.Owner,
                 Name = sensor.Name,
                 Description = sensor.Description,
                 MeasurementType = sensor.MeasurementType,
                 IsConnected = sensor.IsConnected,
                 IsValueType = !sensor.IsBoolType,
                 MinValue = sensor.MinValue,
                 MaxValue = sensor.MaxValue

             }).ToList();

            return View(sensors);
        }

        
        public ActionResult CheckLowerRange(double MinValue, double? LowestValue, double? HighestValue)
        {
            var inRange = MinValue <= HighestValue && MinValue >= LowestValue;
            return Json(inRange, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckUpperRange(double MaxValue, double? LowestValue, double? HighestValue)
        {
            var inRange = MaxValue >= LowestValue && MaxValue <= HighestValue;
            return Json(inRange, JsonRequestBehavior.AllowGet);
        }
    }
}