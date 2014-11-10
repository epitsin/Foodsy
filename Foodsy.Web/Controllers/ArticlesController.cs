using Foodsy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Foodsy.Web.Controllers
{
    public class ArticlesController : BaseController
    {
        private const int PageSize = 2;

        public ArticlesController(IFoodsyData data)
            : base(data)
        {
        }

        public ActionResult AllArticles(int? id)
        {
            int pageNumber = id.GetValueOrDefault(1);
            var allArticles = this.Data.Articles.All().OrderBy(x => x.Id);
            var articles = allArticles.Skip((pageNumber - 1) * PageSize).Take(PageSize);
            ViewBag.Pages = Math.Ceiling((double)allArticles.Count() / PageSize);
            ;
            return View(articles);
        }

        public ActionResult ArticleDetails()
        {
            return View();
        }
    }
}