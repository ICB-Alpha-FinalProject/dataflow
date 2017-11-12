using DataflowICB.Database;
using DataflowICB.Database.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;


namespace DataflowICB
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

            System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());

        }

        public override string GetVaryByCustomString(HttpContext context, string custom)
        {
            if (custom.Equals("User", StringComparison.InvariantCultureIgnoreCase))
            {
                var user = context.User.Identity.Name;
                return "User=" + user;
            }

            return base.GetVaryByCustomString(context, custom);
        }
    }
}
