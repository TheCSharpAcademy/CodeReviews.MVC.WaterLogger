using System.ComponentModel.DataAnnotations;

namespace DrinksLogger.wkktoria.Models;

public class Drink
{
    [Key] public int Id { get; set; }

    [DataType(DataType.Date)] public DateTime Date { get; set; } = DateTime.Now;

    public string Type { get; set; } = null!;

    public string Measurement { get; set; } = null!;

    [Range(0, double.MaxValue, ErrorMessage = "Cannot be negative.")]
    public double Quantity { get; set; }
}