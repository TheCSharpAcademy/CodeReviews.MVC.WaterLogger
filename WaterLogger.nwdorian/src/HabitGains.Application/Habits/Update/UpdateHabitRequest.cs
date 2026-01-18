namespace HabitGains.Application.Habits.Update;

public record UpdateHabitRequest(Guid Id, string Name, string Measurement, bool Favorite);
