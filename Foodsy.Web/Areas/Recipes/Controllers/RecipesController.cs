namespace Foodsy.Web.Areas.Recipes.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text.RegularExpressions;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Foodsy.Common;
    using Foodsy.Data;
    using Foodsy.Data.Models;
    using Foodsy.Web.Areas.Recipes.ViewModels.Actions;
    using Foodsy.Web.Areas.Recipes.ViewModels.Recipes;
    using Foodsy.Web.Controllers;
    using Foodsy.Web.ViewModels.Comment;

    using Microsoft.AspNet.Identity;

    public class RecipesController : BaseController
    {
        private const int PageSize = 9;

        public RecipesController(IFoodsyData data)
            : base(data)
        {
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
            if (!recipe.Views.Any(x => x.AuthorId == User.Identity.GetUserId()))
            {
                recipe.Views.Add(new View
                {
                    AuthorId = User.Identity.GetUserId()
                });

                this.Data.SaveChanges();
            }

            var recipeModel = Mapper.Map<DetailedRecipeViewModel>(recipe);

            if (this.CurrentUser != null)
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
            else
            {
                ViewBag.CanLike = false;
                ViewBag.CanBuy = false;
            }

            if (recipe == null)
            {
                return HttpNotFound();
            }

            return View(recipeModel);
        }

        [HttpGet]
        [Authorize]
        public ActionResult CreateRecipe()
        {
            var ingredients = this.Data.Ingredients.All().ToList();

            var model = new CreateRecipeViewModel();
            model.Actions.Add(new ActionViewModel());
            model.Ingredients = ingredients
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name,
                })
                .ToList();

            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRecipe(CreateRecipeViewModel recipe)
        {
            if (ModelState.IsValid)
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
                    GramsPerPortion = recipe.GramsPerPortion
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

                return RedirectToAction("AddIngredients", new { recipeName = recipe.Name });
            }

            return RedirectToAction("Error", "Home", new { area = String.Empty });
        }

        [HttpGet]
        public ActionResult AddIngredients(string recipeName)
        {
            ViewBag.RecipeName = recipeName;
            var recipe = this.Data.Recipes
                .All()
                .FirstOrDefault(x => x.Name == recipeName);
            var models = new List<AddIngredientViewModel>();
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
                var recipe = this.Data.Recipes
                    .All()
                    .FirstOrDefault(x => x.Name == name);
                foreach (var ingredient in ingredients)
                {
                    var recipeIngredient = recipe.RecipeIngredients.FirstOrDefault(x => x.Ingredient.Name == ingredient.Name);
                    recipeIngredient.Quantity = ingredient.Quantity;
                }

                this.GetCalories(recipe);
                this.Data.SaveChanges();

                return RedirectToAction("UploadImage", "Images", new { recipeName = recipe.Name });
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult PostComment(SubmitCommentViewModel commentModel)
        {
            if (ModelState.IsValid)
            {
                var username = this.User.Identity.GetUserName();
                var userId = this.User.Identity.GetUserId();
                var comment = new Comment()
                {
                    AuthorId = userId,
                    Text = commentModel.Comment,
                    RecipeId = commentModel.RecipeId,
                    CreatedOn = DateTime.Now
                };

                this.Data.Comments.Add(comment);
                this.Data.SaveChanges();

                var viewModel = new CommentViewModel { AuthorUsername = username, Text = comment.Text, CreatedOn = comment.CreatedOn };

                return PartialView("_CommentPartial", viewModel);
            }

            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, ModelState.Values.First().ToString());
        }

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

        private void GetCalories(Recipe recipe)
        {
            foreach (var ingredient in recipe.RecipeIngredients)
            {
                recipe.Proteins += ingredient.Quantity * ingredient.Ingredient.Proteins / 100;
                recipe.Fats += ingredient.Quantity * ingredient.Ingredient.Fats / 100;
                recipe.Carbohydrates += ingredient.Quantity * ingredient.Ingredient.Carbohydrates / 100;
            }
        }
    }
}