namespace Foodsy.Web.Areas.Recipes.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using Foodsy.Data;
    using Foodsy.Data.Models;
    using Foodsy.Web.Areas.Recipes.Models;
    using Foodsy.Web.Areas.Recipes.ViewModels.CustomMenu;
    using Foodsy.Web.Areas.Recipes.ViewModels.Recipes;
    using Foodsy.Web.Controllers;

    public class CustomMenuController : BaseController
    {
        public CustomMenuController(IFoodsyData data)
            : base(data)
        {
        }

        [Authorize]
        [HttpGet]
        public ActionResult GetInformation()
        {
            var model = new CustomMenuViewModel();
            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GenerateMenu(CustomMenuViewModel model)
        {
            if (ModelState.IsValid)
            {
                var bmr = 0;
                if (model.Gender == "Male")
                {
                    bmr = 65 + (14 * model.Weight) + (5 * model.Height) - (7 * model.Age);
                }
                else
                {
                    bmr = 665 + (10 * model.Weight) + (2 * model.Height) - (5 * model.Age);
                }

                var meals = new List<Recipe>();
                if (model.Type == CustomMenuType.HighFat)
                {
                    meals = this.SelectMeals(bmr + 50, (x => x.Fats), null);
                }
                else if (model.Type == CustomMenuType.HighProtein)
                {
                    meals = this.SelectMeals(bmr + 50, (x => x.Proteins), null);
                }
                else if (model.Type == CustomMenuType.LowFat)
                {
                    meals = this.SelectMeals(bmr + 50, (x => x.Carbohydrates), null);
                }
                else if (model.Type == CustomMenuType.Raw)
                {
                    meals = this.SelectMeals(bmr + 50, (x => x.CaloriesPerPortion), Category.Raw);
                }
                else if (model.Type == CustomMenuType.Vegetarian)
                {
                    meals = this.SelectMeals(bmr + 50, (x => x.CaloriesPerPortion), Category.Vegetarian);
                }
                else
                {
                    throw new ArgumentException("Invalid menu type!");
                }

                var mealModels = meals.AsQueryable().Project().To<CustomMenuRecipeViewModel>();
                ViewBag.CustomCalories = bmr;

                return View("CustomMenu", mealModels);
            }

            return RedirectToAction("GetInformation");
        }

        private List<Recipe> SelectMeals(int calories, Func<Recipe, int> action, Category? category)
        {
            var recipes = this.Data.Recipes.All().ToList();
            var recipesCount = recipes.Count();

            int[,] dynamicMatrix = new int[recipesCount + 1, calories + 1];
            int[,] backtrackMatrix = new int[recipesCount + 1, calories + 1];

            for (int i = 1; i <= recipesCount; i++)
            {
                var currentRecipe = recipes[i - 1];
                for (int j = 1; j <= calories; j++)
                {
                    int notUsedValue = dynamicMatrix[i - 1, j];
                    int usedValue = 0;
                    if (j - currentRecipe.CaloriesPerPortion >= 0)
                    {
                        if (category == null)
                        {
                            usedValue = dynamicMatrix[i - 1, j - currentRecipe.CaloriesPerPortion] + action(currentRecipe);
                        }
                        else
                        {
                            if (currentRecipe.Category == category)
                            {
                                usedValue = dynamicMatrix[i - 1, j - currentRecipe.CaloriesPerPortion] + action(currentRecipe);
                            }
                        }
                    }

                    if (usedValue >= notUsedValue && usedValue != 0)
                    {
                        dynamicMatrix[i, j] = usedValue;
                        backtrackMatrix[i, j] = j - currentRecipe.CaloriesPerPortion;
                    }
                    else
                    {
                        dynamicMatrix[i, j] = notUsedValue;
                    }
                }
            }

            int maxCustomPleasure = 0;
            int index = 0;
            for (int i = 0; i < calories + 1; i++)
            {
                if (dynamicMatrix[recipesCount, i] > maxCustomPleasure)
                {
                    maxCustomPleasure = dynamicMatrix[recipesCount, i];
                    index = i;
                }
            }

            var meals = new List<Recipe>();
            for (int i = recipesCount; i > 0; i--)
            {
                if (dynamicMatrix[i, index] != dynamicMatrix[i - 1, index])
                {
                    meals.Add(recipes[i - 1]);
                    index = backtrackMatrix[i, index];
                }
            }

            return meals;
        }
    }
}