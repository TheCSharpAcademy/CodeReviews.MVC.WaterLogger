
namespace WaterLogger.DTOs;

public class HabitAddDTO
{
    public string HabitName { get; set; }
    public int HabitUintId { get; set; }
}

public class HabitUpdateDTO
{
    public int Id { get; set; }
    public string HabitName { get; set; }
    public int HabitUintId { get; set; }
}