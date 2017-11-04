using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataflowICB.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        public AdminController()
        {

        }

        public ActionResult AdminPanel()
        {
            return this.View();
        }
    }
}