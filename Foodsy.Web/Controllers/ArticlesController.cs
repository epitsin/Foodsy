using Foodsy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Foodsy.Web.ViewModels.Articles;

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
            var allArticles = this.Data.Articles.All().OrderBy(x => x.CreatedOn);
            var articles = allArticles.Skip((pageNumber - 1) * PageSize).Take(PageSize).ToList();
            ViewBag.Pages = Math.Ceiling((double)allArticles.Count() / PageSize);
            return View(articles);
        }

        public ActionResult ArticleDetails(int id)
        {
            var article = this.Data.Articles
                .All()
                .AsQueryable()
                .Where(x => x.Id == id)
                .Project()
                .To<ArticleViewModel>()
                .FirstOrDefault();
            return View(article);
        }
    }
}