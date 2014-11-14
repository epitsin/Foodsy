namespace Foodsy.Web.Areas.Recipes.ViewModels.Recipes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Foodsy.Data.Models;
    using Foodsy.Web.Areas.Recipes.ViewModels.Actions;
    using System.Web.Mvc;

    public class CreateRecipeViewModel
    {
        public CreateRecipeViewModel()
        {
            this.Actions = new List<ActionViewModel>();
            this.RecipeIngredients = new List<RecipeIngredient>();
        }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(5000, MinimumLength = 20)]
        public string Description { get; set; }

        [Required]
        public Category Category { get; set; }

        [Required]
        [Display(Name = "Meal type")]
        public MealType MealType { get; set; }

        [Required]
        [Range(5, 2000)]
        [Display(Name = "Grams per portion")]
        public int GramsPerPortion { get; set; }

        public ICollection<ActionViewModel> Actions { get; set; }

        public ICollection<RecipeIngredient> RecipeIngredients { get; set; }

        public IEnumerable<string> SelectedIngredients { get; set; }

        public IEnumerable<SelectListItem> Ingredients { get; set; }
    }
}