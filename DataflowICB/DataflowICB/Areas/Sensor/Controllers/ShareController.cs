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
            var sensor = this.sensorService.GetUserSensorById(id);

            if (this.User.Identity.GetUserId() != sensor.OwnerId)
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

            return this.RedirectToAction("UserSensors", "Sensor");
        }

        [Authorize]
        public ActionResult SharedWith(int id)
        {

            var sharedSensorUsers = this.sensorService.GetUsersSharedSensor(id);

            if (!sharedSensorUsers.SharedWithUsers.Contains(sharedSensorUsers.Owner))
            {
                return this.View("NotAuthenticated");
            }

            var sharedUsersViewModel = new SensorViewModel()
            {
                Id = sharedSensorUsers.Id,
                SharedWithUsers = sharedSensorUsers.SharedWithUsers,
                CreatorUsername = sharedSensorUsers.Owner,

            };

            return this.View("SharedWith", sharedUsersViewModel);
        }

        [Authorize]
        public ActionResult ShowDetails(int id)
        {

            var sensor = this.sensorService.GetUserSensorById(id);

            if (sensor == null)
            {
                return this.View("NotAuthenticated");
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