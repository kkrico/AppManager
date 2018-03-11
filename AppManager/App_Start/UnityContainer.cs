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
using AppManager.Hubs;
using AppManager.Infra.IOC;
using Microsoft.AspNet.SignalR;
using Microsoft.Practices.Unity;
using static System.Web.HttpContext;

namespace AppManager
{
    /// <summary>
    ///     The unity IOC Container
    /// </summary>
    public class UnityContainer
    {
        private static readonly Dictionary<Type, HashSet<Type>> InternalTypeMapping =
            new Dictionary<Type, HashSet<Type>>();

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
        ///     Unity container
        /// </summary>
        public static IUnityContainer Instance => Container.Value;


        /// <summary>
        ///     Register all types on the app
        /// </summary>
        /// <param name="container"></param>
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<AppManagerDbContext>(new PerRequestLifetimeManager(),
                new InjectionConstructor("DefaultConnection"));
            container.RegisterType<IUnitOfWork, UnitOfWork>(new PerRequestLifetimeManager());

            RegisterHubs(container);
            RegisterServerManager(container);
            RegisterAppManager(container);
        }

        private static void RegisterHubs(IUnityContainer container)
        {
            // TODO: Mudar para reflection
            RegisterHubFor<ParseStatusHub>(container);
            RegisterHubFor<LogHub>(container);
        }

        private static void RegisterHubFor<T>(IUnityContainer container) where T : Hub
        {
            container.RegisterType<IHubContext>(typeof(T).Name,
                new InjectionFactory(c => GlobalHost.ConnectionManager.GetHubContext<T>()));
        }

        private static void RegisterAppManager(IUnityContainer container)
        {
            container.RegisterType<AppManagerController>(new InjectionConstructor
                (
                    new ResolvedParameter<IAppManagerService>(),
                    new ResolvedParameter<IHubContext>(nameof(ParseStatusHub))
                ));
        }

        private static void RegisterServerManager(IUnityContainer container)
        {
            if (string.IsNullOrEmpty(AppManagerConfig.ApplicationHostConfigFileLocation))
                container.RegisterType<IIISServerManagerService, IISServerManagerService>(
                    new PerThreadLifetimeManager());
            else
                container.RegisterType<IIISServerManagerService, IISServerManagerService>(
                    new PerThreadLifetimeManager(),
                    new InjectionConstructor(AppManagerConfig.ApplicationHostConfigFileLocation));
        }

        private static void RegisterConventions(Microsoft.Practices.Unity.UnityContainer container,
            IEnumerable<Assembly> assemblies = null)
        {
            foreach (var type in GetClassesFromAssemblies(assemblies))
            {
                IEnumerable<Type> interfacesToBeRegsitered = GetInterfacesToBeRegistered(type);
                AddToInternalTypeMapping(type, interfacesToBeRegsitered);
            }


            foreach (KeyValuePair<Type, HashSet<Type>> typeMapping in InternalTypeMapping)
                if (typeMapping.Value.Count == 1)
                {
                    var type = typeMapping.Value.First();
                    container.RegisterType(typeMapping.Key, type);
                }
                else
                {
                    foreach (var type in typeMapping.Value)
                        container.RegisterType(typeMapping.Key, type, GetNameForRegistration(type));
                }
        }

        private static void AddToInternalTypeMapping(Type type, IEnumerable<Type> interfacesOnType)
        {
            foreach (var interfaceOnType in interfacesOnType)
            {
                if (!InternalTypeMapping.ContainsKey(interfaceOnType))
                    InternalTypeMapping[interfaceOnType] = new HashSet<Type>();

                InternalTypeMapping[interfaceOnType].Add(type);
            }
        }

        private static IEnumerable<Type> GetInterfacesToBeRegistered(Type type)
        {
            List<Type> allInterfacesOnType = type.GetInterfaces()
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
            IEnumerable<Type> allClasses = assemblies != null
                ? AllClasses.FromAssemblies(assemblies)
                : AllClasses.FromAssembliesInBasePath();
            return
                allClasses.Where(
                    n =>
                        n.Namespace != null);
        }
    }

    public class PerRequestLifetimeManager : LifetimeManager
    {
        private readonly object _lifetimeKey = new object();

        public override object GetValue()
        {
            return UnityPerRequestHttpModule.GetValue(_lifetimeKey);
        }

        public override void SetValue(object newValue)
        {
            UnityPerRequestHttpModule.SetValue(_lifetimeKey, newValue);
        }

        public override void RemoveValue()
        {
            var disposable = GetValue() as IDisposable;

            disposable?.Dispose();

            UnityPerRequestHttpModule.SetValue(_lifetimeKey, null);
        }
    }

    public class UnityPerRequestHttpModule : IHttpModule
    {
        private static readonly object ModuleKey = new object();

        /// <summary>
        ///     Disposes the resources used by this module.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        ///     Initializes a module and prepares it to handle requests.
        /// </summary>
        /// <param name="context">
        ///     An <see cref="HttpApplication" /> that provides access to the methods, properties,
        ///     and events common to all application objects within an ASP.NET application.
        /// </param>
        public void Init(HttpApplication context)
        {
            (context ?? throw new ArgumentNullException(nameof(context))).EndRequest += OnEndRequest;
        }

        internal static object GetValue(object lifetimeManagerKey)
        {
            Dictionary<object, object> dict = GetDictionary(Current);

            if (dict == null) return null;
            return dict.TryGetValue(lifetimeManagerKey, out var obj) ? obj : null;
        }

        internal static void SetValue(object lifetimeManagerKey, object value)
        {
            Dictionary<object, object> dict = GetDictionary(Current);

            if (dict == null)
            {
                dict = new Dictionary<object, object>();

                Current.Items[ModuleKey] = dict;
            }

            dict[lifetimeManagerKey] = value;
        }

        private void OnEndRequest(object sender, EventArgs e)
        {
            var app = (HttpApplication)sender;

            Dictionary<object, object> dict = GetDictionary(app.Context);

            if (dict == null) return;
            foreach (var disposable in dict.Values.OfType<IDisposable>())
                disposable.Dispose();
        }

        private static Dictionary<object, object> GetDictionary(HttpContext context)
        {
            if (context == null)
                throw new InvalidOperationException(
                    "The PerRequestLifetimeManager can only be used in the context of an HTTP request.Possible causes for this error are using the lifetime manager on a non-ASP.NET application, or using it in a thread that is not associated with the appropriate synchronization context.");

            var dict = (Dictionary<object, object>)context.Items[ModuleKey];

            return dict;
        }
    }
}