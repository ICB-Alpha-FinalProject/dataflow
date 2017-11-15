using Dataflow.DataServices.Contracts;
using Dataflow.DataServices.Models;
using DataflowICB.Areas.Admin.Models;
using DataflowICB.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DataflowICB.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationUserManager userManager;
        private readonly IUserServices userServices;
        private readonly ISensorService sensorServices;

        public AdminController(ApplicationUserManager userManager, IUserServices userServices, ISensorService sensorServices)
        {
            this.userManager = userManager;
            this.userServices = userServices;
            this.sensorServices = sensorServices;
        }

        public ActionResult AllUsers()
        {
            var applicationUserModel = this.userServices.GetAllUsers();

            List<UserViewModel> usersViewModel = UserViewModel.Convert(applicationUserModel).ToList();
            
            return this.View(usersViewModel);
        }

        public ActionResult AllSensors()
        {
            var sensorDataModel = this.sensorServices.GetAllSensors(true);
            List<AdminSensorViewModel> sensorViewModel = AdminSensorViewModel.Convert(sensorDataModel).ToList();

            return this.View(sensorViewModel);
        }

        public async Task<ActionResult> EditUser(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);
            var userViewModel = UserViewModel.Create.Compile()(user);
            userViewModel.IsAdmin = await this.userManager.IsInRoleAsync(user.Id, "Admin");

            return this.PartialView("_EditUser", userViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUser(UserViewModel userViewModel)
        {
            if (userViewModel.IsAdmin)
            {
                await this.userManager.AddToRoleAsync(userViewModel.Id, "Admin");
            }
            else
            {
                await this.userManager.RemoveFromRoleAsync(userViewModel.Id, "Admin");
            }

            this.userServices.EditUser(new UserDataModel
            {
                Id = userViewModel.Id,
                Username = userViewModel.Username,
                Email = userViewModel.Email,
                IsDeleted = userViewModel.IsDeleted
            });

            return this.RedirectToAction("AllUsers");
        }

        public ActionResult EditSensor(int id)
        {
            var sensor = this.sensorServices.GetAdminSensorById(id);
            var adminSensorViewModel = AdminSensorViewModel.Convert(sensor);

            return this.PartialView("_EditSensor", adminSensorViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSensor(AdminSensorViewModel adminSensorViewModel)
        {
            this.sensorServices.EditSensor(new SensorDataModel
            {
                Id = adminSensorViewModel.Id,
                Name = adminSensorViewModel.Name,
                MeasurementType = adminSensorViewModel.MeasurementType,
                Description = adminSensorViewModel.Description,
                URL = adminSensorViewModel.URL,
                PollingInterval = adminSensorViewModel.PollingInterval,
                MinValue = adminSensorViewModel.MinValue,
                MaxValue = adminSensorViewModel.MaxValue,
                IsPublic = adminSensorViewModel.IsPublic,
                IsDeleted = adminSensorViewModel.IsDeleted,
                IsBoolType = adminSensorViewModel.IsBoolType
                //IsShared = adminSensorViewModel.IsShared
            });           

            return this.RedirectToAction("AllSensors");
        }

        public ActionResult AdminPanel()
        {
            return this.View();
        }
    }
}