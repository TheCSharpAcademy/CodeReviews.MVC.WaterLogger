using System.ComponentModel.DataAnnotations;
namespace HabitHub.Models;

public class HabitRecordModel
{
    public int Id { get; set; }
    public int HabitsId { get; set; }
    [Range(0, float.MaxValue, ErrorMessage = "Value for {0} must be positive")]
    public float Amount { get; set; }
    public string Unit { get; set; }
    public DateTime Date { get; set; }
}
