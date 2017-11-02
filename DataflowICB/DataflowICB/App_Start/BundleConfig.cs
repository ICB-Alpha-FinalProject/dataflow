using System.Web;
using System.Web.Optimization;

namespace DataflowICB
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/vendor-css").Include(
                    "~/Content/vendor/bootstrap/css/bootstra*",
                    "~/Content/vendor/font-awesome/css/font-awesome*"

                ));

            bundles.Add(new ScriptBundle("~/bundles/vendor-js").Include(
                    "~/Content/vendor/jquery/jquery*",
                    "~/Content/vendor/bootstrap/js/bootstrap.bundle*",
                    "~/Content/vendor/jquery-easing/jquery.easing*",
                    "~/Scripts/sb-admin*"
                ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js",
            //          "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/sb-admin.css"));   
            
        }
    }
}
