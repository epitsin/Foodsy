namespace Foodsy.Web.Areas.Recipes.ViewModels.Comments
{
    using System.ComponentModel.DataAnnotations;

    public class SubmitCommentViewModel
    {
        [Required]
        [StringLength(2000, MinimumLength = 10)]
        public string Comment { get; set; }

        [Required]
        public int RecipeId { get; set; }
    }
}