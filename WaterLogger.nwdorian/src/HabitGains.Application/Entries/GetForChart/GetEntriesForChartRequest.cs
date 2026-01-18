namespace HabitGains.Application.Entries.GetForChart;

public sealed record GetEntriesForChartRequest(
    Guid HabitId,
    decimal? QuantityFrom,
    decimal? QuantityTo,
    DateTime? DateFrom,
    DateTime? DateTo
);
