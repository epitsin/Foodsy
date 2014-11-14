namespace Foodsy.Web.Areas.Administration.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using Foodsy.Data.Models;
    using Foodsy.Web.Areas.Administration.Models.Base;
    using Foodsy.Web.Infrastructure.Mapping;

    public class RecipeViewModel : AdministrationViewModel, IMapFrom<Recipe>
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(5000, MinimumLength = 20)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [Display(Name = "Meal type")]
        public MealType MealType { get; set; }

        [Required]
        public Category Category { get; set; }

        [Required]
        [Range(5, 2000)]
        [Display(Name = "Grams per portion")]
        public int GramsPerPortion { get; set; }

        [Required]
        [Range(1, 200)]
        public decimal PricePerPortion { get; set; }
    }
}