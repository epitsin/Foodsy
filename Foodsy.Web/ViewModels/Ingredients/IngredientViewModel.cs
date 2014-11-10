﻿using Foodsy.Data.Models;
using Foodsy.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Foodsy.Web.ViewModels.Ingredients
{
    public class IngredientViewModel : IMapFrom<Ingredient>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Proteins { get; set; }

        public int Carbohydrates { get; set; }

        public int Fats { get; set; }
    }
}