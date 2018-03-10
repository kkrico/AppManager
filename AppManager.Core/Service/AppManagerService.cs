using System;
using System.Collections.Generic;
using System.Linq;
using AppManager.Core.Interfaces;
using AppManager.Data.Access;
using AppManager.Data.Access.Interfaces;
using AppManager.Data.Entity;
using JetBrains.Annotations;

namespace AppManager.Core.Service
{
    public class AppManagerService : IAppManagerService
    {
        private readonly AppManagerDbContext _ctx;
        private readonly IIISServerManagerService _iiisServerManagerService;
        private readonly IUnitOfWork _uow;

        public AppManagerService([NotNull] IUnitOfWork uow, [NotNull] IIISServerManagerService iiisServerManagerService)
        {
            _uow = uow;
            _ctx = uow.DbContext;
            _iiisServerManagerService = iiisServerManagerService;
        }

        public void Parse()
        {
            ICollection<FoundIISWebSite> foundWebSites = _iiisServerManagerService.ListWebSites();
            if (foundWebSites == null || !foundWebSites.Any())
                return;

            ParseWebSites(foundWebSites);
        }

        public event NotifyEntityHandler OnEntityParsed;

        /// <summary>
        ///     Faz parse dos sites encontrados
        /// </summary>
        /// <param name="foundWebSites"></param>
        private void ParseWebSites([NotNull] ICollection<FoundIISWebSite> foundWebSites)
        {
            RaiseOnEntityParsed<IISWebSite>(nameof(IISWebSite));
            IQueryable<IISWebSite> webSitesThatAlreadyExist = ListIISWebSitesThatAlreadyExist(foundWebSites);
            IQueryable<IISWebSite> toDelete = ListIISWebSitesToDelete(foundWebSites);

            MarkToDeleteIISWebSites(toDelete);

            UpdateIISWebSites(foundWebSites, webSitesThatAlreadyExist);

            InsertNewIISWebSites(foundWebSites);
            ParseApplications(foundWebSites);

            _ctx.SaveChanges();
        }

        private static void ParseApplications(ICollection<FoundIISWebSite> foundWebSites)
        {
        }

        /// <summary>
        ///     Insere os sites do IIS de acordo com os sites encontrados
        /// </summary>
        /// <param name="foundWebSites"></param>
        private void InsertNewIISWebSites(IEnumerable<FoundIISWebSite> foundWebSites)
        {
            IEnumerable<IISWebSite> newWebSitesToSave = GenerateIISWebSite(foundWebSites);
            _ctx.IISWebSite.AddRange(newWebSitesToSave);


        }

        /// <summary>
        ///     Atualiza os Sites do IIS de acordo com o sites encontrados
        /// </summary>
        /// <param name="foundWebSites"></param>
        /// <param name="webSitesThatAlreadyExist"></param>
        private static void UpdateIISWebSites(ICollection<FoundIISWebSite> foundWebSites,
            IQueryable<IISWebSite> webSitesThatAlreadyExist)
        {
            foreach (var iisWebSite in webSitesThatAlreadyExist)
            {
                var foundIisWebsite = foundWebSites.First(s => s.IISId == iisWebSite.IISWebSiteId);
                var hasFieldToUpdate = HasFieldToUpdate(foundIisWebsite, iisWebSite);
                if (!hasFieldToUpdate) continue;

                iisWebSite.Apppollname = foundIisWebsite.Apppollname;
                iisWebSite.Namewebsite = foundIisWebsite.Namewebsite;
            }
        }

        /// <summary>
        ///     Lista os sites que estão para deleção
        /// </summary>
        /// <param name="foundWebSites"></param>
        /// <returns></returns>
        private IQueryable<IISWebSite> ListIISWebSitesToDelete([NotNull] IEnumerable<FoundIISWebSite> foundWebSites)
        {
            IEnumerable<int> idsOfFoundIISWebSites = GetIdsOfFoundIISWebSites(foundWebSites);
            return _ctx.IISWebSite.Where(s => !idsOfFoundIISWebSites.Contains(s.IISWebSiteId) && s.Enddate == null);
        }

        /// <summary>
        ///     Lista os IIS WebSites que já existem
        /// </summary>
        /// <param name="foundWebSites"></param>
        /// <returns></returns>
        private IQueryable<IISWebSite> ListIISWebSitesThatAlreadyExist(
            [NotNull] IEnumerable<FoundIISWebSite> foundWebSites)
        {
            IEnumerable<int> idsOfFoundIISWebSites = GetIdsOfFoundIISWebSites(foundWebSites);
            return _ctx.IISWebSite.Where(e => idsOfFoundIISWebSites.Contains(e.IISWebSiteId) && e.Enddate == null);
        }

