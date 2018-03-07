using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AppManager.Core.Interfaces;
using AppManager.Core.Messages;
using AppManager.Data.Entity;
using Microsoft.Web.Administration;

namespace AppManager.Core.Service
{
    public class IISServerManagerService : IIISServerManagerService
    {
        private readonly ServerManager _serverManager;

        public IISServerManagerService()
        {
            try
            {
                _serverManager = new ServerManager();
            }
            catch (Exception e)
            {
                throw new Exception(Message.MSG01, e);
            }
        }
        public IISServerManagerService(string applicationHostConfigurationPath)
        {
            try
            {
                _serverManager = new ServerManager(true, applicationHostConfigurationPath);
            }
            catch (Exception e)
            {
                throw new Exception(Message.MSG01, e);
            }
        }

        /// <summary>
        /// Lista todos os sites do IIS encontrados no server
        /// </summary>
        /// <returns></returns>
        public ICollection<FoundIISWebSite> ListWebSites()
        {
            var sites = _serverManager.Sites;
            if (sites == null) return new List<FoundIISWebSite>();

            return sites.AsQueryable().Select(e => new FoundIISWebSite
            {
                IISId = e.Id,
                Namewebsite = e.Name,
                Apppollname = e.ApplicationDefaults.ApplicationPoolName,
                IISLogPath = e.LogFile.Directory,
            }).ToList();
        }

        public ICollection<IISApplication> ListApplications()
        {
            throw new NotImplementedException();
        }
    }
}