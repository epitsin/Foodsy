namespace Foodsy.Web.Areas.Administration.ViewModels
{
    using Foodsy.Data.Models;
    using Foodsy.Web.Areas.Administration.Models.Base;
    using Foodsy.Web.Infrastructure.Mapping;

    public class RecipeViewModel : AdministrationViewModel, IMapFrom<Recipe>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public MealType MealType { get; set; }

        public Category Category { get; set; }

        public int GramsPerPortion { get; set; }
    }
}