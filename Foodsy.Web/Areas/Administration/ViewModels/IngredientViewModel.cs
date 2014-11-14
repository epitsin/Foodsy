namespace Foodsy.Web.Areas.Administration.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using Foodsy.Data.Models;
    using Foodsy.Web.Areas.Administration.Models.Base;
    using Foodsy.Web.Infrastructure.Mapping;

    public class IngredientViewModel : AdministrationViewModel, IMapFrom<Ingredient>
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }

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