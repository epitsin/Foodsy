using Foodsy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Foodsy.Web.Areas.Recipes.ViewModels.Ingredients;
using Foodsy.Web.Controllers;

namespace Foodsy.Web.Areas.Recipes.Controllers
{
    public class IngredientsController : BaseController
    {
        public IngredientsController(IFoodsyData data)
            : base(data)
        {
        }

        public ActionResult IngredientDetails(int? id)
        {
            var allIngredienits = this.Data.Ingredients.All().AsQueryable().Project().To<IngredientViewModel>();
            var ingredient = allIngredienits.FirstOrDefault(x=>x.Id == id);
            //var viewModel = new IngredientViewModel
            //{
            //    Name = ingredient.Name,
            //    Carbohydrates = ingredient.Carbohydrates,
            //    Proteins = ingredient.Proteins,
            //    Fats = ingredient.Fats
            //};

            return View(ingredient);
        }
    }
}