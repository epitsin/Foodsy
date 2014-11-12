namespace Foodsy.Web.Areas.Recipes.ViewModels.Recipes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Foodsy.Data.Models;
    using Foodsy.Web.Areas.Recipes.ViewModels.Actions;
    using Foodsy.Web.Areas.Recipes.ViewModels.Comment;
    using Foodsy.Web.Infrastructure.Mapping;
    using System.Web.Mvc;

    public class DetailedRecipeViewModel : IMapFrom<Recipe>
    {
        public DetailedRecipeViewModel()
        {
            this.Actions = new List<ActionViewModel>();
            this.Views = new List<View>();
            this.Tags = new List<Tag>();
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

        public User Author { get; set; }

        public ICollection<CommentViewModel> Comments { get; set; }

        [UIHint("Action")]
        public ICollection<ActionViewModel> Actions { get; set; }

        public ICollection<Like> Likes { get; set; }

        public ICollection<RecipeIngredient> RecipeIngredients { get; set; }

        public ICollection<View> Views { get; set; }

        public ICollection<Tag> Tags { get; set; }
    }
}