using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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