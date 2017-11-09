using Dataflow.DataServices.Contracts;
using DataflowICB.Areas.Admin.Models;
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

        public AdminController(ApplicationUserManager userManager, IUserServices services)
        {
            this.userManager = userManager;
            this.services = services;
        }

        public ActionResult AllUsers()
        {
            var usersViewModel = this.services
                .GetAllUsers();

            return this.View(usersViewModel);
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

            return this.RedirectToAction("AllUsers");
        }

        public ActionResult AdminPanel()
        {
            return this.View();
        }
    }
}