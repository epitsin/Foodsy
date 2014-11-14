namespace Foodsy.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using Foodsy.Data.Contracts.Models;
    using Foodsy.Data.Migrations;
    using Foodsy.Data.Models;

    using Microsoft.AspNet.Identity.EntityFramework;
using Foodsy.Data.Contracts.CodeFirstConventions;

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

        public IDbSet<Feedback> Feedbacks { get; set; }

        public IDbSet<Like> Likes { get; set; }

        public IDbSet<Message> Messages { get; set; }

        public IDbSet<Recipe> Recipes { get; set; }

        public IDbSet<Tag> Tags { get; set; }

        public IDbSet<View> Views { get; set; }

        public static FoodsyDbContext Create()
        {
            return new FoodsyDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add(new IsUnicodeAttributeConvention());

            base.OnModelCreating(modelBuilder); // Without this call EntityFramework won't be able to configure the identity model
        }

        public DbContext DbContext
        {
            get
            {
                return this;
            }
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            this.ApplyDeletableEntityRules();
            return base.SaveChanges();
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditInfo)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    if (!entity.PreserveCreatedOn)
                    {
                        entity.CreatedOn = DateTime.Now;
                    }
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }

        private void ApplyDeletableEntityRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (
                var entry in
                    this.ChangeTracker.Entries()
                        .Where(e => e.Entity is IDeletableEntity && (e.State == EntityState.Deleted)))
            {
                var entity = (IDeletableEntity)entry.Entity;

                entity.DeletedOn = DateTime.Now;
                entity.IsDeleted = true;
                entry.State = EntityState.Modified;
            }
        }
    }
}
