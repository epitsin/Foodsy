namespace Foodsy.Web.Areas.Recipes.ViewModels.Recipes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using AutoMapper;

    using Foodsy.Data.Models;
    using Foodsy.Web.Areas.Recipes.ViewModels.Actions;
    using Foodsy.Web.Areas.Recipes.ViewModels.Comments;
    using Foodsy.Web.Infrastructure.Mapping;

    public class DetailedRecipeViewModel : IMapFrom<Recipe>, IHaveCustomMappings
    {
        public DetailedRecipeViewModel()
        {
            this.Actions = new List<ActionViewModel>();
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

        [Required]
        [Display(Name = "Number of Portions")]
        [Range(1, 30)]
        public int NumberOfPortions { get; set; }

        [Required]
        [Range(10, 2000)]
        public int CaloriesPerPortion { get; set; }

        [Required]
        [Range(0, 2000)]
        public int Proteins { get; set; }

        [Required]
        [Range(0, 2000)]
        public int Carbohydrates { get; set; }

        [Required]
        [Range(0, 2000)]
        public int Fats { get; set; }

        [Range(1, 200)]
        public decimal PricePerPortion { get; set; }

        public int Likes { get; set; }

        public int Views { get; set; }

        public User Author { get; set; }

        public ICollection<CommentViewModel> Comments { get; set; }

        [UIHint("Action")]
        public ICollection<ActionViewModel> Actions { get; set; }

        public ICollection<RecipeIngredient> RecipeIngredients { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Recipe, DetailedRecipeViewModel>()
                .ForMember(m => m.Likes, opt => opt.MapFrom(t => t.Likes.Count))
                .ForMember(m => m.Views, opt => opt.MapFrom(t => t.Views.Count))
                .ReverseMap();
        }
    }
}