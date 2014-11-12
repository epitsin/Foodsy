namespace Foodsy.Web.Areas.Recipes.ViewModels.CustomMenu
{
    using System.ComponentModel.DataAnnotations;

    public enum CustomMenuType
    {
        [Display(Name = "High fat diet")]
        HighFat,
        [Display(Name = "Low fat diet")]
        LowFat,
        [Display(Name = "Low carb diet")]
        LowCarb,
        [Display(Name = "Vegetarian diet")]
        Vegetarian,
        [Display(Name = "Raw food diet")]
        Raw
    }
}