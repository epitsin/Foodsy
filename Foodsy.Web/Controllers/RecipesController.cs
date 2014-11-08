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

namespace Foodsy.Web.Controllers
{
    public class RecipesController : Controller
    {
        private const int PageSize = 9;
        //private IRepository<Recipe> recipes;

        //public RecipesController(IRepository<Recipe> recipes)
        //{
        //    this.recipes = recipes;
        //}

        private IFoodsyData data;

        public RecipesController(IFoodsyData data)
        {
            this.data = data;
        }

        public ActionResult AllRecipes(int? id)
        {
            int pageNumber = id.GetValueOrDefault(1);

            var allRecipes = this.data.Recipes.All().Select(x => new RecipeViewModel
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
            var recipe = this.data.Recipes.Find(id);
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
                Likes = recipe.Likes
            };

            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipeModel);
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

                this.data.Comments.Add(comment);

                this.data.SaveChanges();

                var viewModel = new CommentViewModel { AuthorUsername = username, Text = comment.Text, CreatedOn = comment.CreatedOn  };
                return PartialView("_CommentPartial", viewModel);
            }

            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, ModelState.Values.First().ToString());
        }

        public ActionResult Upvote(int id)
        {
            var userId = User.Identity.GetUserId();

            var canVote = !this.data.Likes.All().Any(x => x.RecipeId == id && x.AuthorId == userId);

            if (canVote)
            {
                this.data.Recipes.Find(id).Likes.Add(new Like
                {
                    RecipeId = id,
                    AuthorId = userId,
                    IsPositive = true
                });

                this.data.SaveChanges();
            }

            var votes = this.data.Recipes.Find(id).Likes.Where(x => x.IsPositive).Count();

            return Content(votes.ToString());
        }
    }
}