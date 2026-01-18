namespace HabitGains.Application.Core.Pagination.Entries;

public record EntryFilter(decimal? QuantityFrom, decimal? QuantityTo, DateTime? DateFrom, DateTime? DateTo);
