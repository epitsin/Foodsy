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

    using Model = Foodsy.Data.Models.Ingredient;
    using ViewModel = Foodsy.Web.Areas.Administration.ViewModels.IngredientViewModel;

    public class IngredientsAdminController : KendoGridAdministrationController
    {
        public IngredientsAdminController(IFoodsyData data)
            : base(data)
        {
        }

        public ActionResult AllIngredients()
        {
            return View();
        }

        protected override IEnumerable GetData()
        {
            return this.Data.Ingredients.All().Project().To<IngredientViewModel>();
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Ingredients.Find(id) as T;
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
                this.Data.Ingredients.Delete(model.Id);
                this.Data.SaveChanges();
            }

            return this.GridOperation(model, request);
        }
    }
}