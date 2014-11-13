namespace Foodsy.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

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
            var allArticles = this.Data.Articles.All().Project().To<ArticleViewModel>().OrderBy(x => x.CreatedOn);
            ViewBag.RecentArticles = allArticles.Take(3);

            var tags = this.Data.Tags.All().OrderBy(x => x.Articles.Count);
            ViewBag.Tags = tags.Take(6);

            var articles = allArticles.Skip((pageNumber - 1) * PageSize).Take(PageSize).ToList();
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
        public ActionResult Search(string text)
        {
            var articlesFound = this.Data.Articles.All().Where(x => x.Title.Contains(text) || x.Text.Contains(text)).Project().To<ArticleViewModel>().ToList();

            return PartialView("_AllArticlesPartial", articlesFound);
        }
    }
}