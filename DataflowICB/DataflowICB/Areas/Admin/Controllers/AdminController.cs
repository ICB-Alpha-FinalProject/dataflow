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
    public class AdminController : Controller
    {
        private readonly ApplicationUserManager userManager;
        private readonly IUserServices services;
        private readonly ISensorService sensorService;

        public AdminController(ApplicationUserManager userManager, IUserServices services, ISensorService sensorService)
        {
            this.userManager = userManager;
            this.services = services;
            this.sensorService = sensorService;
        }

        public ActionResult AllUsers()
        {
            var applicationUserModel = this.services.GetAllUsers();

            List<UserViewModel> usersViewModel = UserViewModel.Convert(applicationUserModel).ToList();
            
            return this.View(usersViewModel);
        }

        public ActionResult AllSensors()
        {
            var sensorDataModel = this.sensorService.GetAllSensors(true);

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

            this.services.EditUser(new UserDataModel
            {
                Id = userViewModel.Id,
                Username = userViewModel.Username,
                Email = userViewModel.Email,
                IsDeleted = userViewModel.IsDeleted
            });

            return this.RedirectToAction("AllUsers");
        }

        public ActionResult AdminPanel()
        {
            return this.View();
        }
    }
}