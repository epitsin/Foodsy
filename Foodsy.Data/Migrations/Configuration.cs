namespace Foodsy.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Foodsy.Data.Models;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<FoodsyDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;

            //TODO: Remove in production!
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(FoodsyDbContext context)
        {
            this.SeedIngredients(context);
            this.SeedRecipes(context);
            this.SeedArticles(context);
        }

        protected void SeedIngredients(FoodsyDbContext context)
        {
            if (context.Ingredients.Any())
            {
                return;
            }

            context.Ingredients.Add(new Ingredient { Name = "Pork", Calories = 400, Proteins = 30, Carbohydrates = 0, Fats = 30 });
            context.Ingredients.Add(new Ingredient { Name = "Chicken", Calories = 300, Proteins = 30, Carbohydrates = 0, Fats = 10 });
            context.Ingredients.Add(new Ingredient { Name = "Bread", Calories = 400, Proteins = 0, Carbohydrates = 30, Fats = 5 });
            context.Ingredients.Add(new Ingredient { Name = "Cheese", Calories = 300, Proteins = 20, Carbohydrates = 0, Fats = 20 });
            context.Ingredients.Add(new Ingredient { Name = "Tomato", Calories = 100, Proteins = 5, Carbohydrates = 10, Fats = 0 });
            context.Ingredients.Add(new Ingredient { Name = "Cucumber", Calories = 100, Proteins = 5, Carbohydrates = 10, Fats = 0 });
            context.Ingredients.Add(new Ingredient { Name = "Olive", Calories = 100, Proteins = 0, Carbohydrates = 0, Fats = 10 });
            context.Ingredients.Add(new Ingredient { Name = "Olive oil", Calories = 100, Proteins = 0, Carbohydrates = 0, Fats = 10 });
            context.Ingredients.Add(new Ingredient { Name = "Butter", Calories = 100, Proteins = 0, Carbohydrates = 0, Fats = 10 });
            context.Ingredients.Add(new Ingredient { Name = "Lettuce", Calories = 50, Proteins = 2, Carbohydrates = 5, Fats = 0 });
            context.Ingredients.Add(new Ingredient { Name = "Wine", Calories = 200, Proteins = 0, Carbohydrates = 20, Fats = 0 });
            context.Ingredients.Add(new Ingredient { Name = "Rice", Calories = 200, Proteins = 0, Carbohydrates = 30, Fats = 0 });
            context.Ingredients.Add(new Ingredient { Name = "Milk", Calories = 150, Proteins = 5, Carbohydrates = 10, Fats = 5 });

            context.SaveChanges();
        }

        protected void SeedRecipes(FoodsyDbContext context)
        {
            if (context.Recipes.Any())
            {
                return;
            }

            var banica = new Recipe
            {
                Name = "Banica",
                Description = "Traditional BG meal",
                Category = Category.Vegetarian,
                MealType = MealType.Breakfast,
                CreatedOn = DateTime.Now,
                ImageUrl = "/Content/img/beer-tomato-food-knife-still-life_1920x1080_sc.jpg"
            };

            var batter = new Ingredient { Name = "Batter", Calories = 400, Proteins = 0, Carbohydrates = 30, Fats = 5 };
            var cottageCheese = new Ingredient { Name = "Cottage cheese", Calories = 200, Proteins = 20, Carbohydrates = 0, Fats = 10 };
            var eggs = new Ingredient { Name = "Eggs", Calories = 150, Proteins = 10, Carbohydrates = 0, Fats = 10 };
            var sunflowerOil = new Ingredient { Name = "Sunflower oil", Calories = 100, Proteins = 0, Carbohydrates = 0, Fats = 10 };

            var relationships = new List<RecipeIngredient>();
            relationships.Add(new RecipeIngredient
            {
                Ingredient = batter,
                Recipe = banica,
                Quantity = 200
            });
            relationships.Add(new RecipeIngredient
            {
                Ingredient = cottageCheese,
                Recipe = banica,
                Quantity = 150
            });
            relationships.Add(new RecipeIngredient
            {
                Ingredient = eggs,
                Recipe = banica,
                Quantity = 100
            });
            relationships.Add(new RecipeIngredient
            {
                Ingredient = sunflowerOil,
                Recipe = banica,
                Quantity = 20
            });

            banica.RecipeIngredients = relationships;

            context.Recipes.Add(banica);

            var musaka = new Recipe
            {
                Name = "Musaka",
                Description = "Another traditional BG meal",
                Category = Category.Meat,
                MealType = MealType.MainMeal,
                CreatedOn = DateTime.Now,
                ImageUrl = "/Content/img/black-background-glass-water-drops-liquid-sprays-tangerines-oranges-skin-cuts-food_1920x1080_sc.jpg"
            };

            var potato = new Ingredient { Name = "Potato", Calories = 200, Proteins = 0, Carbohydrates = 30, Fats = 0 };
            var meat = new Ingredient { Name = "Minced meat", Calories = 400, Proteins = 30, Carbohydrates = 0, Fats = 30 };
            var carrots = new Ingredient { Name = "Carrots", Calories = 100, Proteins = 0, Carbohydrates = 15, Fats = 0 };

            var relationshipsMusaka = new List<RecipeIngredient>();
            relationshipsMusaka.Add(new RecipeIngredient
            {
                Ingredient = potato,
                Recipe = musaka,
                Quantity = 200
            });
            relationshipsMusaka.Add(new RecipeIngredient
            {
                Ingredient = meat,
                Recipe = musaka,
                Quantity = 150
            });
            relationshipsMusaka.Add(new RecipeIngredient
            {
                Ingredient = carrots,
                Recipe = musaka,
                Quantity = 50
            });

            musaka.RecipeIngredients = relationshipsMusaka;

            context.Recipes.Add(musaka);

            context.SaveChanges();
        }

        protected void SeedArticles(FoodsyDbContext context)
        {
            if (context.Articles.Any())
            {
                return;
            }

            context.Articles.Add(new Article
            {
                Title = "Some title",
                Text = "Some long long text. Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.",
                CreatedOn = DateTime.Now,
                ImageUrl = "/Content/img/black-background-glass-water-drops-liquid-sprays-tangerines-oranges-skin-cuts-food_1920x1080_sc.jpg"
            });

            context.Articles.Add(new Article
            {
                Title = "Another title",
                Text = "Some long long text. Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.",
                CreatedOn = DateTime.Now,
                ImageUrl = "/Content/img/black-water-glass-strawberry.jpg"
            });
            
            context.Articles.Add(new Article
            {
                Title = "Another another title",
                Text = "Some long long text. Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.",
                CreatedOn = DateTime.Now,
                ImageUrl = "/Content/img/blueberries_1600x900_sc.jpg"
            });

            context.SaveChanges();
        }
    }
}
