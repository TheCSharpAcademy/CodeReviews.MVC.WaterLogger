using HabitGains.Web.Core.Constants;

namespace HabitGains.Web.ViewModels.Habits;

public record GetHabitsQuery(
    int CurrentPage = PagingDefaults.Page,
    int PageSize = PagingDefaults.PageSize,
    string SortColumn = PagingDefaults.SortColumn,
    string SortOrder = PagingDefaults.SortOrder,
    string SearchTerm = PagingDefaults.SearchTerm,
    string? Measurement = null,
    string? Favorite = null
);
