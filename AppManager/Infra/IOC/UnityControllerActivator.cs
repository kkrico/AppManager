using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Microsoft.Practices.Unity;

namespace AppManager.Infra.IOC
{
    public class UnityControllerActivator : IHttpControllerActivator
    {
        private readonly IUnityContainer _container;
 
        public UnityControllerActivator(IUnityContainer container)
        {
            _container = container;
        }
 
        public IHttpController Create(
            HttpRequestMessage request,
            HttpControllerDescriptor controllerDescriptor,
            Type controllerType)
        {
            var controller =
                (IHttpController)_container.Resolve(controllerType);
 
            return controller;
        }
    }
}