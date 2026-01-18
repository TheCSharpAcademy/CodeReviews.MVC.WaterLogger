namespace HabitGains.Application.Habits;

public record HabitResponse(Guid Id, string Name, string Measurement, bool Favorite, DateTime CreatedAt);
