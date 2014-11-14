namespace Foodsy.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using Foodsy.Data.Models;

    public interface IFoodsyDbContext : IDisposable
    {
        IDbSet<Foodsy.Data.Models.Action> Actions { get; set; }

        IDbSet<Article> Articles { get; set; }

        IDbSet<Challenge> Challenges { get; set; }

        IDbSet<Comment> Comments { get; set; }

        IDbSet<Ingredient> Ingredients { get; set; }

        IDbSet<Feedback> Feedbacks { get; set; }

        IDbSet<Like> Likes { get; set; }

        IDbSet<Message> Messages { get; set; }

        IDbSet<Recipe> Recipes { get; set; }

        IDbSet<Tag> Tags { get; set; }

        IDbSet<View> Views { get; set; }

        int SaveChanges();

        void Dispose();

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        IDbSet<T> Set<T>() where T : class;
    }
}
