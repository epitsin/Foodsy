using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Foodsy.Web.Infrastructure.Mapping;
using Foodsy.Data.Models;

namespace Foodsy.Web.ViewModels.Articles
{
    public class ArticleViewModel : IMapFrom<Article>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public string ImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}