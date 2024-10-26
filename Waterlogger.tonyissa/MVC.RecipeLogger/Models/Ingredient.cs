namespace MVC.RecipeLogger.Models;

public class Ingredient
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public float Amount { get; set; }
    public string Measure { get; set; } = string.Empty;
    public string? Comment { get; set; } = string.Empty;
    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; }
}