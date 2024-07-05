namespace WaterLogger.Models;

public class MyLog
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public double Quantity { get; set; }
    public int HabitId { get; set; }
    public Habit Habit {get;set;}
}