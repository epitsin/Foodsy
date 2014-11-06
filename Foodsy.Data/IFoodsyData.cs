namespace Foodsy.Data
{
    using Foodsy.Data.Models;
    using Foodsy.Data.Repositories;

    public interface IFoodsyData
    {
        IRepository<User> Users { get; }

        int SaveChanges();
    }
}
