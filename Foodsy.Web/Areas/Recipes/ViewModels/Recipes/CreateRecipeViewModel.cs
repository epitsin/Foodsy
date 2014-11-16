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
        }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        [UIHint("Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(5000, MinimumLength = 20)]
        [UIHint("Description")]
        public string Description { get; set; }

        [Required]
        [UIHint("Category")]
        public Category Category { get; set; }

        [Required]
        [Display(Name = "Meal type")]
        [UIHint("MealType")]
        public MealType MealType { get; set; }

        [Required]
        [Range(5, 2000)]
        [Display(Name = "Grams per portion")]
        [UIHint("GramsPerPortion")]
        public int GramsPerPortion { get; set; }

        public ICollection<ActionViewModel> Actions { get; set; }

        public IEnumerable<string> SelectedIngredients { get; set; }

        public IEnumerable<SelectListItem> Ingredients { get; set; }
    }
}