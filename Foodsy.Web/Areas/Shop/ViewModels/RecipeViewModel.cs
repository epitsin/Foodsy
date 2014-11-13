namespace Foodsy.Web.Areas.Shop.ViewModels
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Foodsy.Data.Models;
    using Foodsy.Web.Areas.Administration.Models.Base;
    using Foodsy.Web.Infrastructure.Mapping;

    public class RecipeViewModel : AdministrationViewModel, IMapFrom<RecipeShoppingCart>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public int Portions { get; set; }

        public string Name { get; set; }

        public decimal PricePerPortion { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            //throw new System.NotImplementedException();
            //configuration.CreateMap<Recipe, RecipeViewModel>()
            //    .ForMember(m => m.Name, opt => opt.MapFrom(t => t.Recipe.Name))
            //    .ForMember(m => m.PricePerPortion, opt => opt.MapFrom(t => t.Recipe.PricePerPortion));
        }
    }
}