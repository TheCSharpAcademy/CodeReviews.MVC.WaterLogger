namespace HabitGains.Application.Habits.Get;

public record GetHabitsRequest(
    string? Measurement,
    bool? Favorite,
    string SearchTerm,
    int Page,
    int PageSize,
    string SortColumn,
    string SortOrder
);
