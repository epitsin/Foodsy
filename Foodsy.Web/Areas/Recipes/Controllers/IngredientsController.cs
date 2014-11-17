namespace Foodsy.Web.Areas.Recipes.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using Foodsy.Data;
    using Foodsy.Data.Models;
    using Foodsy.Web.Areas.Recipes.ViewModels.Ingredients;
    using Foodsy.Web.Controllers;

    public class IngredientsController : BaseController
    {
        public IngredientsController(IFoodsyData data)
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult IngredientDetails(int? id)
        {
            var ingredient = this.Data.Ingredients
                .All()
                .AsQueryable()
                .Project()
                .To<IngredientViewModel>()
                .FirstOrDefault(x => x.Id == id);
            if (ingredient == null)
            {
                return HttpNotFound();
            }

            return View(ingredient);
        }

        [HttpGet]
        public ActionResult AddIngredients(string recipeName)
        {
            var recipe = this.Data.Recipes
                .All()
                .FirstOrDefault(x => x.Name == recipeName);
            var models = new List<AddIngredientViewModel>();
            ViewBag.RecipeName = recipeName;
            foreach (var ingredient in recipe.RecipeIngredients)
            {
                models.Add(new AddIngredientViewModel
                {
                    Name = ingredient.Ingredient.Name,
                    Quantity = ingredient.Quantity
                });
            }

            return View(models);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddIngredients(List<AddIngredientViewModel> ingredients, string name)
        {
            if (ingredients.Count != 0 && ModelState.IsValid)
            {
                var recipe = this.Data.Recipes
                    .All()
                    .FirstOrDefault(x => x.Name == name);
                foreach (var ingredient in ingredients)
                {
                    var recipeIngredient = recipe.RecipeIngredients
                        .FirstOrDefault(x => x.Ingredient.Name == ingredient.Name);
                    recipeIngredient.Quantity = ingredient.Quantity;
                }

                this.GetCalories(recipe);
                this.Data.SaveChanges();

                return RedirectToAction("UploadImage", "Images", new { recipeName = recipe.Name });
            }

            return View(ingredients);
        }

        private void GetCalories(Recipe recipe)
        {
            foreach (var ingredient in recipe.RecipeIngredients)
            {
                recipe.Proteins += (ingredient.Quantity * ingredient.Ingredient.Proteins) / (100 * recipe.NumberOfPortions);
                recipe.Fats += (ingredient.Quantity * ingredient.Ingredient.Fats / 100) / (100 * recipe.NumberOfPortions);
                recipe.Carbohydrates += (ingredient.Quantity * ingredient.Ingredient.Carbohydrates / 100) / (100 * recipe.NumberOfPortions);
            }
        }
    }
}