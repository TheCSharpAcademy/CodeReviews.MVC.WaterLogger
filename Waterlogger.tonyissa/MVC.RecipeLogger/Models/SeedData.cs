using MVC.RecipeLogger.Context;

namespace MVC.RecipeLogger.Models;

public static class SeedData
{
    public static void Initialize(RecipeContext context)
    {
        if (context.Recipes.Any())
            return;

        context.Recipes.AddRange(
            new Recipe
            {
                Name = "Tuna salad",
                Instructions = @"Prepare ingredients and mix everything together in a bowl.
                                Add more mayo (or olive oil) if not to desired creaminess.",
                Ingredients = [
                        new Ingredient {
                            Name = "tuna",
                            Amount = 2,
                            Measure = "5-oz cans"
                        },
                        new Ingredient {
                            Name = "mayo",
                            Amount = .25f,
                            Measure = "cups",
                            Comment = "Add tbsp at a time if not creamy enough"
                        },
                        new Ingredient {
                            Name = "small red onion",
                            Amount = .5f,
                            Measure = "cups",
                            Comment = "Chop finely"
                        },
                        new Ingredient {
                            Name = "celery",
                            Amount = 1,
                            Measure = "stalk",
                            Comment = "Dice"
                        },
                        new Ingredient {
                            Name = "lemon pepper",
                            Amount = 1.5f,
                            Measure = "tsp"
                        }
                    ]
            },
            new Recipe
            {
                Name = "Ice water",
                Instructions = "Add ice to water. Garnish with mint. Enjoy!",
                Ingredients = [
                    new Ingredient {
                        Name = "water",
                        Amount = 1,
                        Measure = "cups"
                    },
                    new Ingredient {
                        Name = "ice",
                        Amount = 2,
                        Measure = "cubes"
                    }
                ]
            }
        );

        context.SaveChanges();
    }
}