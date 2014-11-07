namespace Foodsy.Data
{
    using System.Data.Entity;

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
    }
}
