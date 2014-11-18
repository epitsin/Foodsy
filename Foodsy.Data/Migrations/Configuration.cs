namespace Foodsy.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Foodsy.Common;
    using Foodsy.Data.Models;

    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;

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
            this.SeedRolesAndUsers(context);
            this.SeedIngredients(context);
            this.SeedRecipes(context);
            this.SeedArticles(context);
            this.SeedChallenges(context);
        }

        private void SeedRolesAndUsers(FoodsyDbContext context)
        {
            if (!context.Users.Any())
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                roleManager.Create(new IdentityRole(GlobalContants.Admin));
                roleManager.Create(new IdentityRole(GlobalContants.User));

                var userManager = new UserManager<User>(new UserStore<User>(context));
                var user = new User { UserName = "epitsin@gmail.com" };
                userManager.Create(user, "123123");
                userManager.AddToRole(user.Id, "Admin");
                userManager.AddToRole(user.Id, "User");

                context.SaveChanges();
            }
        }

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
            context.Ingredients.Add(new Ingredient { Name = "Cucumbers", Proteins = 5, Carbohydrates = 10, Fats = 0 });
            context.Ingredients.Add(new Ingredient { Name = "Olives", Proteins = 0, Carbohydrates = 0, Fats = 10 });
            context.Ingredients.Add(new Ingredient { Name = "Olive oil", Proteins = 0, Carbohydrates = 0, Fats = 10 });
            context.Ingredients.Add(new Ingredient { Name = "Butter", Proteins = 0, Carbohydrates = 0, Fats = 10 });
            context.Ingredients.Add(new Ingredient { Name = "Lettuce", Proteins = 2, Carbohydrates = 5, Fats = 0 });
            context.Ingredients.Add(new Ingredient { Name = "Wine", Proteins = 0, Carbohydrates = 20, Fats = 0 });
            context.Ingredients.Add(new Ingredient { Name = "Rice", Proteins = 0, Carbohydrates = 30, Fats = 0 });

            context.SaveChanges();
        }

        protected void SeedRecipes(FoodsyDbContext context)
        {
            if (context.Recipes.Any())
            {
                return;
            }

            var cremeBrule = new Recipe
            {
                Name = "BLUEBERRY CRÈME BRÛLÉE",
                Description = "Curtis Stone's irresistible creamy brûlées with sweet tangy blueberries are a delicious end to a meal",
                Category = Category.Dessert,
                MealType = MealType.Snack,
                CreatedOn = DateTime.Now,
                ImageUrl = "/Content/img/blueberry-dessert_1920x1080_sc.jpg",
                NumberOfPortions = 2
            };

            var milk = new Ingredient { Name = "Milk", Proteins = 5, Carbohydrates = 10, Fats = 3 };
            var cream = new Ingredient { Name = "Cream", Proteins = 10, Carbohydrates = 0, Fats = 20 };
            var eggs = new Ingredient { Name = "Eggs", Proteins = 10, Carbohydrates = 0, Fats = 10 };
            var mascarpone = new Ingredient { Name = "Mascarpone oil", Proteins = 10, Carbohydrates = 5, Fats = 15 };
            var blueberries = new Ingredient { Name = "Mascarpone oil", Proteins = 5, Carbohydrates = 15, Fats = 0 };

            var relationships = new List<RecipeIngredient>();
            relationships.Add(new RecipeIngredient
            {
                Ingredient = milk,
                Recipe = cremeBrule,
                Quantity = 50
            });
            relationships.Add(new RecipeIngredient
            {
                Ingredient = cream,
                Recipe = cremeBrule,
                Quantity = 450
            });
            relationships.Add(new RecipeIngredient
            {
                Ingredient = eggs,
                Recipe = cremeBrule,
                Quantity = 100
            });
            relationships.Add(new RecipeIngredient
            {
                Ingredient = mascarpone,
                Recipe = cremeBrule,
                Quantity = 125
            });
            relationships.Add(new RecipeIngredient
            {
                Ingredient = blueberries,
                Recipe = cremeBrule,
                Quantity = 125
            });

            cremeBrule.RecipeIngredients = relationships;

            foreach (var relationship in relationships)
            {
                var ingredient = relationship.Ingredient;
                cremeBrule.Proteins += ingredient.Proteins / cremeBrule.NumberOfPortions;
                cremeBrule.Carbohydrates += ingredient.Carbohydrates / cremeBrule.NumberOfPortions;
                cremeBrule.Fats += ingredient.Fats / cremeBrule.NumberOfPortions;
            }

            cremeBrule.Actions.Add(new Foodsy.Data.Models.Action
            {
                Text = "Pour the milk and cream into a large milk pan. Scrape the seeds from the vanilla pods using the tip of a knife. Put the seeds and the pods in the pan. Heat until just below boiling point."
            });
            cremeBrule.Actions.Add(new Foodsy.Data.Models.Action
            {
                Text = "Put the eggs, egg yolks and sugar in a large bowl. Using an electric beater, whisk for about 5 minutes until pale and creamy."
            });
            cremeBrule.Actions.Add(new Foodsy.Data.Models.Action
            {
                Text = "Add the mascarpone and whisk until well combined."
            });
            cremeBrule.Actions.Add(new Foodsy.Data.Models.Action
            {
                Text = "Remove the vanilla pods from the milk. Pour the hot milk mixture over the egg mixture, whisking continuously until well combined."
            });
            cremeBrule.Actions.Add(new Foodsy.Data.Models.Action
            {
                Text = "Strain the mixture back into the pan. Return to the heat for 2-3 minutes just to warm through."
            });
            cremeBrule.Actions.Add(new Foodsy.Data.Models.Action
            {
                Text = "Divide the blueberries between the ramekins. Pour over the cream mixture. Chill for at least 2 hours or overnight until set."
            });
            cremeBrule.Actions.Add(new Foodsy.Data.Models.Action
            {
                Text = "Sprinkle a thin layer of caster sugar over the surface of the cream mixture. Caramelise using a blow torch, or place under a preheated hot grill for 2-3 minutes until the sugar is melted and golden."
            });

            context.Recipes.Add(cremeBrule);

            var mediterranean = new Recipe
            {
                Name = "Mediterranean Orzo Salad with Feta Vinaigrette",
                Description = "Orzo pasta is a versatile base for dishes, and this one is chock-full of zesty ingredients. Red onions add crunch and the combination of artichoke hearts, feta cheese, and kalamata olives all add fresh brininess to the salad.",
                Category = Category.Pasta,
                MealType = MealType.MainMeal,
                CreatedOn = DateTime.Now,
                ImageUrl = "/Content/img/Food Pasta Wallpaper 1920×1080 1920x1080.jpg",
                NumberOfPortions = 2
            };

            var orzo = new Ingredient { Name = "Orzo", Proteins = 0, Carbohydrates = 30, Fats = 0 };
            var meat = new Ingredient { Name = "Minced meat", Proteins = 20, Carbohydrates = 0, Fats = 20 };
            var spinach = new Ingredient { Name = "Spinach", Proteins = 5, Carbohydrates = 15, Fats = 0 };
            var onion = new Ingredient { Name = "Onion", Proteins = 3, Carbohydrates = 10, Fats = 0 };
            var tomatoes = new Ingredient { Name = "Tomatoes", Proteins = 4, Carbohydrates = 15, Fats = 0 };

            var relationshipsMediter = new List<RecipeIngredient>();
            relationshipsMediter.Add(new RecipeIngredient
            {
                Ingredient = orzo,
                Recipe = mediterranean,
                Quantity = 200 
            });
            relationshipsMediter.Add(new RecipeIngredient
            {
                Ingredient = meat,
                Recipe = mediterranean,
                Quantity = 100 
            });
            relationshipsMediter.Add(new RecipeIngredient
            {
                Ingredient = spinach,
                Recipe = mediterranean,
                Quantity = 150
            });
            relationshipsMediter.Add(new RecipeIngredient
            {
                Ingredient = onion,
                Recipe = mediterranean,
                Quantity = 50
            });
            relationshipsMediter.Add(new RecipeIngredient
            {
                Ingredient = tomatoes,
                Recipe = mediterranean,
                Quantity = 70
            });

            mediterranean.RecipeIngredients = relationshipsMediter;

            foreach (var relationship in relationshipsMediter)
            {
                var ingredient = relationship.Ingredient;
                mediterranean.Proteins += ingredient.Proteins / mediterranean.NumberOfPortions;
                mediterranean.Carbohydrates += ingredient.Carbohydrates / mediterranean.NumberOfPortions;
                mediterranean.Fats += ingredient.Fats / mediterranean.NumberOfPortions;
            }

            mediterranean.Actions.Add(new Foodsy.Data.Models.Action
            {
                Text = "Cook the orzo according to package directions, omitting salt and fat."
            });
            mediterranean.Actions.Add(new Foodsy.Data.Models.Action
            {
                Text = "Drain; rinse with cold water. Combine orzo, spinach, and next 5 ingredients (through salt) in a large bowl."
            });
            mediterranean.Actions.Add(new Foodsy.Data.Models.Action
            {
                Text = "Drain artichokes, reserving marinade."
            });
            mediterranean.Actions.Add(new Foodsy.Data.Models.Action
            {
                Text = "Coarsely chop artichokes, and add artichokes, reserved marinade, and 1/2 cup feta cheese to orzo mixture, tossing gently to coat."
            });
            mediterranean.Actions.Add(new Foodsy.Data.Models.Action
            {
                Text = "Sprinkle each serving with remaining feta cheese."
            });

            context.Recipes.Add(mediterranean);

            var salmon = new Recipe
            {
                Name = "Teriyaki Salmon With Zucchini",
                Description = "Six ingredients make a tangy Asian dinner. You’ll get vitamin C and fiber from the zucchini, plus plenty of filling protein from the salmon.",
                Category = Category.Fish,
                MealType = MealType.MainMeal,
                CreatedOn = DateTime.Now,
                ImageUrl = "/Content/img/salmon-and-fresh-vegetables_1920x1080_sc.jpg",
                NumberOfPortions = 2
            };

            var fish = new Ingredient { Name = "Salmon", Proteins = 25, Carbohydrates = 0, Fats = 15 };
            var zucchini = new Ingredient { Name = "Zucchini", Proteins = 5, Carbohydrates = 10, Fats = 0 };
            var teriyaki = new Ingredient { Name = "Teriyaki sauce", Proteins = 0, Carbohydrates = 10, Fats = 10 };
            
            var relationshipsSalmon = new List<RecipeIngredient>();
            relationshipsSalmon.Add(new RecipeIngredient
            {
                Ingredient = fish,
                Recipe = salmon,
                Quantity = 250
            });
            relationshipsSalmon.Add(new RecipeIngredient
            {
                Ingredient = zucchini,
                Recipe = salmon,
                Quantity = 300
            });
            relationshipsSalmon.Add(new RecipeIngredient
            {
                Ingredient = teriyaki,
                Recipe = salmon,
                Quantity = 50
            });

            salmon.RecipeIngredients = relationshipsSalmon;

            foreach (var relationship in relationshipsSalmon)
            {
                var ingredient = relationship.Ingredient;
                salmon.Proteins += ingredient.Proteins / salmon.NumberOfPortions;
                salmon.Carbohydrates += ingredient.Carbohydrates / salmon.NumberOfPortions;
                salmon.Fats += ingredient.Fats / salmon.NumberOfPortions;
            }

            salmon.Actions.Add(new Foodsy.Data.Models.Action
            {
                Text = "Combine 5 tablespoons teriyaki sauce and fish in a zip-top plastic bag. Seal and marinate 20 minutes. "
            });
            salmon.Actions.Add(new Foodsy.Data.Models.Action
            {
                Text = "Toast sesame seeds in a large nonstick skillet over medium heat, and set aside."
            });
            salmon.Actions.Add(new Foodsy.Data.Models.Action
            {
                Text = "Drain fish, discarding marinade. Add fish to skillet, and cook 5 minutes. Turn and cook for 5 more minutes over medium-low heat. Remove from skillet, and keep warm."
            });
            salmon.Actions.Add(new Foodsy.Data.Models.Action
            {
                Text = " Add the zucchini, scallions, and 2 teaspoons oil to skillet. Sauté 4 minutes, or until lightly browned. "
            });
            salmon.Actions.Add(new Foodsy.Data.Models.Action
            {
                Text = "Stir in 2 tablespoons teriyaki sauce. Sprinkle with sesame seeds, and serve with salmon."
            });

            context.Recipes.Add(salmon);

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
                Title = "Caffeine may boost long-term memory",
                Summary = "Numerous studies have suggested that caffeine has many health benefits. Now, new research suggests that a dose of caffeine after a learning session may help to boost long-term memory. This is according to a study published in the journal Nature Neuroscience.",
                Text = "<p>The research team, led by Daniel Borota of the Johns Hopkins University in Baltimore, notes that although previous research has analyzed the effects of caffeine as a cognitive enhancer, whether caffeine can impact long-term memory has not been studied in detail. </p><p>To find out, the investigators analyzed 160 participants aged between 18 and 30 years.</p><p>On the first day of the study, the participants were shown pictures of different objects and were asked to identify them as 'indoor' or 'outdoor' items.</p><p>Soon after this task, they were randomized to receive either 200 mg of caffeine in the form of a pill, or a placebo tablet.</p><p>The next day, the participants were shown the same pictures as well as some new ones. The researchers asked them to identify whether the pictures were 'new,' 'old' or 'similar to the original pictures.'</p><p>200 mg of caffeine 'enhanced memory'</p><p>From this, the researchers found that subjects who took the caffeine were better at identifying pictures that were similar, compared with participants who took the placebo.</p><p>However, the researchers note that both groups were able to accurately distinguish whether pictures were old or new.</p><p>Coffee being poured into a cup which is sitting on a bed of coffee beans</p><p>New research suggests that consuming 200 mg of caffeine a day may boost long-term memory.</p><p>The team conducted further experiments using 100 mg and 300 mg doses of caffeine. They found that performance was better after the 200 mg dose, compared with the 100 mg dose, but there was no improvement after the 300 mg of caffeine, compared with 200 mg.</p><p>'Thus, we conclude that a dose of at least 200 mg is required to observe the enhancing effect of caffeine on consolidation of memory,' the study authors write.</p><p>The team also found that memory performance was not improved if subjects were given caffeine 1 hour before carrying out the picture identification test.</p><p>They investigators say there are many possibilities as to how caffeine may enhance long-term memory.</p><p>For example, they say it may block a molecule called adenosine, preventing it from stopping the function of norepinephrine - a hormone that has been shown to have positive effects on memory.</p><p>They note that further research should be conducted to better understand the mechanisms by which caffeine affects long-term memory.</p>",
                CreatedOn = DateTime.Now,
                ImageUrl = "/Content/img/table-grain-saucer-cup-spoon-coffee-drink-smoke_1920x1080_sc.jpg"
            };
            var second = new Article
            {
                Title = "Fruit juice 'as bad' as sugary drinks, say researchers",
                Summary = "Two medical researchers writing in one of The Lancet journals argue that because of its high sugar content, fruit juice could be just as bad for us as sugar-sweetened beverages like carbonated drinks and sodas.",
                Text = "<p>Naveed Sattar, professor of Metabolic Medicine, and Dr. Jason Gill, both of the Institute of Cardiovascular and Medical Sciences at the University of Glasgow in Scotland, call for the UK government to change the current 'five a day' guideline to exclude a portion of fruit juice from the list of fruits and vegetable servings that count toward it.</p><p>In their paper, published in the The Lancet Diabetes & Endocrinology, they propose that including fruit juice as one of the five a day is 'probably counter-productive,' because it leads people to consider fruit juice as a healthy food that does not need to be limited, as is the case with less healthy foods.</p><p>They also urge food companies to improve container labeling of fruit juices to inform consumers they should drink no more than 150 ml a day of the product.</p><p>Fruit juice has come under the spotlight since medical experts recently started looking more closely at the link between high sugar intake and the risk for heart disease.</p><p>In 2012, researchers at Harvard reported in the journal Circulation that daily consumption of sugary drinks raised heart disease risk in men. Two years earlier, researchers presenting at an American Heart Association conference said Americans' higher consumption of sugary drinks has led to more diabetes and heart disease over the past decade.</p><p>Fruit juice is not a low-sugar alternative to sugar-sweetened drinks</p><p>Dr. Gill says 'there seems to be a clear misperception that fruit juices and smoothies are low-sugar alternatives to sugar-sweetened beverages.'</p><p>Prof. Sattar explains:</p><p>'Fruit juice has a similar energy density and sugar content to other sugary drinks, for example: 250 ml of apple juice typically contains 110 kcal and 26 g of sugar; and 250 ml of cola typically contains 105 kcal and 26.5 g of sugar.'</p><p>He says research is beginning to show that unlike solid fruit intake, for which high consumption appears linked either to reduced or neutral risk for diabetes, high fruit juice intake is linked to raised risk for diabetes.</p><p>Pieces of fruit and fruit juice</p><p>'One glass of fruit juice contains substantially more sugar than one piece of fruit.'</p><p>'One glass of fruit juice contains substantially more sugar than one piece of fruit; in addition, much of the goodness in fruit - fibre, for example - is not found in fruit juice, or is there in far smaller amounts,' he adds.</p><p>Also, although fruit juices contain vitamins and minerals that are mostly absent in sugar-sweetened drinks, the levels of nutrients in fruit juices many not be enough to offset the unhealthy effect that excessive consumption has on metabolism, says Dr. Gill.</p><p>In their paper they refer to a trial where participants drank half a liter of pure grape juice every day for 3 months. And the results showed that despite grape juice's high antioxidant properties, it led to increased insulin resistance and bigger waists in overweight adults.</p>",
                CreatedOn = DateTime.Now,
                ImageUrl = "/Content/img/black-background-glass-water-drops-liquid-sprays-tangerines-oranges-skin-cuts-food_1920x1080_sc.jpg"
            };
            var third = new Article
            {
                Title = "Chocolate, wine and berries may protect against type 2 diabetes",
                Summary = "Good news for chocolate and wine lovers. New research suggests that consuming high levels of flavonoids, found in foods such as chocolate, tea, berries and wine, may help protect against type 2 diabetes. This is according to a study recently published in The Journal of Nutrition.",
                Text = "<p>Investigators from Kings College London and the University of East Anglia, both in the UK, say their research shows that a high intake of these dietary compounds is linked to reduced insulin resistance and improved glucose regulation.</p><p>Type 2 diabetes - the most common form of diabetes - is caused by insulin resistance. This means the body is unable to use insulin properly, which can lead to abnormal blood glucose levels.</p><p>To reach their findings, the research team analyzed 1,997 female volunteers aged between 18 and 76 years from TwinsUK - the largest UK twin registry used for research into genetics, the environment and common diseases.</p><p>All women completed a food questionnaire. This estimated their total dietary flavonoid intake and their intake from six flavonoid subclasses - anthocyanins, flavanones, flavan-3-ols, polymeric flavonoids, flavonols, and flavones.</p><p>Flavonoids 'reduce insulin resistance and inflammation'</p><p>The study revealed that women who consumed high levels of anthocyanins and flavones - compounds found in foods such as berries, herbs, red grapes, chocolate and wine - demonstrated lower insulin resistance.</p><p>Women who consumed the highest levels of flavones also had improved levels of a protein called adiponectin - a regulator of glucose levels, among other metabolic mechanisms.</p><p>Furthermore, the investigators discovered that volunteers who consumed the most anthocyanins were the least likely to have chronic inflammation - a condition linked to diabetes, cardiovascular disease, obesity and cancer.</p><p>However, the researchers note that they do not yet know the levels at which these compounds may protect against type 2 diabetes.</p>",
                CreatedOn = DateTime.Now,
                ImageUrl = "/Content/img/chocolate-i-love-it_1920x1080_sc.jpg"
            };

            context.Articles.Add(first);
            this.GetTagsForArticle(context, first);

            context.Articles.Add(first);
            this.GetTagsForArticle(context, second);

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
                Title = "First Foodsy Challenge",
                Description = "This is the first challange we are organizing since the company was found. We hope that you would enjoy it and see the difference between feeling ordinary and absolutely amazing! You are most welcome to join :)",
                Start = DateTime.Now.AddDays(3),
                Finish = DateTime.Now.AddDays(13),
                ChallengeType = ChallengeType.Detox,
                Recipes = new List<Recipe>() {
                      recipe1,
                      recipe2
                  }
            });

            context.SaveChanges();
        }

        private void GetTagsForArticle(FoodsyDbContext context, Article аrticle)
        {
            var tagsSummary = Regex.Split(аrticle.Summary, @"\W+").ToList();
            tagsSummary.AddRange(Regex.Split(аrticle.Title, @"\W+").ToList());

            foreach (var tag in tagsSummary)
            {
                if (tag.Length >= 3)
                {
                    if (!context.Tags.Any(x => x.Name == tag.ToLower()))
                    {
                        var newTag = new Tag { Name = tag.ToLower() };
                        newTag.Articles.Add(аrticle);
                        context.Tags.Add(newTag);
                    }
                    else
                    {
                        context.Tags.FirstOrDefault(x => x.Name == tag.ToLower()).Articles.Add(аrticle);
                    }
                }
            }
        }
    }
}
