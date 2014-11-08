﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Foodsy.Web.Infrastructure.Mapping;
using Foodsy.Data.Models;
using Foodsy.Web.ViewModels.Comment;

namespace Foodsy.Web.ViewModels.Recipes
{
    public class RecipeViewModel : IMapFrom<Recipe>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public ICollection<CommentViewModel> Comments { get; set; }

        public ICollection<Foodsy.Data.Models.Action> Actions { get; set; }

        public ICollection<Like> Likes { get; set; }
    }
}