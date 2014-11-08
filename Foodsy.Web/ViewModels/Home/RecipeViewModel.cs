using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Foodsy.Web.Infrastructure.Mapping;
using Foodsy.Data.Models;

namespace Foodsy.Web.ViewModels.Home
{
    public class RecipeViewModel : IMapFrom<Recipe>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }
}