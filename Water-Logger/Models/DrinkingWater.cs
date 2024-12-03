using System.ComponentModel.DataAnnotations;

namespace Water_Logger.Models;

public class DrinkingWater
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public DateTime Date { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Value of {0} cannot be negative.")]
    public double Quantity { get; set; }
}
