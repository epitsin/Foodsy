namespace Foodsy.Web.Areas.Recipes.ViewModels.Actions
{
    using System.ComponentModel.DataAnnotations;

    using Foodsy.Data.Models;
    using Foodsy.Web.Infrastructure.Mapping;

    public class ActionViewModel : IMapFrom<Action>
    {
        [Required]
        [StringLength(500, MinimumLength = 10)]
        public string Text { get; set; }

        [Required]
        [Display(Name="Step")]
        public string ParentActions { get; set; }

    }
}