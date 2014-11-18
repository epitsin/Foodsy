namespace Foodsy.Web.Areas.Recipes.ViewModels.CustomMenu
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using Foodsy.Data.Models;
    using Foodsy.Web.Infrastructure.Mapping;

    public class CustomMenuRecipeViewModel : IMapFrom<Recipe>
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

        [Display(Name = "Number of Portions")]
        [Range(1, 30)]
        public int NumberOfPortions { get; set; }

        [Range(10, 2000)]
        public int CaloriesPerPortion { get; set; }

        [Range(0, 2000)]
        public int Proteins { get; set; }

        [Range(0, 2000)]
        public int Carbohydrates { get; set; }

        [Range(0, 2000)]
        public int Fats { get; set; }

    }
}