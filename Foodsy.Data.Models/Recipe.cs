namespace Foodsy.Data.Models
{
    using System.Collections.Generic;

    public class Recipe
    {
        private ICollection<RecipeIngredient> ingredients;
        private ICollection<Action> actions;
        private ICollection<Comment> comments;
        private ICollection<Like> likes;
        private ICollection<Challenge> challenges;

        public Recipe()
        {
            this.ingredients = new HashSet<RecipeIngredient>();
            this.actions = new HashSet<Action>();
            this.comments = new HashSet<Comment>();
            this.likes = new HashSet<Like>();
            this.challenges = new HashSet<Challenge>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public MealType MealType { get; set; }

        public Category Category { get; set; }

        public int AuthorId { get; set; }

        public virtual User Author { get; set; }

        public virtual ICollection<RecipeIngredient> RecipeIngredients
        {
            get
            {
                return this.ingredients;
            }
            set
            {
                this.ingredients = value;
            }
        }

        public virtual ICollection<Action> Actions
        {
            get
            {
                return this.actions;
            }
            set
            {
                this.actions = value;
            }
        }

        public virtual ICollection<Comment> Comments
        {
            get
            {
                return this.comments;
            }
            set
            {
                this.comments = value;
            }
        }

        public virtual ICollection<Like> Likes
        {
            get
            {
                return this.likes;
            }
            set
            {
                this.likes = value;
            }
        }

        public virtual ICollection<Challenge> Challenges
        {
            get
            {
                return this.challenges;
            }
            set
            {
                this.challenges = value;
            }
        }
    }
}
