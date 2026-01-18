using HabitGains.Domain.Entries;

namespace HabitGains.Domain.Habits;

public sealed class Habit
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Measurement { get; set; }
    public bool Favorite { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public List<Entry> Entries { get; set; } = [];
}
