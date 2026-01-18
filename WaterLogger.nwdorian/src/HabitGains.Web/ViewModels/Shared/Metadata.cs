namespace HabitGains.Web.ViewModels.Shared;

public record Metadata(int Page, int PageSize, int TotalCount, int TotalPages, bool HasPreviousPage, bool HasNextPage)
{
    public int StartPage => Math.Max(1, Page - 1);
    public int EndPage => Math.Min(TotalPages, Page + 1);
    public bool IsFirstPage => Page == 1;
    public bool IsLastPage => Page == TotalPages;
}
