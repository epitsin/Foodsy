namespace Foodsy.Data.Models
{
    using System.Collections.Generic;

    public class Ingredient
    {
        private ICollection<Recipe> recipes;

        public Ingredient()
        {
            this.recipes = new HashSet<Recipe>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int Calories { get; set; }

        public int Proteins { get; set; }

        public int Carbohydrates { get; set; }

        public int Fats { get; set; }

        public virtual ICollection<Recipe> Recipes
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
