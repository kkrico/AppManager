using System;
using System.Data;
using System.Data.Entity;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AppManager.Data.Access;
using Microsoft.Practices.Unity;

namespace AppManager
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            AppManagerConfig.RegisterRunMode();
            RouteTable.Routes.MapHubs();
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            IOCConfig.RegisterContainers(ControllerBuilder.Current);
            IOCConfig.RegisterFilters(FilterProviders.Providers);
            PerformanceEngineConfig.RegisterPerformanceMonitor();
        }
    }
}