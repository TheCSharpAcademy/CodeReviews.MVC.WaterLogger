namespace HabitGains.Application.Core.Pagination.Habits;

public class HabitSorting(string column, string order)
{
    public string SortColumn =>
        column switch
        {
            "name" => "Name",
            "measurement" => "Measurement",
            "favorite" => "Favorite",
            "created" => "CreatedAt",
            _ => "CreatedAt",
        };
    public string SortOrder => order.Equals("desc", StringComparison.OrdinalIgnoreCase) ? "DESC" : "ASC";
}
