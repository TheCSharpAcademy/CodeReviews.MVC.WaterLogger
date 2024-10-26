namespace MVC.RecipeLogger.Models;

public record class Recipe(List<Ingredient> Ingredients, string Instructions);