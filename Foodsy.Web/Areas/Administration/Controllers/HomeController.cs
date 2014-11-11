using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Foodsy.Web.Areas.Administration.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Navigation()
        {
            return View();
        }
    }
}