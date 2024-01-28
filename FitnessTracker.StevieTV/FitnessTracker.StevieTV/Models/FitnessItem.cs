using System.ComponentModel.DataAnnotations;

namespace FitnessTracker.StevieTV.Models;

public class FitnessItem
{
    public int Id { get; set; }
    [DisplayFormat(DataFormatString = "{0:dd-MMM-yy}", ApplyFormatInEditMode = false)]
    public DateTime Date { get; set; }

    public string Type { get; set; } = "";
    [DisplayFormat(DataFormatString = @"{0:hh\:mm}", ApplyFormatInEditMode = true)]
    public TimeSpan Duration { get; set; }
}