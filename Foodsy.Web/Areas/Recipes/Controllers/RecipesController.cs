﻿namespace Foodsy.Web.Areas.Recipes.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Text.RegularExpressions;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using Foodsy.Data;
    using Foodsy.Data.Models;
    using Foodsy.Web.Areas.Recipes.ViewModels.Actions;
    using Foodsy.Web.Areas.Recipes.ViewModels.Recipes;
    using Foodsy.Web.Controllers;
    using Foodsy.Web.ViewModels.Comment;

    using Microsoft.AspNet.Identity;
    using System.Collections.Generic;

    public class RecipesController : BaseController
    {
        private const int PageSize = 9;

        public RecipesController(IFoodsyData data)
            : base(data)
        {
        }

        public ActionResult AllRecipes(int? id)
        {
            int pageNumber = id.GetValueOrDefault(1);

            var allRecipes = this.Data.Recipes.All().Project().To<AllRecipesViewModel>().OrderBy(x => x.Id);

            var recipes = allRecipes.Skip((pageNumber - 1) * PageSize).Take(PageSize);
            ViewBag.Pages = Math.Ceiling((double)allRecipes.Count() / PageSize);

            return View(recipes);
        }

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

            var comments = recipe.Comments.AsQueryable().Project().To<CommentViewModel>().ToList();
            var actions = recipe.Actions.AsQueryable().Project().To<ActionViewModel>().ToList();
            var recipeModel = new DetailedRecipeViewModel
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Description = recipe.Description,
                ImageUrl = recipe.ImageUrl,
                CreatedOn = recipe.CreatedOn,
                Actions = actions,
                Comments = comments,
                CaloriesPerPortion = recipe.CaloriesPerPortion,
                Carbohydrates = recipe.Carbohydrates,
                Fats = recipe.Fats,
                Proteins = recipe.Proteins,
                Likes = recipe.Likes,
                RecipeIngredients = recipe.RecipeIngredients,
                Views = recipe.Views,
                Author = recipe.Author,
                Tags = recipe.Tags,
                PricePerPortion = recipe.PricePerPortion,
                GramsPerPortion = recipe.GramsPerPortion
            };

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

        public ActionResult CreateRecipe()
        {
            var recipe = new CreateRecipeViewModel();
            var ingredients = this.Data.Ingredients.All().ToList();

            // now build the view model
            var model = new CreateRecipeViewModel();
            model.Actions.Add(new ActionViewModel());
            model.Name = recipe.Name;
            //model.SelectedIngredients = recipe.Ingredients.Select(x => x.ID);
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
        public ActionResult CreateRecipe(CreateRecipeViewModel recipe)
        {
            if (ModelState.IsValid)
            {
                var actions = recipe.Actions.AsQueryable().Project().To<Foodsy.Data.Models.Action>().ToList();
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
            var recipe = this.Data.Recipes.All().FirstOrDefault(x => x.Name == recipeName);
            var models = new List<AddIngredientToRecipeViewModel>();
            foreach (var ingredient in recipe.RecipeIngredients)
            {
                models.Add(new AddIngredientToRecipeViewModel
                {
                    Name = ingredient.Ingredient.Name,
                    Quantity = ingredient.Quantity
                });
            }

            return View(models);
        }

        [HttpPost]
        public ActionResult AddIngredients(List<AddIngredientToRecipeViewModel> ingredients, string name)
        {
                var recipe = this.Data.Recipes.All().FirstOrDefault(x => x.Name == name);
                foreach (var ingredient in ingredients)
                {
                    var recipeIngredient = recipe.RecipeIngredients.FirstOrDefault(x => x.Ingredient.Name == ingredient.Name);
                    recipeIngredient.Quantity = ingredient.Quantity;
                }

                this.GetTagsForRecipe(recipe);
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
                recipe.Calories += ingredient.Quantity * ingredient.Ingredient.Calories / 100;
                recipe.Proteins += ingredient.Quantity * ingredient.Ingredient.Proteins / 100;
                recipe.Fats += ingredient.Quantity * ingredient.Ingredient.Fats / 100;
                recipe.Carbohydrates += ingredient.Quantity * ingredient.Ingredient.Carbohydrates / 100;
                recipe.CaloriesPerPortion = recipe.Calories * recipe.GramsPerPortion / 100;
            }
        }

        private void GetTagsForRecipe(Recipe recipe)
        {
            var tagNames = Regex.Split(recipe.Name, @"\W+").ToList();

            foreach (var tag in tagNames)
            {
                if (!this.Data.Tags.All().Any(x => x.Name == tag.ToLower()))
                {
                    var newTag = new Tag { Name = tag.ToLower() };
                    newTag.Recipes.Add(recipe);
                    this.Data.Tags.Add(newTag);
                }
                else
                {
                    this.Data.Tags.All().FirstOrDefault(x => x.Name == tag.ToLower()).Recipes.Add(recipe);
                }
            }

            foreach (var ingredient in recipe.RecipeIngredients)
            {
                if (!this.Data.Tags.All().Any(x => x.Name == ingredient.Ingredient.Name.ToLower()))
                {
                    var newTag = new Tag { Name = ingredient.Ingredient.Name.ToLower() };
                    newTag.Recipes.Add(recipe);
                    this.Data.Tags.Add(newTag);
                }
                else
                {
                    this.Data.Tags.All().FirstOrDefault(x => x.Name == ingredient.Ingredient.Name.ToLower()).Recipes.Add(recipe);
                }
            }
        }
    }
}