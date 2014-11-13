namespace Foodsy.Web.Areas.Recipes.ViewModels.Ingredients
{
    using Foodsy.Data.Models;
    using Foodsy.Web.Infrastructure.Mapping;

    public class IngredientViewModel : IMapFrom<Ingredient>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Calories
        {
            get
            {
                return this.Proteins * 4 + this.Carbohydrates * 4 + this.Fats * 9;
            }
        }

        public int Proteins { get; set; }

        public int Carbohydrates { get; set; }

        public int Fats { get; set; }
    }
}