namespace Foodsy.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using Foodsy.Common;
    using Foodsy.Data;
    using Foodsy.Web.ViewModels.Articles;

    public class ArticlesController : BaseController
    {
        private const int PageSize = 2;

        public ArticlesController(IFoodsyData data)
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult AllArticles(int? id)
        {
            int pageNumber = id.GetValueOrDefault(1);
            var allArticles = this.Data.Articles
                .All()
                .Project()
                .To<ArticleViewModel>()
                .OrderByDescending(x => x.CreatedOn);
            ViewBag.RecentArticles = allArticles.Take(3);

            var tags = this.Data.Tags
                .All()
                .OrderBy(x => x.Articles.Count);
            ViewBag.Tags = tags.Take(12);

            var articles = allArticles
                .Skip((pageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToList();
            ViewBag.Pages = Math.Ceiling((double)allArticles.Count() / PageSize);

            return View(articles);
        }

        [HttpGet]
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(string text)
        {
            var articlesFound = this.Data.Articles
                .All()
                .Where(x => x.Title.Contains(text) || x.Text.Contains(text))
                .Project()
                .To<ArticleViewModel>()
                .ToList();

            if (articlesFound.Count == 0)
            {
                return Content(GlobalContants.NoArticles);
            }

            return PartialView("_AllArticlesPartial", articlesFound);
        }

        [HttpPost]
        public ActionResult Sort(string tagName)
        {
            var articlesFound = this.Data.Articles
                .All()
                .Where(x => x.Tags.Any(y => y.Name == tagName))
                .Project()
                .To<ArticleViewModel>()
                .ToList();

            if (articlesFound.Count == 0)
            {
                return Content(GlobalContants.NoArticles);
            }

            return PartialView("_AllArticlesPartial", articlesFound);
        }
    }
}