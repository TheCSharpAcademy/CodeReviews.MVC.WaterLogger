using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC.RecipeLogger.Models;

public class Recipe
{
    public int Id { get; set; }
    [Display(Name = "Recipe name")]
    [StringLength(50, MinimumLength = 1)]
    public string Name { get; set; } = string.Empty;
    public List<Ingredient> Ingredients { get; set; } = [];
    [StringLength(500, MinimumLength = 1)]
    public string Instructions { get; set; } = string.Empty;
}