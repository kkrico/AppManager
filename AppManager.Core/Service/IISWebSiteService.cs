using System.Collections.Generic;
using System.Linq;
using AppManager.Core.Interfaces;
using AppManager.Data.Access.Interfaces;
using AppManager.Data.Entity;

namespace AppManager.Core.Service
{
    public class IISWebSiteService : IIISWebSiteService
    {
        private readonly IIISServerManagerService _iiisServerManagerService;
        private readonly IUnitOfWork _uow;

        public IISWebSiteService(IUnitOfWork uow, IIISServerManagerService iiisServerManagerService)
        {
            _uow = uow;
            _iiisServerManagerService = iiisServerManagerService;
        }

        public ICollection<IISWebSite> ListAllSites()
        {
            IQueryable<IISWebSite> sites = _uow.IISWebSiteRepository.List();
            return sites?.ToList() ?? new List<IISWebSite>();
        }
    }
}