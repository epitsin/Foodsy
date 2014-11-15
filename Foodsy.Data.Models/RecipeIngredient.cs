namespace Foodsy.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class RecipeIngredient
    {
        [Key]
        public int Id { get; set; }

        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }

        public int IngredientId { get; set; }

        public virtual Ingredient Ingredient { get; set; }

        public int Quantity { get; set; }
    }
}
