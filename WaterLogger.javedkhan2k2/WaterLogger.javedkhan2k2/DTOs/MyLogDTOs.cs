namespace WaterLogger.DTOs;

public class MyLogAddDTO
{
    public DateTime Date { get; set; }
    public int HabitId { get; set; }
    public double Quantity { get; set; }
}

public class MyLogUpdateDTO
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int HabitId { get; set; }
    public double Quantity { get; set; }
}