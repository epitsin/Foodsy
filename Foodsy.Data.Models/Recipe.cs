namespace Foodsy.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Foodsy.Data.Contracts.Models;

    public class Recipe : DeletableEntity
    {
        private ICollection<RecipeIngredient> ingredients;
        private ICollection<Action> actions;
        private ICollection<Comment> comments;
        private ICollection<Like> likes;
        private ICollection<Challenge> challenges;
        private ICollection<View> views;
        private ICollection<Tag> tags;

        public Recipe()
        {
            this.ingredients = new HashSet<RecipeIngredient>();
            this.actions = new HashSet<Action>();
            this.comments = new HashSet<Comment>();
            this.likes = new HashSet<Like>();
            this.challenges = new HashSet<Challenge>();
            this.views = new HashSet<View>();
            this.tags = new HashSet<Tag>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }
        
        public MealType MealType { get; set; }

        public Category Category { get; set; }

        public int Calories { get; set; }

        public int CaloriesPerPortion { get; set; }

        public int Proteins { get; set; }

        public int Carbohydrates { get; set; }

        public int Fats { get; set; }

        public int GramsPerPortion { get; set; }

        public string AuthorId { get; set; }

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

        public virtual ICollection<View> Views
        {
            get
            {
                return this.views;
            }
            set
            {
                this.views = value;
            }
        }

        public virtual ICollection<Tag> Tags
        {
            get
            {
                return this.tags;
            }
            set
            {
                this.tags = value;
            }
        }
    }
}
