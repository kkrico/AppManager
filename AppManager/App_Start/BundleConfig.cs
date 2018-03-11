using System.Web.Optimization;

namespace AppManager
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            const string appName = "appManager";
            bundles.Add(new StyleBundle("~/css")
                .Include("~/Styles/bootstrap/css/bootstrap.css")
                .Include("~/Styles/bootstrap/css/darkly.css")
                .Include("~/Styles/site.css"));

            bundles.Add(new ScriptBundle("~/js")
                .Include("~/Scripts/angular/angular.js")
                .Include("~/Scripts/angular/angular-route.js")
                .Include("~/Scripts/angular/angular-resource.js")
                .Include("~/Scripts/angular/angular-animate.js")
                .Include("~/Scripts/angular/angular-touch.js")
                .Include("~/Scripts/jquery/jquery-1.6.4.js")
                .Include("~/Scripts/sginalr/jquery.signalR-1.2.2.js")
                .Include("~/Scripts/ui-bootstrap/ui-bootstrap-tpls-2.5.0.js")
                .Include($"~/Scripts/{appName}/*.js")
                .Include($"~/Scripts/{appName}/controllers/*.js")
                .Include($"~/Scripts/{appName}/services/*.js")
                .Include($"~/Scripts/{appName}/directives/*.js")
                .Include($"~/Scripts/*.js")
            );
        }
    }
}