using System.ComponentModel.DataAnnotations;

namespace CodingTrackerWeb.Models;

public class CodingHour
{
    public int Id { get; set; }

    [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
    public string Date { get; set; } = string.Empty;

    [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
    public string StartTime { get; set; } = string.Empty;

    [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
    public string EndTime { get; set; } = string.Empty;

    [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
    public string Duration { get; internal set; } = string.Empty;
}
