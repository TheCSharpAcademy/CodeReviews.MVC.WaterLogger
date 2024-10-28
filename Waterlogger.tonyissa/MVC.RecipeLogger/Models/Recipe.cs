using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC.RecipeLogger.Models;

public class Recipe
{
    public int Id { get; set; }
    [Display(Name = "Recipe name")]
    public string Name { get; set; } = string.Empty;
    public List<Ingredient> Ingredients { get; set; } = [];
    public string Instructions { get; set; } = string.Empty;
}