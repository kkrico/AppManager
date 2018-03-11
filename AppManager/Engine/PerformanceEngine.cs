using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AppManager.Core.Interfaces;
using AppManager.Data.Access;
using AppManager.Data.Access.Interfaces;
using AppManager.Hubs;
using Microsoft.AspNet.SignalR;
using Microsoft.Practices.Unity;

namespace AppManager.Engine
{
    /// <summary>
    ///     Performance Engine  ¯\_(ツ)_/¯
    /// </summary>
    public class PerformanceEngine
    {
        private static readonly Lazy<PerformanceEngine> InstanceHolder =
            new Lazy<PerformanceEngine>(() => new PerformanceEngine());

        private readonly IHubContext _hub;

        private IIISWebSiteService _iiisWebSiteService;

        private PerformanceEngine()
        {
            _iiisWebSiteService = UnityContainer.Instance.Resolve<IIISWebSiteService>(
                new DependencyOverride(typeof(IUnitOfWork),
                    new UnitOfWork(new AppManagerDbContext("DefaulConnection"))));
            _hub = UnityContainer.Instance.Resolve<IHubContext>(nameof(LogHub));


            //var fileSystemWatcher = new FileSystemWatcher
            //{
            //    Filter = "*.xml",
            //    Path = "C:\\AppLogs\\",
            //    IncludeSubdirectories = true,
            //    NotifyFilter = NotifyFilters.LastAccess |
            //                   NotifyFilters.LastWrite |
            //                   NotifyFilters.FileName |
            //                   NotifyFilters.DirectoryName,
            //    EnableRaisingEvents = true
            //};

            //fileSystemWatcher.Changed += OnChanged;
            //fileSystemWatcher.Created += Created;
            //fileSystemWatcher.Deleted += Deleted;
            //fileSystemWatcher.Renamed += OnRenamed;
        }


        public static PerformanceEngine Instance => InstanceHolder.Value;

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            _hub.Clients.All.doAlgo("Changed");
        }

        private void Created(object sender, FileSystemEventArgs e)
        {
            _hub.Clients.All.doAlgo("Created");
        }

        private void Deleted(object sender, FileSystemEventArgs e)
        {
            _hub.Clients.All.doAlgo("Deleted");
        }

        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            _hub.Clients.All.doAlgo("Renamed");
        }

        public Task IniciarMonitoramento()
        {
            var rand = new Random();
            while (true)
            {
                Thread.Sleep(5000);
                var n = rand.Next(0, 100);
                _hub.Clients.All.doAlgo(n);
            }
        }
    }
}