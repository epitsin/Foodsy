namespace Foodsy.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Foodsy.Common;
    using Foodsy.Data.Models;

    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Text.RegularExpressions;

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
            //this.userManager = new UserManager();
            this.SeedIngredients(context);
            this.SeedRecipes(context);
            this.SeedArticles(context);
            this.SeedChallenges(context);
        }

        private void SeedRoles(FoodsyDbContext context)
        {
            context.Roles.AddOrUpdate(x => x.Name, new IdentityRole(GlobalContants.Admin));
            context.SaveChanges();
        }

        //private void SeedUsers(FoodsyDbContext context)
        //{
        //    for (int i = 0; i < 5; i++)
        //    {
        //        var user = new User
        //        {
        //            Email = string.Format("{0}@{1}.com", this.random.RandomString(6, 16)),
        //            UserName = this.random.RandomStirng(6, 16)
        //        };
        //        this.userManager.Create(user);
        //    }
        //    context.SaveChanges();
        //}

        protected void SeedIngredients(FoodsyDbContext context)
        {
            if (context.Ingredients.Any())
            {
                return;
            }

            context.Ingredients.Add(new Ingredient { Name = "Pork", Proteins = 30, Carbohydrates = 0, Fats = 30 });
            context.Ingredients.Add(new Ingredient { Name = "Chicken", Proteins = 30, Carbohydrates = 0, Fats = 10 });
            context.Ingredients.Add(new Ingredient { Name = "Bread", Proteins = 0, Carbohydrates = 30, Fats = 5 });
            context.Ingredients.Add(new Ingredient { Name = "Cheese", Proteins = 20, Carbohydrates = 0, Fats = 20 });
            context.Ingredients.Add(new Ingredient { Name = "Tomato", Proteins = 5, Carbohydrates = 10, Fats = 0 });
            context.Ingredients.Add(new Ingredient { Name = "Cucumber", Proteins = 5, Carbohydrates = 10, Fats = 0 });
            context.Ingredients.Add(new Ingredient { Name = "Olive", Proteins = 0, Carbohydrates = 0, Fats = 10 });
            context.Ingredients.Add(new Ingredient { Name = "Olive oil", Proteins = 0, Carbohydrates = 0, Fats = 10 });
            context.Ingredients.Add(new Ingredient { Name = "Butter", Proteins = 0, Carbohydrates = 0, Fats = 10 });
            context.Ingredients.Add(new Ingredient { Name = "Lettuce", Proteins = 2, Carbohydrates = 5, Fats = 0 });
            context.Ingredients.Add(new Ingredient { Name = "Wine", Proteins = 0, Carbohydrates = 20, Fats = 0 });
            context.Ingredients.Add(new Ingredient { Name = "Rice", Proteins = 0, Carbohydrates = 30, Fats = 0 });
            context.Ingredients.Add(new Ingredient { Name = "Milk", Proteins = 5, Carbohydrates = 10, Fats = 5 });

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

            var batter = new Ingredient { Name = "Batter", Proteins = 0, Carbohydrates = 30, Fats = 5 };
            var cottageCheese = new Ingredient { Name = "Cottage cheese", Proteins = 20, Carbohydrates = 0, Fats = 10 };
            var eggs = new Ingredient { Name = "Eggs", Carbohydrates = 0, Fats = 10 };
            var sunflowerOil = new Ingredient { Name = "Sunflower oil", Proteins = 0, Carbohydrates = 0, Fats = 10 };

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

            foreach (var relationship in relationships)
            {
                var ingredient = relationship.Ingredient;
                banica.Calories += ingredient.Calories * relationship.Quantity / 100;
                banica.Proteins += ingredient.Proteins;
                banica.Carbohydrates += ingredient.Carbohydrates;
                banica.Fats += ingredient.Fats;
            }

            banica.CaloriesPerPortion = banica.Calories * banica.GramsPerPortion / 100;

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

            var potato = new Ingredient { Name = "Potato", Proteins = 0, Carbohydrates = 30, Fats = 0 };
            var meat = new Ingredient { Name = "Minced meat", Proteins = 30, Carbohydrates = 0, Fats = 30 };
            var carrots = new Ingredient { Name = "Carrots", Proteins = 0, Carbohydrates = 15, Fats = 0 };

            var relationshipsMusaka = new List<RecipeIngredient>();
            relationshipsMusaka.Add(new RecipeIngredient
            {
                Ingredient = potato,
                Recipe = musaka,
                Quantity = 200 //240
            });
            relationshipsMusaka.Add(new RecipeIngredient
            {
                Ingredient = meat,
                Recipe = musaka,
                Quantity = 150 //585
            });
            relationshipsMusaka.Add(new RecipeIngredient
            {
                Ingredient = carrots,
                Recipe = musaka,
                Quantity = 50 //30
            });

            musaka.RecipeIngredients = relationshipsMusaka;

            foreach (var relationship in relationshipsMusaka)
            {
                var ingredient = relationship.Ingredient;
                musaka.Calories += ingredient.Calories * relationship.Quantity / 100;
                musaka.Proteins += ingredient.Proteins;
                musaka.Carbohydrates += ingredient.Carbohydrates;
                musaka.Fats += ingredient.Fats;
            }

            musaka.CaloriesPerPortion = musaka.Calories * musaka.GramsPerPortion / 100;

            context.Recipes.Add(musaka);

            context.SaveChanges();
        }

        protected void SeedArticles(FoodsyDbContext context)
        {
            if (context.Articles.Any())
            {
                return;
            }
            var first = new Article
            {
                Title = "Some title",
                Text = "Some long long text. Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.",
                CreatedOn = DateTime.Now,
                ImageUrl = "/Content/img/black-background-glass-water-drops-liquid-sprays-tangerines-oranges-skin-cuts-food_1920x1080_sc.jpg"
            };
            var second = new Article
            {
                Title = "Another title",
                Text = "Some long long text. Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.",
                CreatedOn = DateTime.Now,
                ImageUrl = "/Content/img/black-water-glass-strawberry.jpg"
            };
            var third = new Article
            {
                Title = "Another another title",
                Text = "Some long long text. Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.",
                CreatedOn = DateTime.Now,
                ImageUrl = "/Content/img/blueberries_1600x900_sc.jpg"
            };

            context.Articles.Add(first);
            this.GetTagsForArticle(context, first);
            context.SaveChanges();

            context.Articles.Add(first);
            this.GetTagsForArticle(context, second);
            context.SaveChanges();

            context.Articles.Add(first);
            this.GetTagsForArticle(context, third);
            context.SaveChanges();
        }

        protected void SeedChallenges(FoodsyDbContext context)
        {
            if (context.Challenges.Any())
            {
                return;
            }

            var recipe1 = context.Recipes.Find(1);
            var recipe2 = context.Recipes.Find(2);

            context.Challenges.Add(new Challenge
            {
                Title = "Only Bulgarian food challenge",
                Description = "Some long long text. Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.Some long long text.",
                Start = DateTime.Now,
                Finish = DateTime.Now,
                ChallengeType = ChallengeType.Detox,
                Recipes = new List<Recipe>() {
                      recipe1,
                      recipe2
                  }
            });

            context.SaveChanges();
        }

        private void GetTagsForArticle(FoodsyDbContext context, Article àrticle)
        {
            var tagNames = Regex.Split(àrticle.Title, @"\W+").ToList();

            foreach (var tag in tagNames)
            {
                if (!context.Tags.Any(x => x.Name == tag.ToLower()))
                {
                    var newTag = new Tag { Name = tag.ToLower() };
                    newTag.Articles.Add(àrticle);
                    context.Tags.Add(newTag);
                }
                else
                {
                    context.Tags.FirstOrDefault(x => x.Name == tag.ToLower()).Articles.Add(àrticle);
                }
            }
        }
    }
}
