using Bytes2you.Validation;
using Dataflow.DataServices.Contracts;
using Dataflow.Services.Contracts;
using DataflowICB.Areas.Sensor.Models;
using DataflowICB.Attributes;
using DataflowICB.Database.Models;
using DataflowICB.Models.DataApi;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using SensorApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DataflowICB.Areas.Sensor.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly ISensorService sensorService;
        private readonly IHttpClientProvider httpClient;

        public RegistrationController(ISensorService sensorService, IHttpClientProvider httpClient)
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
                var isValueType = !sensor.Description.Contains("false");
                sensor.IsValueType = isValueType;

                if (isValueType)
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

                return this.Json("Successfully Registered !");
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
    }
}