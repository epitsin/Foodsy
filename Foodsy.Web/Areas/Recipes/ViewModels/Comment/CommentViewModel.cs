namespace Foodsy.Web.Areas.Recipes.ViewModels.Comment
{
    using System;

    using Foodsy.Data.Models;
    using Foodsy.Web.Infrastructure.Mapping;

    public class CommentViewModel : IMapFrom<Comment>
    {
        public string AuthorUsername { get; set; }

        public string Text { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}