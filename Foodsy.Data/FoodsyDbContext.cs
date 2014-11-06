namespace Foodsy.Data
{
    using System.Data.Entity;

    using Foodsy.Data.Migrations;
    using Foodsy.Data.Models;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class FoodsyDbContext : IdentityDbContext<User>
    {
        public FoodsyDbContext()
            : base("FoodsyConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<FoodsyDbContext, Configuration>());
        }

        public static FoodsyDbContext Create()
        {
            return new FoodsyDbContext();
        }
    }
}
