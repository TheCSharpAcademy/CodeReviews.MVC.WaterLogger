using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.RecipeLogger.Models;

public class Ingredient
{
    public int Id { get; set; }
    [Display(Name = "Ingredient name")]
    [StringLength(50, MinimumLength = 1)]
    public string Name { get; set; } = string.Empty;
    [Range(0.01, 100.00)]
    public float Amount { get; set; }
    [Display(Name = "Measure (example: tbsp, cups)")]
    [StringLength(25, MinimumLength = 1)]
    public string Measure { get; set; } = string.Empty;
    [Display(Name = "Comment (optional)")]
    [StringLength(200)]
    public string? Comment { get; set; } = string.Empty;
    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; }
}