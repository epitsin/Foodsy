namespace Foodsy.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using Foodsy.Data;
    using Foodsy.Web.Areas.Administration.Controllers.Base;
    using Foodsy.Web.Areas.Administration.ViewModels;

    using Kendo.Mvc.UI;

    using Model = Foodsy.Data.Models.Recipe;
    using ViewModel = Foodsy.Web.Areas.Administration.ViewModels.RecipeViewModel;

    public class RecipesAdminController : KendoGridAdministrationController
    {
        public RecipesAdminController(IFoodsyData data)
            : base(data)
        {
        }

        public ActionResult AllRecipes()
        {
            return View();
        }

        protected override IEnumerable GetData()
        {
            return this.Data.Recipes.All().Project().To<RecipeViewModel>();
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Recipes.Find(id) as T;
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
                this.Data.Recipes.Delete(model.Id);
                this.Data.SaveChanges();
            }

            return this.GridOperation(model, request);
        }
    }
}