using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Foodsy.Web.Controllers
{
    public class ArticlesController : Controller
    {
        // GET: Articles
        public ActionResult AllArticles()
        {
            return View();
        }
        public ActionResult ArticleDetails()
        {
            return View();
        }
    }
}