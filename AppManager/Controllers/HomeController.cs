using System.Web.Mvc;
using AppManager.Core.Interfaces;

namespace AppManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly IIISServerManagerService _iiisServerManagerService;
        private readonly IIISWebSiteService _iiisWebSiteService;

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