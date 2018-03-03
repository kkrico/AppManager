using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using AppManager.Core.Servico;

namespace AppManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISiteService _siteService;

        public HomeController(ISiteService siteService)
        {
            _siteService = siteService;
        }
        // GET
        public ActionResult Index()
        {
            return View();
        }
    }
}