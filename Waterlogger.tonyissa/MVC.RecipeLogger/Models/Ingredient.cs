namespace MVC.RecipeLogger.Models;

public record class Ingredient(string Name, float Amount, string Measure, string? Comment, int RecipeId, Recipe Recipe);