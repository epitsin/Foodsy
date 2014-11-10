using Foodsy.Data.Contracts.Repository;
using Foodsy.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Foodsy.Data;
using System.Net;
using Foodsy.Web.ViewModels.Comment;
using Foodsy.Web.ViewModels.Recipes;
using Microsoft.AspNet.Identity;
using Foodsy.Web.ViewModels.Actions;

namespace Foodsy.Web.Controllers
{
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

            var allRecipes = this.Data.Recipes.All().Select(x => new RecipeViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
                CreatedOn = x.CreatedOn,
                Category = x.Category
            }).OrderBy(x => x.Id);

            var recipes = allRecipes.Skip((pageNumber - 1) * PageSize).Take(PageSize);
            ViewBag.Pages = Math.Ceiling((double)allRecipes.Count() / PageSize);
            //.AsQueryable().Project().To<RecipeViewModel>();

            return View(recipes);
        }

        public ActionResult RecipeDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var recipe = this.Data.Recipes.Find(id);

            this.IncreaseViewCount(recipe);
            this.Data.SaveChanges();

            var comments = recipe.Comments.Select(x => new CommentViewModel
                {
                    AuthorUsername = x.Author.UserName,
                    CreatedOn = x.CreatedOn,
                    Text = x.Text
                }).ToList();

            var recipeModel = new RecipeViewModel
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Description = recipe.Description,
                ImageUrl = recipe.ImageUrl,
                CreatedOn = recipe.CreatedOn,
                Actions = recipe.Actions,
                Comments = comments,
                CaloriesPerPortion = recipe.CaloriesPerPortion,
                Carbohydrates = recipe.Carbohydrates,
                Fats = recipe.Fats,
                Proteins = recipe.Proteins,
                Likes = recipe.Likes,
                ViewCount = recipe.ViewCount,
                RecipeIngredients = recipe.RecipeIngredients
            };

            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipeModel);
        }

        [HttpGet]
        public ActionResult CreateRecipe()
        {
            var recipe = new CreateRecipeViewModel();
            recipe.Actions.Add(new Foodsy.Data.Models.Action());
            return View(recipe);
        }

        [HttpPost]
        public ActionResult CreateRecipe(RecipeViewModel recipe)
        {
            if(ModelState.IsValid)
            {
                var newRecipe = new Recipe
                {
                   Name = recipe.Name,
                   Description = recipe.Description,
                   Category = recipe.Category,
                   AuthorId = this.User.Identity.GetUserId(),
                   CreatedOn = DateTime.Now,
                   MealType = recipe.MealType
                };

                this.Data.Recipes.Add(newRecipe);
                this.Data.SaveChanges();
            }

            return RedirectToAction("UploadImage", "Images", new { recipeName = recipe.Name});
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

                var viewModel = new CommentViewModel { AuthorUsername = username, Text = comment.Text, CreatedOn = comment.CreatedOn  };
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

        private void IncreaseViewCount(Recipe recipe)
        {
            if (Request.Cookies["ViewedRecipe"] != null)
            {
                if (Request.Cookies["ViewedRecipe"][string.Format("rId_{0}", recipe.Id)] == null)
                {
                    HttpCookie cookie = Request.Cookies["ViewedRecipe"];
                    cookie[string.Format("rId_{0}", recipe.Id)] = "1";
                    cookie.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(cookie);

                    recipe.ViewCount += 1;
                }
            }
            else
            {
                HttpCookie cookie = new HttpCookie("ViewedRecipe");
                cookie[string.Format("rId_{0}", recipe.Id)] = "1";
                cookie.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(cookie);

                recipe.ViewCount += 1;
            }
        }
    }
}