﻿namespace Foodsy.Data.Models
{
    using System;

    public class Article : IMapFrom<Recipe>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public string ImageUrl { get; set; }

        public DateTime? CreatedOn { get; set; }
    }
}
