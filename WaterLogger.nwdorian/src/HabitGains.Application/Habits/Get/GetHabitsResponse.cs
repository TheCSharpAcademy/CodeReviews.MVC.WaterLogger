namespace HabitGains.Application.Habits.Get;

public record GetHabitsResponse(
    int Page,
    int PageSize,
    int TotalCount,
    int TotalPages,
    bool HasPreviousPage,
    bool HasNextPage,
    IReadOnlyList<HabitResponse> Items
);
