using Foodsy.Data;
using Foodsy.Web.ViewModels.Ingredients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Foodsy.Web.Controllers
{
    public class IngredientsController : Controller
    {
        private IFoodsyData data;

        public IngredientsController(IFoodsyData data)
        {
            this.data = data;
        }

        public ActionResult IngredientDetails(int? id)
        {
            var ingredient = this.data.Ingredients.Find(id);
            var viewModel = new IngredientViewModel
            {
                Name = ingredient.Name,
                Carbohydrates = ingredient.Carbohydrates,
                Proteins = ingredient.Proteins,
                Fats = ingredient.Fats
            };

            return View(viewModel);
        }
    }
}