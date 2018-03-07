using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using AppManager.Core.Interfaces;
using AppManager.Data.Access.Interfaces;
using AppManager.Data.Entity;

namespace AppManager.Core.Service
{
    public class AppManagerService : IAppManagerService
    {
        private readonly IUnitOfWork _uow;
        private readonly IIISServerManagerService _iiisServerManagerService;

        public AppManagerService(IUnitOfWork uow, IIISServerManagerService iiisServerManagerService)
        {
            _uow = uow;
            _iiisServerManagerService = iiisServerManagerService;
        }

        public void Parse()
        {
            var foundWebSites = _iiisServerManagerService.ListWebSites();
            if (foundWebSites == null)
                throw new InvalidOperationException();

            var ctx = _uow.DbContext;

            var newWebSites = from fws in foundWebSites
                              join s in ctx.IISWebSite on fws.IISId equals s.IISWebSiteId into siteGroup
                              from d in siteGroup.DefaultIfEmpty()
                              select fws;

            var foundIisWebSitesToRecord = newWebSites.ToList();

            var newRecords = foundIisWebSitesToRecord.Select(e => new IISWebSite()
            {
                Namewebsite = e.Namewebsite,
                Apppollname = e.Apppollname,
                Creationdate = DateTime.Now,
                IISWebSiteId = (int) e.IISId,
                Iislogpath = e.IISLogPath
            });

            foreach (var iisWebSite in newRecords)
            {
                ctx.IISWebSite.Add(iisWebSite);
            }
            ctx.SaveChanges();
        }
    }

    public interface IAppManagerService
    {
        void Parse();
    }
}
