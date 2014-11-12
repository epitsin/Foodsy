namespace Foodsy.Data.Models
{
    using System.Collections.Generic;

    public class Tag
    {
        private ICollection<Recipe> recipes;
        private ICollection<Article> articles;

        public Tag()
        {
            this.recipes = new HashSet<Recipe>();
            this.articles = new HashSet<Article>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Recipe> Recipes
        {
            get { return this.recipes; }
            set { this.recipes = value; }
        }

        public virtual ICollection<Article> Articles
        {
            get { return this.articles; }
            set { this.articles = value; }
        }
    }
}
