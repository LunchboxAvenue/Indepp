using System.Web;
using System.Web.Optimization;

namespace Indepp
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                      "~/Scripts/jquery-2.1.4.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/editor").Include(
                "~/Scripts/ckeditor/ckeditor.js",
                "~/Scripts/ckeditor/config.js"
                ));
        }
    }
}