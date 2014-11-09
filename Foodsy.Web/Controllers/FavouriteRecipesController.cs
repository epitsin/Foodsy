using Foodsy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Foodsy.Web.Controllers
{
    public class FavouriteRecipesController : Controller
    {

        private IFoodsyData data;

        public FavouriteRecipesController(IFoodsyData data)
        {
            this.data = data;
        }

        public ActionResult FavouriteRecipes()
        {
            var userId = this.User.Identity.GetUserId();
            var recipes = this.data.Recipes.All().Where(x => x.Likes.Select(l => l.AuthorId).Contains(userId));
            return View(recipes);
        }
    }
}