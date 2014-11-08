using Foodsy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Foodsy.Web.Controllers
{
    public class ImagesController : Controller
    {
        private IFoodsyData data;

        public ImagesController(IFoodsyData data)
        {
            this.data = data;
        }

        [HttpGet]
        public ActionResult UploadImage(string recipeName)
        {
            ViewBag.RecipeName = recipeName;
            return View();
        }

        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase image, string recipeName)
        {
            if (image != null)
            {
                image.SaveAs(Server.MapPath("~/Content/img/") + image.FileName);
                var recipe = this.data.Recipes.All().FirstOrDefault(x => x.Name == recipeName);
                recipe.ImageUrl = "~/Content/img/" + image.FileName;

                return RedirectToAction("RecipeDetails", "Recipes", new { id = recipe.Id });
            }

            return RedirectToAction("AllRecipes", "Recipes");
        }
    }
}