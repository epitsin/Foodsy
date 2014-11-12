namespace Foodsy.Data
{
    using Foodsy.Data.Models;
    using Foodsy.Data.Repositories;

    public interface IFoodsyData
    {
        IRepository<User> Users { get; }

        IRepository<Foodsy.Data.Models.Action> Actions { get; }

        IRepository<Article> Articles { get; }

        IRepository<Challenge> Challenges { get; }

        IRepository<Comment> Comments { get; }

        IRepository<Ingredient> Ingredients { get; }

        IRepository<Like> Likes { get; }

        IRepository<Message> Messages { get; }

        IRepository<Recipe> Recipes { get; }

        IRepository<RecipeIngredient> RecipeIngredients { get; }

        IRepository<Tag> Tags { get; }

        IRepository<View> Views { get; }

        int SaveChanges();
    }
}
