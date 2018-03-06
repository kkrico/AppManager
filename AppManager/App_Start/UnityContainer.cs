using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using AppManager.Controllers.api;
using AppManager.Core.Interfaces;
using AppManager.Core.Service;
using AppManager.Data.Access;
using AppManager.Data.Access.Interfaces;
using AppManager.Data.Entity;
using AppManager.Hubs;
using AppManager.Infra.IOC;
using Microsoft.AspNet.SignalR;
using Microsoft.Practices.Unity;
using static System.Web.HttpContext;

namespace AppManager
{
    /// <summary>
    /// The unity IOC Container
    /// </summary>
    public class UnityContainer
    {
        private static readonly Dictionary<Type, HashSet<Type>> InternalTypeMapping = new Dictionary<Type, HashSet<Type>>();

        #region Unity Container
        private static readonly Lazy<IUnityContainer> Container = new Lazy<IUnityContainer>(() =>
        {
            var container = new Microsoft.Practices.Unity.UnityContainer();
            RegisterConventions(container);
            RegisterTypes(container);
            return container;
        });
        #endregion

        /// <summary>
        /// Unity container
        /// </summary>
        public static IUnityContainer Instance => Container.Value;


        /// <summary>
        /// Register all types on the app
        /// </summary>
        /// <param name="container"></param>
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<AppManagerDbContext, AppManagerDbContext>(
                new InjectionConstructor("DefaultConnection"));
            container.RegisterType<IUnitOfWork, UnitOfWork>(new PerThreadLifetimeManager());

            ResolveServerManager(container);
            ResolveAppManager(container);

            container.RegisterType<IAppManagerService, AppManagerService>();
            
        }

        private static void ResolveAppManager(IUnityContainer container)
        {
            var parseStatusHub = GlobalHost.ConnectionManager.GetHubContext<ParseStatusHub>();
            container.RegisterType<AppManagerController>(new InjectionConstructor(container.Resolve<IAppManagerService>(), parseStatusHub));
        }

        private static void ResolveServerManager(IUnityContainer container)
        {
            if (string.IsNullOrEmpty(AppManagerConfig.ApplicationHostConfigFileLocation))
                container.RegisterType<IIISServerManagerService, IISIiisServerManagerService>();
            else
                container.RegisterType<IIISServerManagerService, IISIiisServerManagerService>(new InjectionConstructor(AppManagerConfig.ApplicationHostConfigFileLocation));
        }

        private static void RegisterConventions(Microsoft.Practices.Unity.UnityContainer container, IEnumerable<Assembly> assemblies = null)
        {
            foreach (var type in GetClassesFromAssemblies(assemblies))
            {
                var interfacesToBeRegsitered = GetInterfacesToBeRegistered(type);
                AddToInternalTypeMapping(type, interfacesToBeRegsitered);
            }


            foreach (var typeMapping in InternalTypeMapping)
            {
                if (typeMapping.Value.Count == 1)
                {
                    var type = typeMapping.Value.First();
                    container.RegisterType(typeMapping.Key, type);
                }
                else
                {
                    foreach (var type in typeMapping.Value)
                    {
                        container.RegisterType(typeMapping.Key, type, GetNameForRegistration(type));
                    }
                }
            }
        }

        private static void AddToInternalTypeMapping(Type type, IEnumerable<Type> interfacesOnType)
        {
            foreach (var interfaceOnType in interfacesOnType)
            {
                if (!InternalTypeMapping.ContainsKey(interfaceOnType))
                {
                    InternalTypeMapping[interfaceOnType] = new HashSet<Type>();
                }

                InternalTypeMapping[interfaceOnType].Add(type);
            }
        }

        private static IEnumerable<Type> GetInterfacesToBeRegistered(Type type)
        {
            var allInterfacesOnType = type.GetInterfaces()
                .Select(i => i.IsGenericType ? i.GetGenericTypeDefinition() : i).ToList();

            return allInterfacesOnType.Except(allInterfacesOnType.SelectMany(i => i.GetInterfaces())).ToList();
        }

        private static string GetNameForRegistration(Type type)
        {
            var name = type.Name;

            return name;
        }

        private static IEnumerable<Type> GetClassesFromAssemblies(IEnumerable<Assembly> assemblies = null)
        {
            var allClasses = assemblies != null ? AllClasses.FromAssemblies(assemblies) : AllClasses.FromAssembliesInBasePath();
            return
                allClasses.Where(
                    n =>
                        n.Namespace != null);
        }
    }
}
