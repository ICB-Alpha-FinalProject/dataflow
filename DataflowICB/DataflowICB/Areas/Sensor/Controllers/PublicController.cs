using Bytes2you.Validation;
using Dataflow.DataServices.Contracts;
using Dataflow.Services.Contracts;
using DataflowICB.Areas.Sensor.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataflowICB.Areas.Sensor.Controllers
{
    public class PublicController : Controller
    {
        private readonly ISensorService sensorService;
        private readonly IHttpClientProvider httpClient;

        public PublicController(ISensorService sensorService, IHttpClientProvider httpClient)
        {
            Guard.WhenArgument(sensorService, "sensorService").IsNull().Throw();
            Guard.WhenArgument(httpClient, "httpClient").IsNull().Throw();

            this.httpClient = httpClient;
            this.sensorService = sensorService;
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
                 MaxValue = sensor.MaxValue,
                 IsReadOnly = true

             }).ToList();

            return View(sensors);
        }

        public ActionResult ShowDetails(int id)
        {
            
            var sensor = this.sensorService.GetUserSensorById(id);
            
            if (sensor == null || !sensor.IsPublic)
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
                IsShared = sensor.IsShared,
                IsReadOnly = true
            };

            return this.View("ShowDetails", sensorViewModel);
        }



    }
}