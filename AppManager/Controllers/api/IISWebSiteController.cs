using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AppManager.Core.Interfaces;
using AppManager.Data.Entity;

namespace AppManager.Controllers.api
{
    /// <summary>
    ///     Operacoes relativas a site
    /// </summary>
    public class IISWebSiteController : ApiController
    {
        private readonly IIISWebSiteService _iiisWebSiteService;

        public IISWebSiteController(IIISWebSiteService iiisWebSiteService)
        {
            _iiisWebSiteService = iiisWebSiteService;
        }

        /// <summary>
        ///     Lista os sites no servidor configurado
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IISWebSite> Get()
        {
            return _iiisWebSiteService.ListAllSites().ToList();
        }
    }
}