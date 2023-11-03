using System.ComponentModel;

namespace CarSpottingTracker.mariusgrHiof.Models;
public class CarSpottedModel
{
    public int Id { get; set; }

    [DisplayName("Car Name")]
    public string CarName { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public int Quantity { get; set; }
}

