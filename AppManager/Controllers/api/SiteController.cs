using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AppManager.Core.Servico;
using AppManager.Data.Entity;

namespace AppManager.Controllers.api
{
    /// <summary>
    /// Operacoes relativas a site
    /// </summary>
    public class SiteController : ApiController
    {
        private readonly ISiteService _siteService;

        public SiteController(ISiteService siteService)
        {
            _siteService = siteService;
        }
        /// <summary>
        /// Lista os sites no servidor configurado
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IISWebsite> Get()
        {
            return _siteService.ListAllSites().ToList();
        }

        /// <summary>
        /// Obtem ums site pelo id
        /// </summary>
        /// <param name="id">Id do site</param>
        /// <returns>Site encontrado</returns>
        public string Get(int id)
        {
            return "value";
        }
    }
}