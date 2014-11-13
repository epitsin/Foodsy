namespace Foodsy.Data.Models
{
    using Foodsy.Data.Contracts.Models;

    public class RecipeShoppingCart : DeletableEntity
    {
        public int Id { get; set; }

        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }

        public int ShoppingCartId { get; set; }

        public virtual ShoppingCart ShoppingCart { get; set; }

        public int Portions { get; set; }
    }
}
