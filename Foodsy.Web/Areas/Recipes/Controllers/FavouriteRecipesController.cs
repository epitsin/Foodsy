namespace Foodsy.Web.Areas.Recipes.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using Foodsy.Data;
    using Foodsy.Web.Areas.Recipes.ViewModels.Recipes;
    using Foodsy.Web.Controllers;

    using Microsoft.AspNet.Identity;

    public class FavouriteRecipesController : BaseController
    {
        private const int PageSize = 9;

        public FavouriteRecipesController(IFoodsyData data)
            : base(data)
        {
        }

        [Authorize]
        [HttpGet]
        public ActionResult FavouriteRecipes(int? id)
        {
            int pageNumber = id.GetValueOrDefault(1);

            var userId = this.User.Identity.GetUserId();
            var recipes = this.Data.Recipes
                .All()
                .Where(x => x.Likes.Select(l => l.AuthorId)
                    .Contains(userId));

            var recipeModels = recipes
                .Project()
                .To<AllRecipesViewModel>()
                .OrderBy(x => x.Id);
            var recipesOnPage = recipeModels
                .Skip((pageNumber - 1) * PageSize)
                .Take(PageSize);

            ViewBag.Pages = Math.Ceiling((double)recipeModels.Count() / PageSize);

            return View(recipesOnPage);
        }
    }
}