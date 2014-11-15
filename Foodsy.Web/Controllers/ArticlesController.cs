namespace Foodsy.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using Foodsy.Common;
    using Foodsy.Data;
    using Foodsy.Web.ViewModels.Articles;
    using Foodsy.Web.ViewModels.Tags;

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
                .OrderByDescending(x => x.CreatedOn)
                .Project()
                .To<ArticleViewModel>();

            var articles = allArticles
                .Skip((pageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToList();
            ViewBag.Pages = Math.Ceiling((double)allArticles.Count() / PageSize);

            return View(articles);
        }

        [HttpGet]
        [ChildActionOnly]
        [OutputCache(Duration = 1800)]
        public ActionResult RecentArticles()
        {
            var articles = this.Data.Articles
                .All()
                .OrderByDescending(x => x.CreatedOn)
                .Take(3)
                .Project()
                .To<ArticleViewModel>();

            return PartialView("_RecentPostsPartial", articles);
        }

        [HttpGet]
        [ChildActionOnly]
        [OutputCache(Duration = 1800)]
        public ActionResult Tags()
        {
            var tags = this.Data.Tags
                .All()
                .OrderBy(x => x.Articles.Count)
                .Take(12)
                .Project()
                .To<TagViewModel>();

            return PartialView("_TagsPartial", tags);
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