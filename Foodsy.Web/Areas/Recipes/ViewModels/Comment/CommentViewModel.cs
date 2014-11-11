namespace Foodsy.Web.Areas.Recipes.ViewModels.Comment
{
    using System;

    public class CommentViewModel
    {
        public string AuthorUsername { get; set; }

        public string Text { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}