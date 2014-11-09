using Foodsy.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Foodsy.Web.ViewModels.Recipes
{
    public class CreateRecipeViewModel
    {
        public CreateRecipeViewModel()
        {
            this.Actions = new List<Foodsy.Data.Models.Action>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public Category Category { get; set; }

        [Display(Name = "Meal type")]
        public MealType MealType { get; set; }

        [Display(Name = "Grams per portion")]
        public int GramsPerPortion { get; set; }

        public ICollection<Foodsy.Data.Models.Action> Actions { get; set; }
    }
}