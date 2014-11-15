namespace Foodsy.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class View
    {
        [Key]
        public int Id { get; set; }

        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
