using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using AppManager.Infra.IOC;

namespace AppManager
{
    public class IOCConfig
    {
        public static void RegisterContainers(ControllerBuilder current)
        {
            RegisterContainer(current);
        }

        public static void RegisterFilters(FilterProviderCollection filterProviderCollection)
        {
            GlobalConfiguration.Configuration.Services.Replace(
                typeof(IHttpControllerActivator),
                new UnityControllerActivator(UnityConfig.GetConfiguredContainer()));
            filterProviderCollection.Add(new UnityFilterAttributeFilterProvider(UnityConfig.GetConfiguredContainer()));
        }

        private static void RegisterContainer(ControllerBuilder current)
        {
            var container = UnityConfig.GetConfiguredContainer();

            var controllerFactory = new UnityControllerFactory(container);
            current.SetControllerFactory(controllerFactory);
        }

        public static void DisposeContainers()
        {
            var container = UnityConfig.GetConfiguredContainer();

            container.Dispose();
        }
    }
}