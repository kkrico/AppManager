using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.Practices.Unity;

namespace AppManager.Infra.IOC
{
    public class UnityFilterAttributeFilterProvider : FilterAttributeFilterProvider
    {
        private readonly IUnityContainer _container;

        public UnityFilterAttributeFilterProvider(IUnityContainer container)
        {
            _container = container;
        }

        protected override IEnumerable<FilterAttribute> GetActionAttributes(ControllerContext controllerContext,
            ActionDescriptor actionDescriptor)
        {
            IEnumerable<FilterAttribute> list = base.GetActionAttributes(controllerContext, actionDescriptor);

            foreach (var item in list) _container.BuildUp(item.GetType(), item);

            return list;
        }

        protected override IEnumerable<FilterAttribute> GetControllerAttributes(ControllerContext controllerContext,
            ActionDescriptor actionDescriptor)
        {
            IEnumerable<FilterAttribute> list = base.GetControllerAttributes(controllerContext, actionDescriptor);

            foreach (var item in list) _container.BuildUp(item.GetType(), item);

            return list;
        }
    }
}