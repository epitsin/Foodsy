using Foodsy.Data;
using Foodsy.Data.Models;
using Foodsy.Web.Areas.Recipes.ViewModels.CustomMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Foodsy.Web.Controllers;

namespace Foodsy.Web.Areas.Recipes.Controllers
{
    public class CustomMenuController : BaseController
    {
        public CustomMenuController(IFoodsyData data)
            : base(data)
        {
        }

        public ActionResult GetInformation()
        {
            return View();
        }

        public ActionResult GenerateMenu(CustomMenuViewModel model)
        {
            if (ModelState.IsValid)
            {
                var BMR = 0;
                if (model.Gender == "Male")
                {
                    BMR = 65 + (14 * model.Weight) + (5 * model.Height) - (7 * model.Age);
                }
                else
                {
                    BMR = 665 + (10 * model.Weight) + (2 * model.Height) - (5 * model.Age);
                }

                var meals = new List<Recipe>();
                if (model.Type == CustomMenuType.HighFat)
                {
                    meals = this.SelectMeals(BMR + 100, (x => x.Fats), null);
                }
                else if (model.Type == CustomMenuType.LowCarb)
                {
                    meals = this.SelectMeals(BMR, (x => x.Carbohydrates), null);
                }
                else if (model.Type == CustomMenuType.LowFat)
                {
                    meals = this.SelectMeals(BMR, (x => x.Carbohydrates), null);
                }
                else if (model.Type == CustomMenuType.Raw)
                {
                    meals = this.SelectMeals(BMR, (x => x.Calories), Category.Raw);
                }
                else if (model.Type == CustomMenuType.Vegan)
                {
                    meals = this.SelectMeals(BMR, (x => x.Calories), Category.Vegetarian);

                }
                else if (model.Type == CustomMenuType.Vegetarian)
                {
                    meals = this.SelectMeals(BMR, (x => x.Calories), Category.Vegetarian);
                }
                else
                {
                    throw new ArgumentException("Invalid menu type!");
                }

                ViewBag.CustomCalories = BMR;

                return View("CustomMenu", meals);
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
                    if (j - currentRecipe.Calories >= 0)
                    {
                        //usedValue = dynamicMatrix[i - 1, j - currentRecipe.Calories] + currentRecipe.Carbohydrates; //TODO: CHANGE THIS WHEN YOU KNOW HOW
                        if (category == null)
                        {
                            usedValue = dynamicMatrix[i - 1, j - currentRecipe.Calories] + action(currentRecipe);
                        }
                        else
                        {
                            if(currentRecipe.Category == category)
                            {
                                usedValue = dynamicMatrix[i - 1, j - currentRecipe.Calories] + action(currentRecipe);
                            }
                        }
                    }

                    if (usedValue >= notUsedValue && usedValue != 0)
                    {
                        dynamicMatrix[i, j] = usedValue;
                        backtrackMatrix[i, j] = j - currentRecipe.Calories;
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