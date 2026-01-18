namespace HabitGains.Application.Entries.Create;

public record CreateEntryRequest(Guid HabitId, DateTime Date, decimal Quantity);
