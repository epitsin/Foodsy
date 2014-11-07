namespace Foodsy.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    using Foodsy.Data.Repositories;
    using Foodsy.Data.Models;

    public class FoodsyData : IFoodsyData
    {
        private DbContext context;

        private IDictionary<Type, object> repositories;

        public FoodsyData() :
            this(new FoodsyDbContext())
        {
        }

        public FoodsyData(DbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<User> Users
        {
            get
            {
                return this.GetRepository<User>();
            }
        }

        public IRepository<Foodsy.Data.Models.Action> Actions
        {
            get
            {
                return this.GetRepository<Foodsy.Data.Models.Action>();
            }
        }

        public IRepository<Article> Articles
        {
            get
            {
                return this.GetRepository<Article>();
            }
        }

        public IRepository<Comment> Comments
        {
            get
            {
                return this.GetRepository<Comment>();
            }
        }

        public IRepository<Challenge> Challenges
        {
            get
            {
                return this.GetRepository<Challenge>();
            }
        }

        public IRepository<Ingredient> Ingredients
        {
            get
            {
                return this.GetRepository<Ingredient>();
            }
        }

        public IRepository<Like> Likes
        {
            get
            {
                return this.GetRepository<Like>();
            }
        }

        public IRepository<Message> Messages
        {
            get
            {
                return this.GetRepository<Message>();
            }
        }

        public IRepository<Recipe> Recipes
        {
            get
            {
                return this.GetRepository<Recipe>();
            }
        }

        public IRepository<RecipeIngredient> RecipeIngredients
        {
            get
            {
                return this.GetRepository<RecipeIngredient>();
            }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var typeOfRepository = typeof(T);

            if (!this.repositories.ContainsKey(typeOfRepository))
            {
                var newRepository = Activator.CreateInstance(typeof(Repository<T>), this.context);
                this.repositories.Add(typeOfRepository, newRepository);
            }

            return (IRepository<T>)this.repositories[typeOfRepository];
        }
    }
}
