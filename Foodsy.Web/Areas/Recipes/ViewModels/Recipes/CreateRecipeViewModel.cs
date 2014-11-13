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

        public string Name { get; set; }

        public string Description { get; set; }

        public Category Category { get; set; }

        [Display(Name = "Meal type")]
        public MealType MealType { get; set; }

        [Display(Name = "Grams per portion")]
        public int GramsPerPortion { get; set; }

        public ICollection<ActionViewModel> Actions { get; set; }

        public ICollection<RecipeIngredient> RecipeIngredients { get; set; }

        public IEnumerable<string> SelectedIngredients { get; set; }

        public IEnumerable<SelectListItem> Ingredients { get; set; }
    }
}