using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using AppManager.Core.Interfaces;
using AppManager.Data.Access;
using AppManager.Data.Access.Interfaces;
using AppManager.Data.Entity;
using static System.String;

namespace AppManager.Core.Service
{
    public class AppManagerService : IAppManagerService
    {
        private readonly IUnitOfWork _uow;
        private readonly IIISServerManagerService _iiisServerManagerService;
        public event EventHandler OnEntityParse;
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
            ParseWebSites(foundWebSites, ctx);

            ctx.SaveChanges();
        }

        private void ParseWebSites(ICollection<FoundIISWebSite> foundWebSites, AppManagerDbContext ctx)
        {
            OnOnEntityParse<IISWebSite>(nameof(IISWebSite));

            var idsOfFoundIISWebSites = foundWebSites.Select(e => (int) e.IISId);

            var webSitesThatAlreadyExist =
                ctx.IISWebSite.Where(e => idsOfFoundIISWebSites.Contains(e.IISWebSiteId) && e.Enddate == null);
            var newOnes = idsOfFoundIISWebSites.Except(webSitesThatAlreadyExist.Select(e => e.IISWebSiteId));
            var toDelete = ctx.IISWebSite.Where(s => !idsOfFoundIISWebSites.Contains(s.IISWebSiteId) && s.Enddate == null);

            DeleteIISWebSite(toDelete);

            var newIisWebSites = new List<IISWebSite>();
            foreach (var idfoundIisWebSite in newOnes)
            {
                var foundIisWebsite = foundWebSites.First(s => s.IISId == idfoundIisWebSite);

                newIisWebSites.Add(new IISWebSite
                {
                    Namewebsite = foundIisWebsite.Namewebsite,
                    Apppollname = foundIisWebsite.Apppollname,
                    Creationdate = DateTime.Now,
                    IISWebSiteId = (int) foundIisWebsite.IISId,
                    Iislogpath = foundIisWebsite.IISLogPath
                });
            }

            ctx.IISWebSite.AddRange(newIisWebSites);

            foreach (var iisWebSite in webSitesThatAlreadyExist)
            {
                var foundIisWebsite = foundWebSites.First(s => s.IISId == iisWebSite.IISWebSiteId);
                var hasFieldToUpdate = HasFieldToUpdate(foundIisWebsite, iisWebSite);
                if (!hasFieldToUpdate) continue;

                iisWebSite.Apppollname = foundIisWebsite.Apppollname;
                iisWebSite.Namewebsite = foundIisWebsite.Namewebsite;
            }
        }

        private static void DeleteIISWebSite(IQueryable<IISWebSite> toDelete)
        {
            foreach (var iisWebSite in toDelete)
            {
                iisWebSite.Enddate = DateTime.Now;
            }
        }

        private static bool HasFieldToUpdate(FoundIISWebSite foundIisWebsite, IISWebSite iisWebSite)
        {
            if (foundIisWebsite == null) throw new ArgumentNullException(nameof(foundIisWebsite));
            if (iisWebSite == null) throw new ArgumentNullException(nameof(iisWebSite));

            return !string.Equals(foundIisWebsite.Namewebsite?.Trim(), iisWebSite.Namewebsite?.Trim(), StringComparison.InvariantCultureIgnoreCase) ||
                   !string.Equals(foundIisWebsite.Apppollname?.Trim(), iisWebSite.Apppollname?.Trim(), StringComparison.InvariantCultureIgnoreCase);
        }

        protected virtual void OnOnEntityParse<T>(string entityName)
        {
            OnEntityParse?.Invoke(this, EventArgs.Empty);
        }

        public class EntityParseEventArgs: EventArgs
        {
            public string EntityName { get; }
            public Type EntityType { get; }

            public EntityParseEventArgs(string entityName, Type entityType)
            {
                EntityName = entityName;
                EntityType = entityType;
            }
        }
    }
}
