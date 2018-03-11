using System;
using Microsoft.Practices.Unity;

namespace AppManager
{
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
}