        /// <summary>
        ///     Lista os Ids dos Sites Encontrados
        /// </summary>
        /// <param name="foundWebSites"></param>
        /// <returns></returns>
        private static IEnumerable<int> GetIdsOfFoundIISWebSites(IEnumerable<FoundIISWebSite> foundWebSites)
        {
            return foundWebSites.Select(e => (int)e.IISId);
        }

        /// <summary>
        ///     Gera os IIS Websites para inserção
        /// </summary>
        /// <param name="foundIISWebSites"></param>
        /// <returns></returns>
        private IEnumerable<IISWebSite> GenerateIISWebSite([NotNull] IEnumerable<FoundIISWebSite> foundIISWebSites)
        {
            var newIisWebSites = new List<IISWebSite>();
            IEnumerable<int> idsOfFoundIISWebSites = GetIdsOfFoundIISWebSites(foundIISWebSites);
            IEnumerable<IISWebSite> webSitesThatAlreadyExist = ListIISWebSitesThatAlreadyExist(foundIISWebSites);
            IEnumerable<int> newOnes =
                idsOfFoundIISWebSites.Except(webSitesThatAlreadyExist.Select(e => e.IISWebSiteId));

            foreach (var idfoundIisWebSite in newOnes)
            {
                var foundIisWebsite = foundIISWebSites.First(s => s.IISId == idfoundIisWebSite);
                var newIISWebSite = new IISWebSite
                {
                    Namewebsite = foundIisWebsite.Namewebsite,
                    Apppollname = foundIisWebsite.Apppollname,
                    Creationdate = DateTime.Now,
                    IISWebSiteId = (int)foundIisWebsite.IISId,
                    Iislogpath = foundIisWebsite.IISLogPath,
                    PhysicalPath = foundIisWebsite.PhysicalPath
                };
                newIisWebSites.Add(newIISWebSite);

                InsertNewIISApplicationsFor(newIISWebSite, foundIisWebsite);
            }

            return newIisWebSites;
        }

        private void InsertNewIISApplicationsFor([NotNull]IISWebSite newIISWebSite, [NotNull]FoundIISWebSite foundIisWebsite)
        {
            
        }

        /// <summary>
        ///     Notifica quem usa esta serviço que a entidade que está sendo feito o parse
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityName"></param>
        private void RaiseOnEntityParsed<T>(string entityName)
        {
            OnEntityParsed?.Invoke(entityName, typeof(T));
        }

        /// <summary>
        ///     Fecha a vigencia dos IIS Websites especificados
        /// </summary>
        /// <param name="toDelete"></param>
        private void MarkToDeleteIISWebSites(IQueryable<IISWebSite> toDelete)
        {
            foreach (var iisWebSite in toDelete)
            {
                iisWebSite.Enddate = DateTime.Now;
                MarkToDeleteIISApplicationsFromWebSite(iisWebSite);
            }
        }

        /// <summary>
        ///     Marca para fechar a vigencia das aplicações especificadas
        /// </summary>
        /// <param name="iisWebSite"></param>
        private void MarkToDeleteIISApplicationsFromWebSite([NotNull] IISWebSite iisWebSite)
        {
            IQueryable<IISApplication> applications =
                _uow.IISApplicationRepository.List().Where(e => e.Idiiswebsite == iisWebSite.IISWebSiteId);

            MarkToDeleteIISApplications(applications);
        }

        /// <summary>
        ///     Fecha a vigencia das applications especificadas
        /// </summary>
        /// <param name="applications"></param>
        private static void MarkToDeleteIISApplications([NotNull] IEnumerable<IISApplication> applications)
        {
            foreach (var application in applications)
                application.Enddate = DateTime.Now;
        }

        /// <summary>
        ///     Verifica se há campo para atualização
        /// </summary>
        /// <param name="foundIisWebsite"></param>
        /// <param name="iisWebSite"></param>
        /// <returns></returns>
        private static bool HasFieldToUpdate([NotNull] FoundIISWebSite foundIisWebsite, [NotNull] IISWebSite iisWebSite)
        {
            return !string.Equals(foundIisWebsite.Namewebsite?.Trim(), iisWebSite.Namewebsite?.Trim(),
                       StringComparison.InvariantCultureIgnoreCase) ||
                   !string.Equals(foundIisWebsite.PhysicalPath?.Trim(), iisWebSite.PhysicalPath?.Trim(),
                       StringComparison.InvariantCultureIgnoreCase) ||
                   !string.Equals(foundIisWebsite.Apppollname?.Trim(), iisWebSite.Apppollname?.Trim(),
                       StringComparison.InvariantCultureIgnoreCase);
        }
    }
}