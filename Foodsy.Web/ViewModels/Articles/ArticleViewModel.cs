﻿namespace Foodsy.Web.ViewModels.Articles
{
    using System;
    using System.Collections.Generic;

    using Foodsy.Data.Models;
    using Foodsy.Web.Infrastructure.Mapping;

    public class ArticleViewModel : IMapFrom<Article>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public string ImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public ICollection<Tag> Tags { get; set; }
    }
}