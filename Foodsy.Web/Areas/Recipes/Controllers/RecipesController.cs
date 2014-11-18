namespace Foodsy.Web.Areas.Recipes.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Foodsy.Common;
    using Foodsy.Data;
    using Foodsy.Data.Models;
    using Foodsy.Web.Areas.Recipes.ViewModels.Actions;
    using Foodsy.Web.Areas.Recipes.ViewModels.Recipes;
    using Foodsy.Web.Infrastructure.Populators;
    using Foodsy.Web.Controllers;

    using Microsoft.AspNet.Identity;

    public class RecipesController : BaseController
    {
        private const int PageSize = 9;

        private IDropDownListPopulator populator;

        public RecipesController(IFoodsyData data, IDropDownListPopulator populator)
            : base(data)
        {
            this.populator = populator;
        }

        [HttpGet]
        public ActionResult AllRecipes(int? id)
        {
            int pageNumber = id.GetValueOrDefault(1);

            var allRecipes = this.Data.Recipes
                .All()
                .Project()
                .To<AllRecipesViewModel>()
                .OrderBy(x => x.Id);

            var recipes = allRecipes.Skip((pageNumber - 1) * PageSize).Take(PageSize);
            ViewBag.Pages = Math.Ceiling((double)allRecipes.Count() / PageSize);

            return View(recipes);
        }

        [HttpPost]
        public ActionResult Sort(int id)
        {
            var category = (Category)id;
            var recipes = this.Data.Recipes
                .All()
                .Where(x => x.Category == category)
                .Project()
                .To<AllRecipesViewModel>()
                .ToList();

            if (recipes.Count == 0)
            {
                return Content(GlobalContants.NoRecipes);
            }

            return PartialView("_AllRecipesPartial", recipes);
        }

        [HttpGet]
        public ActionResult RecipeDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var recipe = this.Data.Recipes.Find(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }

            if (this.CurrentUser != null && !recipe.Views.Any(x => x.AuthorId == this.CurrentUser.Id))
            {
                var view = new View
                {
                    AuthorId = this.CurrentUser.Id
                };
                recipe.Views.Add(view);

                this.Data.SaveChanges();
            }

            var recipeModel = Mapper.Map<DetailedRecipeViewModel>(recipe);

            if (this.CurrentUser != null && recipe.AuthorId != this.CurrentUser.Id)
            {
                var canLike = !recipe.Likes.Any(x => x.AuthorId == this.CurrentUser.Id);
                ViewBag.CanLike = canLike;

                if (this.CurrentUser.ShoppingCart != null)
                {
                    var canBuy = !this.CurrentUser.ShoppingCart.RecipeShoppingCarts.Any(x => x.RecipeId == recipe.Id);
                    ViewBag.CanBuy = canBuy;
                }
                else
                {
                    this.CurrentUser.ShoppingCart = new ShoppingCart();
                    this.Data.SaveChanges();
                    ViewBag.CanBuy = true;
                }
            }
            else if (this.CurrentUser != null && recipe.AuthorId == this.CurrentUser.Id)
            {
                ViewBag.Rate = this.LikeProbability(recipe);
                ViewBag.CanLike = false;
            }
            else
            {
                ViewBag.CanLike = false;
                ViewBag.CanBuy = false;
            }

            return View(recipeModel);
        }

        [HttpGet]
        [Authorize]
        public ActionResult CreateRecipe()
        {
            var model = new CreateRecipeViewModel();
            model.Actions.Add(new ActionViewModel());
            model.Ingredients = this.populator.GetIngredients();

            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRecipe(CreateRecipeViewModel recipe)
        {
            if (recipe != null && ModelState.IsValid)
            {
                var actions = recipe.Actions
                    .AsQueryable()
                    .Project()
                    .To<Foodsy.Data.Models.Action>()
                    .ToList();

                foreach (var action in actions)
                {
                    this.Data.Actions.Add(action);
                }

                var newRecipe = new Recipe
                {
                    Name = recipe.Name,
                    Description = recipe.Description,
                    Category = recipe.Category,
                    AuthorId = this.User.Identity.GetUserId(),
                    CreatedOn = DateTime.Now,
                    MealType = recipe.MealType,
                    Actions = actions,
                    NumberOfPortions = recipe.NumberOfPortions
                };

                foreach (var ingredient in recipe.SelectedIngredients)
                {
                    newRecipe.RecipeIngredients.Add(new RecipeIngredient
                    {
                        IngredientId = int.Parse(ingredient)
                    });
                }

                this.Data.Recipes.Add(newRecipe);
                this.Data.SaveChanges();

                return RedirectToAction("AddIngredients", "Ingredients", new { recipeName = recipe.Name });
            }

            recipe.Ingredients = this.populator.GetIngredients();

            return View(recipe);
        }

        [HttpPost]
        public ActionResult Upvote(int id)
        {
            var userId = User.Identity.GetUserId();

            var canVote = !this.Data.Likes.All().Any(x => x.RecipeId == id && x.AuthorId == userId);

            if (canVote)
            {
                this.Data.Recipes.Find(id).Likes.Add(new Like
                {
                    RecipeId = id,
                    AuthorId = userId,
                    IsPositive = true
                });

                this.Data.SaveChanges();
            }

            var votes = this.Data.Recipes.Find(id).Likes.Where(x => x.IsPositive).Count();

            return Content(votes.ToString());
        }

        [HttpPost]
        public ActionResult Buy(int id)
        {
            this.CurrentUser.ShoppingCart.RecipeShoppingCarts
                .Add(new RecipeShoppingCart
                {
                    RecipeId = id
                });
            this.Data.SaveChanges();

            return Content("Recipe added to shopping cart!");
        }

        private int LikeProbability(Recipe myRecipe)
        {
            var sortedIngredients = new Dictionary<int, double>();
            var sortedCategories = new Dictionary<int, double>();
            var allRecipes = this.Data.Recipes.All().ToList();

            foreach (var recipe in allRecipes)
            {
                double coef = 0d;
                if (recipe.Likes.Count != 0)
                {
                    coef = (double)recipe.Likes.Count / recipe.Views.Count;
                }

                if (sortedCategories.ContainsKey((int)recipe.Category))
                {
                    sortedCategories[(int)recipe.Category] += coef;
                }
                else
                {
                    sortedCategories.Add((int)recipe.Category, coef);
                }

                foreach (var ingredient in recipe.RecipeIngredients)
                {
                    if (sortedIngredients.ContainsKey(ingredient.IngredientId))
                    {
                        sortedIngredients[ingredient.IngredientId] += coef;
                    }
                    else
                    {
                        sortedIngredients.Add(ingredient.IngredientId, coef);
                    }
                }
            }

            var minCountOfSteps = this.Data.Recipes
                .All()
                .OrderBy(x => x.Actions.Count)
                .FirstOrDefault()
                .Actions
                .Count;

            double myIngredientCoef = 0d;
            foreach (var ingredient in myRecipe.RecipeIngredients)
            {
                if (sortedIngredients.ContainsKey(ingredient.IngredientId))
                {
                    myIngredientCoef += sortedIngredients[ingredient.IngredientId];
                }
            }

            double highestIngredientCoef = 0d;
            var reversedIngredients = sortedIngredients.OrderByDescending(x => x.Value);
            int count = 0;
            foreach (var ingredient in reversedIngredients)
            {
                if (count >= myRecipe.RecipeIngredients.Count)
                {
                    break;
                }

                highestIngredientCoef += ingredient.Value;
                count += 1;
            }

            double finalIngredientCoef = 0d;
            double finalCategoryCoef = 0d;
            if (highestIngredientCoef != 0)
            {
                finalIngredientCoef = myIngredientCoef / highestIngredientCoef;
            }

            double highestCategoryCoef = sortedCategories.OrderByDescending(x => x.Value).FirstOrDefault().Value;
            if (finalCategoryCoef != 0)
            {
                finalCategoryCoef = sortedCategories[(int)myRecipe.Category] / highestCategoryCoef;
            }

            double finalActionsCoef = 0;
            if (myRecipe.Actions.Count <= minCountOfSteps)
            {
                finalActionsCoef = 1;
            }
            else if (minCountOfSteps != 0)
            {
                finalActionsCoef = (double)minCountOfSteps / myRecipe.Actions.Count;
            }

            int finalCoef = (int)((finalActionsCoef + finalCategoryCoef + finalIngredientCoef) / 3 * 100);

            return finalCoef;
        }
    }
}