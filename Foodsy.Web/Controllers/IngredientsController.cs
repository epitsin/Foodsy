using Foodsy.Data;
using Foodsy.Web.ViewModels.Ingredients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;

namespace Foodsy.Web.Controllers
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