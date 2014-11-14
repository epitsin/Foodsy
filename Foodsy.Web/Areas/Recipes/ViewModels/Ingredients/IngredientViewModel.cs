namespace Foodsy.Web.Areas.Recipes.ViewModels.Ingredients
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using Foodsy.Data.Models;
    using Foodsy.Web.Infrastructure.Mapping;

    public class IngredientViewModel : IMapFrom<Ingredient>
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [Range(0, 1000)]
        public int Calories
        {
            get
            {
                return this.Proteins * 4 + this.Carbohydrates * 4 + this.Fats * 9;
            }
        }

        [Required]
        [Range(0, 100)]
        public int Proteins { get; set; }

        [Required]
        [Range(0, 100)]
        public int Carbohydrates { get; set; }

        [Required]
        [Range(0, 100)]
        public int Fats { get; set; }
    }
}