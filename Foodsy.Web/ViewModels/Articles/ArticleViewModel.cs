namespace Foodsy.Web.ViewModels.Articles
{
    using System;

    public class ArticleViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public string ImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}