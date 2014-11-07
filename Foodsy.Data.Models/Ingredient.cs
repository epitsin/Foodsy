namespace Foodsy.Data.Models
{
    using System.Collections.Generic;

    public class Ingredient
    {
        private ICollection<RecipeIngredient> recipes;

        public Ingredient()
        {
            this.recipes = new HashSet<RecipeIngredient>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int Calories { get; set; }

        public int Proteins { get; set; }

        public int Carbohydrates { get; set; }

        public int Fats { get; set; }

        public virtual ICollection<RecipeIngredient> RecipeIngredients
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
