using Foodsy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Foodsy.Web.Controllers;
using Microsoft.AspNet.Identity;

namespace Foodsy.Web.Areas.Recipes.Controllers
{
    public class FavouriteRecipesController : BaseController
    {
        public FavouriteRecipesController(IFoodsyData data)
            : base(data)
        {
        }

        [Authorize]
        [HttpGet]
        public ActionResult FavouriteRecipes()
        {
            var userId = this.User.Identity.GetUserId();
            var recipes = this.Data.Recipes.All().Where(x => x.Likes.Select(l => l.AuthorId).Contains(userId));
            return View(recipes);
        }
    }
}