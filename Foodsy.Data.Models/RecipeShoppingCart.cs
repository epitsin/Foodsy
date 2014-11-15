namespace Foodsy.Data.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using Foodsy.Data.Contracts.Models;

    public class RecipeShoppingCart : DeletableEntity
    {
        [Key]
        public int Id { get; set; }

        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }

        public int ShoppingCartId { get; set; }

        public virtual ShoppingCart ShoppingCart { get; set; }

        [Required]
        [DefaultValue(1)]
        public int Portions { get; set; }
    }
}
