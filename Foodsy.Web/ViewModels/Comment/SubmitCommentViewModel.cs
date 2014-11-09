namespace Foodsy.Web.ViewModels.Comment
{
    using System.ComponentModel.DataAnnotations;

    public class SubmitCommentViewModel
    {
        [Required]
        public string Comment { get; set; }

        [Required]
        public int RecipeId { get; set; }
    }
}