using System.Web.Mvc;

namespace DataflowICB.Areas.Sensor
{
    public class SensorAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Sensor";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.Routes.LowercaseUrls = true;

            context.MapRoute(
                "Sensor_default",
                "Sensor/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}