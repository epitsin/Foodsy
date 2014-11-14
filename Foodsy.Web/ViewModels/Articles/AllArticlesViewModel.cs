namespace Foodsy.Web.ViewModels.Articles
{

    using System.ComponentModel.DataAnnotations;
    public class AllArticlesViewModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        [StringLength(15000, MinimumLength = 20)]
        public string Text { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}