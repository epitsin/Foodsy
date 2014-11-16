namespace Foodsy.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Action
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 10)]
        public string Text { get; set; }

        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
