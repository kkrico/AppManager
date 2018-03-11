using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppManager
{
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
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            context.EndRequest += OnEndRequest;
        }

        internal static object GetValue(object lifetimeManagerKey)
        {
            Dictionary<object, object> dict = GetDictionary(HttpContext.Current);

            if (dict == null) return null;
            return dict.TryGetValue(lifetimeManagerKey, out object obj) ? obj : null;
        }

        internal static void SetValue(object lifetimeManagerKey, object value)
        {
            Dictionary<object, object> dict = GetDictionary(HttpContext.Current);

            if (dict == null)
            {
                dict = new Dictionary<object, object>();

                HttpContext.Current.Items[ModuleKey] = dict;
            }

            dict[lifetimeManagerKey] = value;
        }

        private void OnEndRequest(object sender, EventArgs e)
        {
            var app = (HttpApplication) sender;

            Dictionary<object, object> dict = GetDictionary(app.Context);

            if (dict == null) return;
            foreach (IDisposable disposable in dict.Values.OfType<IDisposable>())
                disposable.Dispose();
        }

        private static Dictionary<object, object> GetDictionary(HttpContext context)
        {
            if (context == null)
                throw new InvalidOperationException(
                    "The PerRequestLifetimeManager can only be used in the context of an HTTP request.Possible causes for this error are using the lifetime manager on a non-ASP.NET application, or using it in a thread that is not associated with the appropriate synchronization context.");

            var dict = (Dictionary<object, object>) context.Items[ModuleKey];

            return dict;
        }
    }
}