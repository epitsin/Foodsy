namespace Foodsy.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Foodsy.Data.Contracts.Models;

    public class Ingredient : DeletableEntity
    {
        private ICollection<RecipeIngredient> recipes;

        public Ingredient()
        {
            this.recipes = new HashSet<RecipeIngredient>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }

        [Range(0, 1000)]
        public int Calories
        {
            get
            {
                return this.Proteins * 4 + this.Carbohydrates * 4 + this.Fats * 9;
            }
        }

        [Required]
        [Range(0, 100)]
        public int Proteins { get; set; }

        [Required]
        [Range(0, 100)]
        public int Carbohydrates { get; set; }

        [Required]
        [Range(0, 100)]
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
