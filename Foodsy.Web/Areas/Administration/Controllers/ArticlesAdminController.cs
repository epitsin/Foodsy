namespace Foodsy.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using Kendo.Mvc.UI;

    using Foodsy.Data;
    using Foodsy.Data.Models;
    using Foodsy.Web.Areas.Administration.Controllers.Base;
    using Foodsy.Web.Areas.Administration.ViewModels;

    using Model = Foodsy.Data.Models.Article;
    using ViewModel = Foodsy.Web.Areas.Administration.ViewModels.ArticleViewModel;

    public class ArticlesAdminController : KendoGridAdministrationController
    {
        public ArticlesAdminController(IFoodsyData data)
            : base(data)
        {
        }

        public ActionResult AllArticles()
        {
            return View();
        }

        protected override IEnumerable GetData()
        {
            return this.Data.Articles.All().Project().To<ArticleViewModel>();
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Articles.Find(id) as T;
        }
        
        [HttpPost]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            var dbModel = base.Create<Model>(model);
            if (dbModel != null)
                model.Id = dbModel.Id;
            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            base.Update<Model, ViewModel>(model, model.Id);
            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                this.Data.Articles.Delete(model.Id);
                this.Data.SaveChanges();
            }

            return this.GridOperation(model, request);
        }

        private void GetTagsForArticle(ArticleViewModel article, Article newArticle)
        {
            var tagNames = Regex.Split(article.Title, @"\W+").ToList();

            foreach (var tag in tagNames)
            {
                if (!this.Data.Tags.All().Any(x => x.Name == tag.ToLower()))
                {
                    var newTag = new Tag { Name = tag.ToLower() };
                    newTag.Articles.Add(newArticle);
                    this.Data.Tags.Add(newTag);
                }
                else
                {
                    this.Data.Tags.All().FirstOrDefault(x => x.Name == tag.ToLower()).Articles.Add(newArticle);
                }
            }
        }
    }
}