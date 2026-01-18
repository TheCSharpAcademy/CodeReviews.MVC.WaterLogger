namespace HabitGains.Application.Entries.GetByHabitId;

public record GetEntriesByHabitIdResponse(
    int Page,
    int PageSize,
    int TotalCount,
    int TotalPages,
    bool HasPreviousPage,
    bool HasNextPage,
    decimal TotalQuantity,
    IReadOnlyList<EntryResponse> Items
);
