using System.ComponentModel.DataAnnotations;

namespace kmakai.MVC.RunningTracker.Models;

public class RunModel
{
    public int Id { get; set; }

    [DataType(DataType.Date)]
    public DateTime Date { get; set; }
    public double Miles { get; set; }

    public double Minutes { get; set; }
}
