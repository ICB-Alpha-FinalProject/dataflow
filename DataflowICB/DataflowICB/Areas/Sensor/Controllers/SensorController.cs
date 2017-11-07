using DataflowICB.Areas.Sensor.Models;
using DataflowICB.Attributes;
using DataflowICB.Models.DataApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DataflowICB.Areas.Sensor.Controllers
{
    public class SensorController : Controller
    {
        public SensorController()
        {
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
        public ActionResult CreateBoolSensor(BoolTypeSensorViewModel model)
        {
            return this.Content(model.ToString());
        }

        [Authorize]
        [AjaxOnly]
        public ActionResult GetProperRegView(string sensorId, bool isValueType)
        {
            var sensorVm = new SensorViewModel()
            {
                Url = "http://telerikacademy.icb.com/api/sensor/" + sensorId,
                IsValueType = isValueType
            };

            if (sensorVm.IsValueType)
            {
                var valueTypeSensorVm = new ValueTypeSensorViewModel()
                {
                    Sensor = sensorVm
                };

                return this.View("RegisterValueSensor", valueTypeSensorVm);
            }
            else
            {
                var boolSensorVm = new BoolTypeSensorViewModel()
                {
                    Sensor = sensorVm
                };

                return this.View("RegisterBoolSensor", boolSensorVm);
            }
        }
    }
}