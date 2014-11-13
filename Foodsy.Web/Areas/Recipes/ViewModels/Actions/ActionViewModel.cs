namespace Foodsy.Web.Areas.Recipes.ViewModels.Actions
{
    using System.ComponentModel.DataAnnotations;

    using Foodsy.Data.Models;
    using Foodsy.Web.Infrastructure.Mapping;

    public class ActionViewModel : IMapFrom<Action>
    {
        public string Text { get; set; }

        [Display(Name="Step")]
        public string ParentActions { get; set; }

    }
}