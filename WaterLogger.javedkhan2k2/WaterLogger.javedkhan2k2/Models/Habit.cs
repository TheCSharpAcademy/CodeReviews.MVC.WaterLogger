
namespace WaterLogger.Models;

public class Habit
{
    public int Id { get; set; }
    public string HabitName { get; set; }
    public int HabitUintId { get; set; }

    public HabitUnit HabitUnit { get; set; }
}