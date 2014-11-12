namespace Foodsy.Web.Areas.Recipes.ViewModels.CustomMenu
{
    using System.ComponentModel.DataAnnotations;

    public enum CustomMenuType
    {
        [Display(Name = "High fat diet")]
        HighFat,
        [Display(Name = "High protein diet")]
        HighProtein,
        [Display(Name = "Low fat diet")]
        LowFat,
        [Display(Name = "Vegetarian diet")]
        Vegetarian,
        [Display(Name = "Raw food diet")]
        Raw
    }
}