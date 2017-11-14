using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataflowICB.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Default()
        {
            return View();
        }

        public ActionResult Custom404()
        {
            return View();
        }

        public ActionResult Custom403()
        {
            return View();
        }
    }
}