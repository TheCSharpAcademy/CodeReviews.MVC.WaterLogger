using System.ComponentModel.DataAnnotations;

namespace HabitGains.Web.ViewModels.Habits;

public class HabitItem(Guid id, string name, string measurement, bool favorite, DateTime createdAt)
{
    public Guid Id { get; set; } = id;
    public string? Name { get; set; } = name;
    public string? Measurement { get; set; } = measurement;
    public bool Favorite { get; set; } = favorite;

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy H:mm}")]
    [Display(Name = "Created")]
    public DateTime CreatedAt { get; } = createdAt;
}
