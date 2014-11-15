namespace Foodsy.Web.Areas.Recipes.ViewModels.Recipes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class AddIngredientViewModel
    {
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [Range(1, 2000)]
        public int Quantity { get; set; }
    }
}