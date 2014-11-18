namespace Foodsy.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Foodsy.Data.Contracts.Models;

    public class Recipe : DeletableEntity
    {
        private ICollection<RecipeIngredient> ingredients;
        private ICollection<Action> actions;
        private ICollection<Comment> comments;
        private ICollection<Like> likes;
        private ICollection<Challenge> challenges;
        private ICollection<View> views;
        private ICollection<RecipeShoppingCart> recipes;

        public Recipe()
        {
            this.ingredients = new HashSet<RecipeIngredient>();
            this.actions = new HashSet<Action>();
            this.comments = new HashSet<Comment>();
            this.likes = new HashSet<Like>();
            this.challenges = new HashSet<Challenge>();
            this.views = new HashSet<View>();
            this.recipes = new HashSet<RecipeShoppingCart>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(5000, MinimumLength = 10)]
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public MealType MealType { get; set; }

        [Required]
        public Category Category { get; set; }

        [Range(0, 2000)]
        public int Calories
        {
            get
            {
                return this.Proteins * 4 + this.Carbohydrates * 4 + this.Fats * 9;
            }
        }

        [Range(0, 2000)]
        public int CaloriesPerPortion
        {
            get
            {
                return this.Calories / this.NumberOfPortions;
            }
        }

        [Required]
        [Range(0, 2000)]
        public int Proteins { get; set; }

        [Required]
        [Range(0, 2000)]
        public int Carbohydrates { get; set; }

        [Required]
        [Range(0, 2000)]
        public int Fats { get; set; }

        [Required]
        [Range(1, 30)]
        public int NumberOfPortions { get; set; }

        [Range(0, 200)]
        public decimal PricePerPortion { get; set; }

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
