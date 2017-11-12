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

namespace DataflowICB.Areas.Sensor.Controllers
{
    public class SensorController : Controller
    {

        private readonly ISensorService sensorService;

        public SensorController(ISensorService sensorService)
        {
            // validation

            this.sensorService = sensorService;
        }

        [Authorize]
        public async Task<ActionResult> RegisterSensor()
        {
            // TODO: depdency inverse HttpClient
            using (HttpClient hc = new HttpClient())
            {
                hc.DefaultRequestHeaders.Add("auth-token", "8e4c46fe-5e1d-4382-b7fc-19541f7bf3b0");
                var resp = await hc.GetAsync("http://telerikacademy.icb.bg/api/sensor/all");

                resp.EnsureSuccessStatusCode();

                string content = await resp.Content.ReadAsStringAsync();

                var resViewModel = JsonConvert.DeserializeObject<List<SensorApiData>>(content);

                foreach (var sensor in resViewModel)
                {
                    var isValueTYpe = !sensor.Description.Contains("false");

                    sensor.IsValueType = isValueTYpe;
                }

                return this.View(resViewModel);
            }
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
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

                if (model.BoolTypeSensor != null)
                {
                    var boolType = new BoolTypeSensor()
                    {
                        CurrentValue = model.BoolTypeSensor.CurrentValue,
                        MeasurementType = model.MeasurementType
                    };
                    sensor.IsBoolType = true;
                    sensor.BoolTypeSensor = boolType;
                }

                if (model.ValueTypeSensor != null)
                {
                    var valueType = new ValueTypeSensor()
                    {
                        MeasurementType = model.MeasurementType,
                        CurrentValue = model.ValueTypeSensor.CurrentValue,
                        IsInAcceptableRange = model.ValueTypeSensor.IsInAcceptableRange,
                        Maxvalue = model.ValueTypeSensor.Maxvalue,
                        MinValue = model.ValueTypeSensor.MinValue
                    };

                    sensor.ValueTypeSensor = valueType;
                }

                this.sensorService.AddSensor(sensor);

                return this.RedirectToAction("Index", "Home", new { area = "" });
            }
            else
            {
                ModelState.AddModelError("keyName", "Form is not valid");
                return View("RegisterValueSensor", model);
            }
        }

        [Authorize]
        [AjaxOnly]
        public ActionResult GetProperRegView(string sensorId, bool isValueType, string measureType)
        {

            var sensorVm = new SensorViewModel()
            {
                Url = "http://telerikacademy.icb.bg/api/sensor/" + sensorId,
                IsValueType = isValueType,
                CreatorUsername = this.HttpContext.User.Identity.Name,
                MeasurementType = measureType
            };

            if (isValueType)
            {
                var valueTypeSensorVm = new ValueTypeSensorViewModel();

                sensorVm.ValueTypeSensor = valueTypeSensorVm;
                return this.PartialView("RegisterValueSensor", sensorVm);
            }
            else
            {
                var boolSensorVm = new BoolTypeSensorViewModel();

                sensorVm.BoolTypeSensor = boolSensorVm;
                return this.PartialView("RegisterBoolSensor", sensorVm);
            }
        }

        public async Task<ActionResult> UpdateSensors()
        {
            await this.sensorService.UpdateSensors();
            //return this.RedirectToAction("Index", "Home", new { area = "" });
            return new EmptyResult();
        }

        [Authorize]
        public ActionResult SensorHistoryGraph(string sensorId)
        {
            return this.View();
        }

        [AjaxOnly]
        [Authorize]
        public JsonResult GetHistoryDataForSensor(int sensorId)
        {
            var sensors = this.sensorService.HistoryDataForValueSensorsById(sensorId);
            var serialized = JsonConvert.SerializeObject(sensors);

            return this.Json(serialized, JsonRequestBehavior.AllowGet);
        }

        //CACHING ???
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
                IsPublic = sensor.IsPublic,
                IsShared = sensor.IsShared
            }).ToList();

            return View(sensors);
        }

        [Authorize]
        public ActionResult EditSensor(int id)
        {
            var sensor = this.sensorService.GetSensorById(id);

            var sensorViewModel = new SensorViewModel()
            {
                Id = sensor.Id,
                Name = sensor.Name,
                Description = sensor.Description,
                Url = sensor.URL,
                PollingInterval = sensor.PollingInterval,
                MeasurementType = sensor.MeasurementType,
                IsPublic = sensor.IsPublic,
                IsShared = sensor.IsShared,
                MaxValue = sensor.MaxValue,
                MinValue = sensor.MinValue
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
                Id = viewModel.Id
            });

            return this.RedirectToAction("UserSensors");
        }


        [Authorize]
        public ActionResult ShowDetails()
        {
            return View();
        }

        [Authorize]
        public ActionResult ShareSensor()
        {
            return View();
        }
    }
}