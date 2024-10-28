using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.RecipeLogger.Models;

public class Ingredient
{
    public int Id { get; set; }
    [Display(Name = "Ingredient name")]
    public string Name { get; set; } = string.Empty;
    public float Amount { get; set; }
    [Display(Name = "Measure (example: tbsp, cups)")]
    public string Measure { get; set; } = string.Empty;
    [Display(Name = "Comment (optional)")]
    public string? Comment { get; set; } = string.Empty;
    public int RecipeId { get; set; }
    public Recipe? Recipe { get; set; }
}