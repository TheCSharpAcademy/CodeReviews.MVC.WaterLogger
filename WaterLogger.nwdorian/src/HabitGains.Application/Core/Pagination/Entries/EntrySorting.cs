namespace HabitGains.Application.Core.Pagination.Entries;

public class EntrySorting(string column, string order)
{
    public string SortColumn =>
        column switch
        {
            "quantity" => "Quantity",
            "date" => "Date",
            _ => "Date",
        };
    public string SortOrder => order.Equals("desc", StringComparison.OrdinalIgnoreCase) ? "DESC" : "ASC";
}
