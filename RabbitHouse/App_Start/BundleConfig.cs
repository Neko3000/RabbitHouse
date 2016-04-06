using System.Web;
using System.Web.Optimization;

namespace RabbitHouse
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/sitebehavior").Include(
                      "~/Scripts/site-behavior.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryUI").Include(
                      "~/Scripts/jquery-ui-1.11.4.min.js"));


            bundles.Add(new StyleBundle("~/Content/fontawesome").Include(
                     "~/Content/css/font-awesome.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                    "~/Content/bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/Content/style").Include(
                      "~/Content/SiteStyle.min.css", new CssRewriteUrlTransform()));

            //bracket
            bundles.Add(new StyleBundle("~/Content/bracket/MainStyle").Include(                
                "~/Content/bracket/css/bootstrap.min.css",
                "~/Content/bracket/css/bootstrap-override.css",
                "~/Content/bracket/css/weather-icons.min.css",
                "~/Content/bracket/css/jquery-ui-1.10.3.css",
                "~/Content/bracket/css/font-awesome.min.css",
                "~/Content/bracket/css/toggles.css",
                "~/Content/bracket/css/select2.css",
                "~/Content/bracket/css/lato.css",
                "~/Content/bracket/css/roboto.css",
                "~/Content/bracket/css/style.default.css"
                ));

            bundles.Add(new StyleBundle("~/Content/bracket/GeneralFormStyle").Include(
                "~/Content/bracket/css/bootstrap-timepicker.min.css",
                "~/Content/bracket/css/jquery.tagsinput.css",
                "~/Content/bracket/css/colorpicker.css",
                "~/Content/bracket/css/dropzone.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/bracket/MainScript").Include(
                      "~/Content/bracket/js/jquery-1.11.1.min.js",
                      "~/Content/bracket/js/jquery-migrate-1.2.1.min.js",
                      "~/Content/bracket/js/jquery-ui-1.10.3.min.js",
                      "~/Content/bracket/js/bootstrap.min.js",
                      "~/Content/bracket/js/modernizr.min.js",
                      "~/Content/bracket/js/jquery.sparkline.min.js",
                      "~/Content/bracket/js/toggles.min.js",
                      "~/Content/bracket/js/retina.min.js",
                      "~/Content/bracket/js/jquery.cookies.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/bracket/GeneralFormScript").Include(
                      "~/Content/bracket/js/jquery.autogrow-textarea.js",
                      "~/Content/bracket/js/bootstrap-timepicker.min.js",
                      "~/Content/bracket/js/jquery.maskedinput.min.js",
                      "~/Content/bracket/js/jquery.tagsinput.min.js",
                      "~/Content/bracket/js/jquery.mousewheel.js",
                      "~/Content/bracket/js/select2.min.js",
                      "~/Content/bracket/js/dropzone.min.js",
                      "~/Content/bracket/js/colorpicker.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/bracket/TableScript").Include(
                      "~/Content/bracket/js/jquery.datatables.min.js",
                      "~/Content/bracket/js/select2.min.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/bracket/CustomScript").Include(
                      "~/Content/bracket/js/custom.js" 
                      ));
        }
    }
}
