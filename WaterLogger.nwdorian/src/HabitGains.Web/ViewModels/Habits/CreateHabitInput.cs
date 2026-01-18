using System.ComponentModel.DataAnnotations;

namespace HabitGains.Web.ViewModels.Habits;

public class CreateHabitInput
{
    [Required]
    [MinLength(2, ErrorMessage = "Name can't be shorter than 2 characters.")]
    [MaxLength(100, ErrorMessage = "Name can't be longer than 100 cahracters.")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(50, ErrorMessage = "Measurement can't be longer than 50 cahracters.")]
    public string Measurement { get; set; } = string.Empty;
    public bool Favorite { get; set; }
}
