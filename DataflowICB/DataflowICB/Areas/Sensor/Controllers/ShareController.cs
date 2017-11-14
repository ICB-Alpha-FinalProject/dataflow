using Bytes2you.Validation;
using Dataflow.DataServices.Contracts;
using Dataflow.Services.Contracts;
using DataflowICB.Areas.Sensor.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DataflowICB.Areas.Sensor.Controllers
{

    public class ShareController : Controller
    {
        private readonly ISensorService sensorService;
        private readonly IHttpClientProvider httpClient;

        public ShareController(ISensorService sensorService, IHttpClientProvider httpClient)
        {
            Guard.WhenArgument(sensorService, "sensorService").IsNull().Throw();
            Guard.WhenArgument(httpClient, "httpClient").IsNull().Throw();

            this.httpClient = httpClient;
            this.sensorService = sensorService;
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
    }
}