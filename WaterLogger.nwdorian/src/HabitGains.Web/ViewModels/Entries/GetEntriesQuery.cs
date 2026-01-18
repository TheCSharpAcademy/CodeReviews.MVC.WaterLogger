using HabitGains.Web.Core.Constants;

namespace HabitGains.Web.ViewModels.Entries;

public record GetEntriesQuery(
    int CurrentPage = PagingDefaults.Page,
    int PageSize = PagingDefaults.PageSize,
    string SortColumn = PagingDefaults.SortColumn,
    string SortOrder = PagingDefaults.SortOrder,
    decimal? QuantityFrom = null,
    decimal? QuantityTo = null,
    DateTime? DateFrom = null,
    DateTime? DateTo = null
);
