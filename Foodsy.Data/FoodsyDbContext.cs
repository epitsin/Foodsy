namespace Foodsy.Data
{
    using Foodsy.Data.Models;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class FoodsyDbContext : IdentityDbContext<User>
    {
        public FoodsyDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static FoodsyDbContext Create()
        {
            return new FoodsyDbContext();
        }
    }
}
