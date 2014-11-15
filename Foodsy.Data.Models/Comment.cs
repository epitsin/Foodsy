namespace Foodsy.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(2000, MinimumLength = 10)]
        public string Text { get; set; }

        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
