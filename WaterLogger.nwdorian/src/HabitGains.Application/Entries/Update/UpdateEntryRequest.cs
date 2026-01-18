namespace HabitGains.Application.Entries.Update;

public record UpdateEntryRequest(Guid Id, DateTime Date, decimal Quantity);
