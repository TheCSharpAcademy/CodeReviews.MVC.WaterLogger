using System.ComponentModel.DataAnnotations;

namespace ActionTracker.TwilightSaw.Models;

public class Record
{
    public int Id { get; set; }
    [DisplayFormat(DataFormatString = "{0:dd.MM.yy}", ApplyFormatInEditMode = true)]
    public DateTime Date { get; set; }

    [DisplayFormat()]
    public string Measurement { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Value for {0} must be positive.")]
    public double Quantity { get; set; }
    public int ActionsId { get; set; }
}