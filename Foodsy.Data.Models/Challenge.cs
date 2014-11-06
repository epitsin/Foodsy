namespace Foodsy.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Challenge
    {
        private ICollection<Recipe> recipes;

        public Challenge()
        {
            this.recipes = new HashSet<Recipe>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? Start { get; set; }

        public DateTime? Finish { get; set; }

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
