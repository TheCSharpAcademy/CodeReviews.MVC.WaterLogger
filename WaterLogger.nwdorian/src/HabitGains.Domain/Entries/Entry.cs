namespace HabitGains.Domain.Entries;

public class Entry
{
    public Guid Id { get; set; }
    public Guid HabitId { get; set; }
    public DateTime Date { get; set; }
    public decimal Quantity { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
