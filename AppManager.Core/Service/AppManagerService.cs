using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using AppManager.Core.Interfaces;
using AppManager.Data.Access.Interfaces;
using AppManager.Data.Entity;
using static System.String;

namespace AppManager.Core.Service
{
    public class AppManagerService : IAppManagerService
    {
        private readonly IUnitOfWork _uow;
        private readonly IIISServerManagerService _iiisServerManagerService;
        private bool hasFieldToUpdate;

        public AppManagerService(IUnitOfWork uow, IIISServerManagerService iiisServerManagerService)
        {
            _uow = uow;
            _iiisServerManagerService = iiisServerManagerService;
        }

        public void Parse()
        {
            var foundWebSites = _iiisServerManagerService.ListWebSites();
            if (foundWebSites == null || !foundWebSites.Any())
                return;

            var ctx = _uow.DbContext;
            var foundIds = foundWebSites.Select(e => (int)e.IISId);

            var existingOnes = ctx.IISWebSite.Where(e => foundIds.Contains(e.IISWebSiteId) && e.Enddate == null);
            var idsExistingOnes = existingOnes.Select(e => e.IISWebSiteId);
            var newOnes = foundIds.Except(existingOnes.Select(e => e.IISWebSiteId));
            var toDelete = ctx.IISWebSite.Where(s => !idsExistingOnes.Contains(s.IISWebSiteId) && s.Enddate != null);


            foreach (var iisWebSite in toDelete)
            {
                iisWebSite.Enddate = DateTime.Now;
            }

            var newIisWebSites = new List<IISWebSite>();
            foreach (var idfoundIisWebSite in newOnes)
            {
                var foundIisWebsite = foundWebSites.First(s => s.IISId == idfoundIisWebSite);

                newIisWebSites.Add(new IISWebSite
                {
                    Namewebsite = foundIisWebsite.Namewebsite,
                    Apppollname = foundIisWebsite.Apppollname,
                    Creationdate = DateTime.Now,
                    IISWebSiteId = (int)foundIisWebsite.IISId,
                    Iislogpath = foundIisWebsite.IISLogPath
                });
            }

            ctx.IISWebSite.AddRange(newIisWebSites);

            foreach (var iisWebSite in existingOnes)
            {
                var foundIisWebsite = foundWebSites.First(s => s.IISId == iisWebSite.IISWebSiteId);
                hasFieldToUpdate = foundIisWebsite.Namewebsite != iisWebSite.Namewebsite ||
                          foundIisWebsite.Apppollname != iisWebSite.Apppollname;
                if (!hasFieldToUpdate) continue;

                iisWebSite.Apppollname = foundIisWebsite.Apppollname;
                iisWebSite.Namewebsite = foundIisWebsite.Namewebsite;
            }

            ctx.SaveChanges();
            //var newWebSites = foundWebSites.Where(s => !ctx.IISWebSite.Any(ws => ws.IISWebSiteId == s.IISId));

            //var foundIisWebSitesToRecord = newWebSites.ToList();
            //var newRecords = foundIisWebSitesToRecord.Select(e => new IISWebSite
            //{
            //    Namewebsite = e.Namewebsite,
            //    Apppollname = e.Apppollname,
            //    Creationdate = DateTime.Now,
            //    IISWebSiteId = (int)e.IISId,
            //    Iislogpath = e.IISLogPath
            //});


            //var recordsToUpdate = ctx.IISWebSite.Where(s =>
            //    foundWebSites.Any(fws => fws.IISId == s.IISWebSiteId && !string.Equals(s.Namewebsite, fws.Namewebsite,
            //                                 StringComparison.InvariantCultureIgnoreCase)) && s.Enddate != null)
            //                .ToList()
            //    .ForEach(s =>
            //    {
            //        s.Namewebsite = 
            //    });

            //foreach (var iisWebSite in newRecords)
            //    ctx.IISWebSite.Add(iisWebSite);

            //ctx.SaveChanges();
        }
    }
}
