﻿namespace Foodsy.Web.Areas.Shop.Controllers
{

    using System;
    using System.Collections;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using Kendo.Mvc.UI;

    using Foodsy.Data;
    using Foodsy.Web.Areas.Administration.Controllers.Base;
    using Foodsy.Web.Areas.Shop.ViewModels;

    using Model = Foodsy.Data.Models.RecipeShoppingCart;
    using ViewModel = Foodsy.Web.Areas.Shop.ViewModels.RecipeViewModel;

    public class ShoppingCartController : KendoGridAdministrationController
    {
        public ShoppingCartController(IFoodsyData data)
            :base(data)
        {

        }

        //public ActionResult AllRecipes()
        //{
        //    var recipes = this.CurrentUser.ShoppingCart.RecipeShoppingCarts
        //        .ToList()
        //        .AsQueryable()
        //        .Project()
        //        .To<RecipeViewModel>();

        //    return View(recipes);
        //}
        public ActionResult AllRecipes()
        {
            return View();
        }

        protected override IEnumerable GetData()
        {
            return this.CurrentUser.ShoppingCart.RecipeShoppingCarts.AsQueryable().Project().To<RecipeViewModel>();
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Recipes.Find(id) as T;
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
                this.Data.Recipes.Delete(model.Id);
                this.Data.SaveChanges();
            }

            return this.GridOperation(model, request);
        }
    }
}