namespace Foodsy.Web.Areas.Shop.ViewModels
{
    using Foodsy.Data.Models;
    using Foodsy.Web.Areas.Administration.Models.Base;
    using Foodsy.Web.Infrastructure.Mapping;

    public class RecipeViewModel : AdministrationViewModel, IMapFrom<RecipeShoppingCart>
    {
        public int Id { get; set; }

        public int Portions { get; set; }

        public Recipe Recipe { get; set; }
    }
}