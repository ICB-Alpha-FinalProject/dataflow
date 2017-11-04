using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataflowICB.Controllers
{
    public class SensorController : Controller
    {
        public SensorController()
        {
        }

        public ActionResult RegisterSensor()
        {

            return this.View();
        }
    }
}