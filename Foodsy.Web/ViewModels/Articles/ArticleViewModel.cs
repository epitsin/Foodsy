namespace Foodsy.Web.ViewModels.Articles
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using Foodsy.Data.Models;
    using Foodsy.Web.Infrastructure.Mapping;

    public class ArticleViewModel : IMapFrom<Article>
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        [StringLength(15000, MinimumLength = 20)]
        public string Text { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public ICollection<Tag> Tags { get; set; }
    }
}