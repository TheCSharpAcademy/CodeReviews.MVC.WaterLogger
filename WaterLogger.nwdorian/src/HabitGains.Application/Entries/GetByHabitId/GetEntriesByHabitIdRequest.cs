namespace HabitGains.Application.Entries.GetByHabitId;

public record GetEntriesByHabitIdRequest(
    Guid HabitId,
    decimal? QuantityFrom,
    decimal? QuantityTo,
    DateTime? DateFrom,
    DateTime? DateTo,
    int Page,
    int PageSize,
    string SortColumn,
    string SortOrder
);
