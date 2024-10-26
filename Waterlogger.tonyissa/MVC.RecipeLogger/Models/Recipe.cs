namespace MVC.RecipeLogger.Models;

public class Recipe
{
    public int Id { get; set; }
    public List<Ingredient> Ingredients { get; set; } = [];
    public string Instructions { get; set; } = string.Empty;
}