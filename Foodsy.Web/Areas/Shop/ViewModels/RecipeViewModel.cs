namespace Foodsy.Web.Areas.Shop.ViewModels
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Foodsy.Data.Models;
    using Foodsy.Web.Areas.Administration.Models.Base;
    using Foodsy.Web.Infrastructure.Mapping;

    public class RecipeViewModel : AdministrationViewModel, IMapFrom<RecipeShoppingCart>, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [Range(0, 30)]
        [DefaultValue(1)]
        public int Portions { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [Range(1, 200)]
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