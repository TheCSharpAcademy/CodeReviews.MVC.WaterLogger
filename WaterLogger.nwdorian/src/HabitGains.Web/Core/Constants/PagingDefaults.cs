namespace HabitGains.Web.Core.Constants;

public static class PagingDefaults
{
    public const string SearchTerm = "";
    public const int Page = 1;
    public const int PageSize = 10;
    public const string SortColumn = "created";
    public const string SortOrder = "asc";

    public static readonly int[] PageSizeOptions = [5, 10, 20];
}
