namespace Foodsy.Web.Areas.Recipes.ViewModels.Recipes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using Foodsy.Data.Models;
    using Foodsy.Web.Areas.Recipes.ViewModels.Actions;
    using Foodsy.Web.Infrastructure.Mapping;
    using Foodsy.Web.ViewModels.Comment;

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

        [Required]
        [StringLength(5000, MinimumLength = 20)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public Category Category { get; set; }

        [Required]
        [Display(Name = "Meal type")]
        public MealType MealType { get; set; }

        [Display(Name = "Grams per portion")]
        [Range(10, 1000)]
        public int GramsPerPortion { get; set; }

        [Range(10, 2000)]
        public int CaloriesPerPortion { get; set; }

        [Range(0, 2000)]
        public int Proteins { get; set; }

        [Range(0, 2000)]
        public int Carbohydrates { get; set; }

        [Range(0, 2000)]
        public int Fats { get; set; }

        [Range(1, 200)]
        public decimal PricePerPortion { get; set; }

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