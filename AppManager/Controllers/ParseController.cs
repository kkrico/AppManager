using System.Web.Mvc;

namespace AppManager.Controllers
{
    public class ParseController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}