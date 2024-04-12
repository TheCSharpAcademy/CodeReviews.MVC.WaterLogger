using System.ComponentModel.DataAnnotations;

namespace MoodLogger.Models;

public class Mood
{
    public int Id { get; set; }
    [DisplayFormat(DataFormatString = "{0:d}")]
    public DateTime Date { get; set; }
    [Range(1, 5, ErrorMessage = "Value for {0} must be between 1 and 5 (inclusive).")]
    public int MoodLevel { get; set; }
}