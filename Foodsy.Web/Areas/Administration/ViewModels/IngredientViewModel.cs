namespace Foodsy.Web.Areas.Administration.ViewModels
{
    using Foodsy.Data.Models;
    using Foodsy.Web.Areas.Administration.Models.Base;
    using Foodsy.Web.Infrastructure.Mapping;

    public class IngredientViewModel : AdministrationViewModel, IMapFrom<Ingredient>
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public int Proteins { get; set; }

        public int Carbohydrates { get; set; }

        public int Fats { get; set; }
    }
}