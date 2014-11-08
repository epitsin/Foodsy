using Foodsy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Foodsy.Web.Controllers
{
    public class ArticlesController : Controller
    {
        private IFoodsyData data;

        public ArticlesController(IFoodsyData data)
        {
            this.data = data;
        }

        public ActionResult AllArticles()
        {
            var articles = this.data.Articles.All().ToList();
            return View(articles);
        }
        public ActionResult ArticleDetails()
        {
            return View();
        }
    }
}