﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Foodsy.Web.Infrastructure.Mapping;
using Foodsy.Data.Models;
using Foodsy.Web.ViewModels.Comment;
using System.ComponentModel.DataAnnotations;

namespace Foodsy.Web.ViewModels.Recipes
{
    public class RecipeViewModel : IMapFrom<Recipe>
    {
        public RecipeViewModel()
        {
            this.Actions = new List<Foodsy.Data.Models.Action>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public Category Category { get; set; }

        [Display(Name = "Meal type")]
        public MealType MealType { get; set; }

        [Display(Name = "Grams per portion")]
        public int GramsPerPortion { get; set; }

        public int ViewCount { get; set; }

        public ICollection<CommentViewModel> Comments { get; set; }

        [UIHint("Action")]
        public ICollection<Foodsy.Data.Models.Action> Actions { get; set; }

        public ICollection<Like> Likes { get; set; }
    }
}