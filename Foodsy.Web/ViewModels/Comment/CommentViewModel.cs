namespace Foodsy.Web.ViewModels.Comment
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Foodsy.Data.Models;
    using Foodsy.Web.Infrastructure.Mapping;

    public class CommentViewModel : IMapFrom<Comment>
    {
        [Required]
        public string AuthorUsername { get; set; }

        [Required]
        [StringLength(2000, MinimumLength = 10)]
        public string Text { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }
    }
}