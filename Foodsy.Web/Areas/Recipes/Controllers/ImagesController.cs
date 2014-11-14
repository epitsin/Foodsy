namespace Foodsy.Web.Areas.Recipes.Controllers
{
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Foodsy.Web.Controllers;
    using Foodsy.Data;

    public class ImagesController : BaseController
    {
        public ImagesController(IFoodsyData data)
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult UploadImage(string recipeName)
        {
            ViewBag.RecipeName = recipeName;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadImage(HttpPostedFileBase image, string name)
        {
            if (image != null)
            {
                image.SaveAs(Server.MapPath("~/Content/img/") + image.FileName);
                var recipe = this.Data.Recipes
                    .All()
                    .FirstOrDefault(x => x.Name == name);
                recipe.ImageUrl = "/Content/img/" + image.FileName;

                this.Data.SaveChanges();

                return RedirectToAction("RecipeDetails", "Recipes", new { id = recipe.Id });
            }

            return RedirectToAction("AllRecipes", "Recipes");
        }
    }
}