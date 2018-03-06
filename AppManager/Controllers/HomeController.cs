using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using AppManager.Core.Interfaces;

namespace AppManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly IIISWebSiteService _iiisWebSiteService;
        private readonly IIISServerManagerService _iiisServerManagerService;

        public HomeController(IIISWebSiteService iiisWebSiteService, IIISServerManagerService iiisServerManagerService)
        {
            _iiisWebSiteService = iiisWebSiteService;
            _iiisServerManagerService = iiisServerManagerService;
        }
        
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}