namespace Foodsy.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using Foodsy.Data.Contracts.Models;
    using Foodsy.Data.Migrations;
    using Foodsy.Data.Models;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class FoodsyDbContext : IdentityDbContext<User>, IFoodsyDbContext
    {
        public FoodsyDbContext()
            : base("FoodsyConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<FoodsyDbContext, Configuration>());
        }

        public IDbSet<Foodsy.Data.Models.Action> Actions { get; set; }

        public IDbSet<Article> Articles { get; set; }

        public IDbSet<Challenge> Challenges { get; set; }

        public IDbSet<Comment> Comments { get; set; }

        public IDbSet<Ingredient> Ingredients { get; set; }

        public IDbSet<Like> Likes { get; set; }

        public IDbSet<Message> Messages { get; set; }

        public IDbSet<Recipe> Recipes { get; set; }

        public static FoodsyDbContext Create()
        {
            return new FoodsyDbContext();
        }

        public override int SaveChanges()
        {
            //this.ApplyAuditInfoRules();
            //this.ApplyDeletableEntityRules();
            return base.SaveChanges();
        }

        //private void ApplyAuditInfoRules()
        //{
        //    // Approach via @julielerman: http://bit.ly/123661P
        //    foreach (var entry in
        //        this.ChangeTracker.Entries()
        //            .Where(
        //                e =>
        //                e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
        //    {
        //        var entity = (IAuditInfo)entry.Entity;

        //        if (entry.State == EntityState.Added)
        //        {
        //            if (!entity.PreserveCreatedOn)
        //            {
        //                entity.CreatedOn = DateTime.Now;
        //            }
        //        }
        //        else
        //        {
        //            entity.ModifiedOn = DateTime.Now;
        //        }
        //    }
        //}

        //private void ApplyDeletableEntityRules()
        //{
        //    // Approach via @julielerman: http://bit.ly/123661P
        //    foreach (
        //        var entry in
        //            this.ChangeTracker.Entries()
        //                .Where(e => e.Entity is IDeletableEntity && (e.State == EntityState.Deleted)))
        //    {
        //        var entity = (IDeletableEntity)entry.Entity;

        //        entity.DeletedOn = DateTime.Now;
        //        entity.IsDeleted = true;
        //        entry.State = EntityState.Modified;
        //    }
        //}
    }
}
