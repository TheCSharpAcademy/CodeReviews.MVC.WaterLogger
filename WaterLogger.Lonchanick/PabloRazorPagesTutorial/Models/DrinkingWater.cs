using System.ComponentModel.DataAnnotations;

namespace PabloRazorPagesTutorial.Models;

public class DrinkingWater
{
    public int ID { get; set; }
    public int Quantity { get; set; }
    public DateTime Date { get; set; }
}
