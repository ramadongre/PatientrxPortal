using System.Web;
using System.Web.Optimization;

namespace WebUI.PatientRxPortal
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatablejs").Include(
                       "~/Scripts/DataTables/jquery.dataTables.min.js.js",
                       "~/Scripts/DataTables/dataTables.bootstrap4.min.js"
                       ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*", "~/Scripts/_extensions.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/custom").Include(
                       "~/Scripts/jquery-ui-{version}.js",
                       "~/Scripts/jquery.mask.js",
                       "~/Scripts/custom.js"));
                       

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/themes/base/jquery-ui.min.css"));

            bundles.Add(new StyleBundle("~/Content/FontAwesome").Include(
                "~/Content/font-awesome.min.css"));
        }
    }
}
