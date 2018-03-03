using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using AppManager.Data.Entity;
using Microsoft.Web.Administration;

namespace AppManager.Core.Servico
{
    public class IISServerManager : IServerManager
    {
        private readonly ServerManager _serverManager;

        public IISServerManager(string applicationHostConfigurationPath)
        {
            _serverManager = new ServerManager(applicationHostConfigurationPath);
        }

        public ICollection<IISWebsite> ListAllSites()
        {
            return _serverManager.Sites.AsQueryable().Select(e => new IISWebsite
            {
                Idiiswebsite = (int)e.Id,
                Namewebsite = e.Name,
            }).ToList();
        }
    }
}