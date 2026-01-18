namespace HabitGains.Application.Core.Pagination;

public sealed class PagedList<T>(IReadOnlyList<T> items, int page, int pageSize, int totalCount)
{
    public int Page { get; } = page;
    public int PageSize { get; } = pageSize;
    public int TotalCount { get; } = totalCount;
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasPreviousPage => Page > 1;
    public bool HasNextPage => Page < TotalPages;
    public IReadOnlyList<T> Items { get; } = items;
}
