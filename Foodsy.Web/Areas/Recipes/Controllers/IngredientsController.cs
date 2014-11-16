namespace Foodsy.Web.Areas.Recipes.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using Foodsy.Data;
    using Foodsy.Web.Areas.Recipes.ViewModels.Ingredients;
    using Foodsy.Web.Controllers;

    public class IngredientsController : BaseController
    {
        public IngredientsController(IFoodsyData data)
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult IngredientDetails(int? id)
        {
            var allIngredienits = this.Data.Ingredients
                .All()
                .AsQueryable()
                .Project()
                .To<IngredientViewModel>();
            var ingredient = allIngredienits.FirstOrDefault(x=>x.Id == id);

            return View(ingredient);
        }
    }
}