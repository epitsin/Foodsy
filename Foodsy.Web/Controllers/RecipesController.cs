using Foodsy.Data.Contracts.Repository;
using Foodsy.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Foodsy.Web.ViewModels.Home;
using Foodsy.Data;

namespace Foodsy.Web.Controllers
{
    public class RecipesController : Controller
    {
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

        public ActionResult AllRecipes()
        {
            var recipes = this.data.Recipes.All().ToList();
            //.AsQueryable().Project().To<RecipeViewModel>();

            return View(recipes);
        }

        public ActionResult RecipeDetails(int id)
        {
            //var viewModel = this.data.Recipes.Find(id).Project().To<BlogPostViewModel>().FirstOrDefault();

            var recipe = this.data.Recipes.Find(id);
            return View(recipe);
        }
    }
}