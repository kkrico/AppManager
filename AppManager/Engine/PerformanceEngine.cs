using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using AppManager.Core.Servico;
using AppManager.Hubs;
using Microsoft.AspNet.SignalR;
using Microsoft.Practices.Unity;

namespace AppManager.Engine
{
    /// <summary>
    /// Performance Engine  ¯\_(ツ)_/¯
    /// </summary>
    public class PerformanceEngine
    {
        private readonly IHubContext _hub;
        private static readonly Lazy<PerformanceEngine> Lazy =
            new Lazy<PerformanceEngine>(() => new PerformanceEngine());

        private ISiteService _siteService;

        private PerformanceEngine()
        {
            _siteService = UnityConfig.GetConfiguredContainer().Resolve<ISiteService>();

            _hub = GlobalHost.ConnectionManager.GetHubContext<LogHub>();
            var fileSystemWatcher = new FileSystemWatcher()
            {
                Filter = "*.xml",
                Path = "C:\\AppLogs\\",
                IncludeSubdirectories = true,
                NotifyFilter = NotifyFilters.LastAccess | 
                               NotifyFilters.LastWrite | 
                               NotifyFilters.FileName | 
                               NotifyFilters.DirectoryName
            };
            fileSystemWatcher.EnableRaisingEvents = true;

            fileSystemWatcher.Changed += OnChanged;
            fileSystemWatcher.Created += Created;
            fileSystemWatcher.Deleted += Deleted;
            fileSystemWatcher.Renamed += OnRenamed;
        }

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


        public static PerformanceEngine Instance
        {
            get { return Lazy.Value; }
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