namespace Foodsy.Data.Models
{
    using System;

    public class Comment
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
