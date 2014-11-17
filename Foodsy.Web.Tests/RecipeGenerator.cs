using Foodsy.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodsy.Web.Tests
{
    class RecipeGenerator
    {
        public static IEnumerable<Recipe> GetRecipes(int count)
        {
            var subtitles = new List<Recipe>();

            for (int i = 0; i < count; i++)
            {
                subtitles.Add(new Recipe()
                {
                    Id = i,
                    Name = "Some name",
                    Description = "Some long long description here",
                    NumberOfPortions = i + 1,
                    MealType = MealType.MainMeal,
                    Category = Category.Meat,
                    ImageUrl = "/Content/img/28.jpg",
                    PricePerPortion = 20
                });
            }

            return subtitles;
        }
    }
}
