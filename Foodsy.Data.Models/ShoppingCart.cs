namespace Foodsy.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ShoppingCart
    {
        private ICollection<RecipeShoppingCart> recipes;

        public ShoppingCart()
        {
            this.recipes = new HashSet<RecipeShoppingCart>();
        }

        public int Id { get; set; }

        [Key, ForeignKey("Owner")]
        public string OwnerId { get; set; }

        public virtual User Owner { get; set; }

        public virtual ICollection<RecipeShoppingCart> RecipeShoppingCarts
        {
            get
            {
                return this.recipes;
            }
            set
            {
                this.recipes = value;
            }
        }
    }
}
