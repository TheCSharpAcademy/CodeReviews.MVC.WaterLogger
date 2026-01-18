namespace HabitGains.Application.Entries;

public record EntryResponse(Guid Id, Guid HabitId, DateTime Date, decimal Quantity, DateTime CreatedAt);
