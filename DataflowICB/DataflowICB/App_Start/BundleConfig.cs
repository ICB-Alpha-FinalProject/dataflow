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

            bundles.Add(new ScriptBundle("~/bundles/jqueryajax").Include(
                      "~/Scripts/jquery.unobtrusive-ajax*"));

            bundles.Add(new StyleBundle("~/bundles/vendor-css").Include(
                    "~/Content/vendor/bootstrap/css/bootstra*",
                    "~/Content/vendor/font-awesome/css/font-awesome*"

                ));

            bundles.Add(new ScriptBundle("~/bundles/vendor-js").Include(
                    "~/Content/vendor/jquery/jquery*",
                    "~/Content/vendor/bootstrap/js/bootstrap.bundle*",
                    "~/Content/vendor/jquery-easing/jquery.easing*",
                    "~/Scripts/sb-admin*"
                ));

            bundles.Add(new ScriptBundle("~/bundles/mvcfoolproof").Include(
                    "~/Client Scripts/mvcfoolproof.unobtrusive.js",
                    "~/Client Scripts/mvcfoolproof.unobtrusive.min.js",
                    "~/Client Scripts/MvcFoolproofJQueryValidation.js",
                    "~/Client Scripts/MvcFoolproofJQueryValidation.min.js",
                    "~/Client Scripts/MvcFoolproofValidation.js",
                    "~/Client Scripts/MvcFoolproofValidation.min.js"));
            

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/sb-admin.css",
                      "~/Content/Site.css"
                      ));

            bundles.Add(new StyleBundle("~/bundles/wizard-css").Include(
                        "~/Content/wizard/css/gsdk-bootstrap-wizard.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/wizard-js").Include(
                      "~/Content/wizard/js/jquery.bootstrap.wizard.js",
                      "~/Content/wizard/js/gsdk-bootstrap-wizard.js",
                      "~/Content/wizard/js/bootstrap.min.js",
                      "~/Content/wizard/js/jquery.validate.min.js",
                      "~/Content/wizard/js/jquery-2.2.4.min.js"
              ));

            bundles.Add(new StyleBundle("~/bundles/tables-css").Include(
                "~/Content/vendor/bootstrap/css/bootstrap-*",
                "~/Content/vendor/datatables/dataTables.bootstrap4.css"));

            bundles.Add(new ScriptBundle("~/bundles/tables-js").Include(
                "~/Content/vendor/datatables/dataTables.bootstrap4.js",
                "~/Content/vendor/datatables/jquery.dataTables.js",
                "~/Content/tables/js/sb-admin-*"
               ));

        }
    }
}
