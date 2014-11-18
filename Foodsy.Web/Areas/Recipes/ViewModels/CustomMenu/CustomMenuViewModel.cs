namespace Foodsy.Web.Areas.Recipes.ViewModels.CustomMenu
{
    using System.ComponentModel.DataAnnotations;

    using Foodsy.Web.Areas.Recipes.Models;

    public class CustomMenuViewModel
    {
        [Required]
        public string Gender { get; set; }

        [Required]
        [Range(5, 100)]
        public int Age { get; set; }

        [Required]
        [Range(100, 230)]
        public int Height { get; set; }

        [Required]
        [Range(30, 300)]
        public int Weight { get; set; }

        public int UserId { get; set; }

        [Required]
        public CustomMenuType Type { get; set; }
    }
}