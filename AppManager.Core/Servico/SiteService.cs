using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppManager.Data.Access;
using AppManager.Data.Entity;

namespace AppManager.Core.Servico
{
    public class SiteService : ISiteService
    {
        private readonly IUnitOfWork _uow;
        private readonly IServerManager _serverManager;

        public SiteService(IUnitOfWork uow, IServerManager serverManager)
        {
            _uow = uow;
            _serverManager = serverManager;
        }

        public ICollection<IISWebsite> ListAllSites()
        {
            var foundSites = _serverManager.ListAllSites();
            var result = new List<IISWebsite>();
            if (foundSites == null) return result;

            return foundSites;
        }

        public IISWebsite GetSite(int siteId)
        {
            throw new NotImplementedException();
        }

        public ICollection<string> ListarUrlsAppLog()
        {
            throw new NotImplementedException();
        }
    }
}
