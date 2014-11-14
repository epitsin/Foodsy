namespace Foodsy.Web.Areas.Recipes.ViewModels.Recipes
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using Foodsy.Data.Models;
    using Foodsy.Web.Infrastructure.Mapping;

    public class AllRecipesViewModel : IMapFrom<Recipe>
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
    }
}