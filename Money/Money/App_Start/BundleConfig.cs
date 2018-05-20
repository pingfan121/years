using System.Web;
using System.Web.Optimization;

namespace Money
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/JsLib/UtilLib.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                "~/JsLib/DataTables/media/js/jquery.dataTables.js"));   //表格js


            //日历
            bundles.Add(new ScriptBundle("~/bundles/datepicker").Include(
              "~/JsLib/DatePicker/WdatePicker.js"));   //表格js

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/JsLib/DataTables/media/css/jquery.dataTables.min.css"   //datatables 表格样式
                      ));

            

           
        }
    }
}
