using System;
using System.Collections.Generic;
using System.Linq;
using AppManager.Core.Interfaces;
using AppManager.Data.Entity;
using Microsoft.Web.Administration;

namespace AppManager.Core.Service
{
    public class IISIiisServerManagerService : IIISServerManagerService
    {
        private readonly ServerManager _serverManager;

        public IISIiisServerManagerService()
        {
            _serverManager = new ServerManager();
        }
        public IISIiisServerManagerService(string applicationHostConfigurationPath)
        {
            _serverManager = new ServerManager(true, applicationHostConfigurationPath);
        }

        public ICollection<FoundIISWebSite> ListWebSites()
        {
            var sites = _serverManager.Sites.AsQueryable();

            return sites.AsQueryable().Select(e => new FoundIISWebSite()
            {
                IISId = e.Id,
                Namewebsite = e.Name,
                Apppollname= e.ApplicationDefaults.ApplicationPoolName,
            }).ToList();
        }

        public ICollection<IISApplication> ListApplications()
        {
            throw new NotImplementedException();
        }
    }
}