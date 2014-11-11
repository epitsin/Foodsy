namespace Foodsy.Web.ViewModels.Recipes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Foodsy.Data.Models;
    using Foodsy.Web.ViewModels.Comment;
    using Foodsy.Web.Infrastructure.Mapping;
    using System.Web.Mvc;
    using Foodsy.Web.ViewModels.Actions;

    public class RecipeViewModel : IMapFrom<Recipe>
    {
        public RecipeViewModel()
        {
            this.Actions = new List<ActionViewModel>();
        }

        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public Category Category { get; set; }

        [Display(Name = "Meal type")]
        public MealType MealType { get; set; }

        [Display(Name = "Grams per portion")]
        [Range(0, 1000)]
        public int GramsPerPortion { get; set; }

        [Range(0, 2000)]
        public int CaloriesPerPortion { get; set; }

        [Range(0, 2000)]
        public int Proteins { get; set; }

        [Range(0, 2000)]
        public int Carbohydrates { get; set; }

        [Range(0, 2000)]
        public int Fats { get; set; }

        public int ViewCount { get; set; }

        public ICollection<CommentViewModel> Comments { get; set; }

        [UIHint("Action")]
        public ICollection<ActionViewModel> Actions { get; set; }

        public ICollection<Like> Likes { get; set; }

        public ICollection<RecipeIngredient> RecipeIngredients { get; set; }
    }
}