using Bytes2you.Validation;
using Dataflow.DataServices.Contracts;
using Dataflow.Services.Contracts;
using DataflowICB.Attributes;
using DataflowICB.Models.DataApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DataflowICB.Areas.Sensor.Controllers
{
    public class HistoryController : Controller
    {
        private readonly ISensorService sensorService;
        private readonly IHttpClientProvider httpClient;

        public HistoryController(ISensorService sensorService, IHttpClientProvider httpClient)
        {
            Guard.WhenArgument(sensorService, "sensorService").IsNull().Throw();
            Guard.WhenArgument(httpClient, "httpClient").IsNull().Throw();

            this.httpClient = httpClient;
            this.sensorService = sensorService;
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

        public async Task<ActionResult> UpdateSensors()
        {
            await this.sensorService.UpdateSensors();
            //return this.RedirectToAction("Index", "Home", new { area = "" });
            return new EmptyResult();
        }
    }
}