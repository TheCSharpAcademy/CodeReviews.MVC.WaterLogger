namespace HabitGains.Application.Core.Pagination.Habits;

public record HabitFilter(string SearchTerm, string? Measurement, bool? Favorite);
