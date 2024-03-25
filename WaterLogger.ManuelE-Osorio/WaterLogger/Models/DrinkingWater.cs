using System.ComponentModel.DataAnnotations;

namespace WaterLogger.Models;

public class DrinkingWater
{
    public int Id {get; set;}
    
    [DataType(DataType.Date)]
    public DateTime? Date {get; set;}

    [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid quantity.")]
    public int Quantity {get; set;}
}