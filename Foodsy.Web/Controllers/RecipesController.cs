using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Foodsy.Web.Controllers
{
    public class RecipesController : Controller
    {
        // GET: Recipes
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AllRecipes()
        {
            return View();
        }

        public ActionResult RecipeDetails()
        {
            return View();
        }
    }
}