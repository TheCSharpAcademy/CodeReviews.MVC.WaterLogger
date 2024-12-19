using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using WaterLogger.jollejonas.Models;

namespace WaterLogger.jollejonas.Pages;

public class IndexModel : PageModel
{

    private readonly IConfiguration _configuration;
    public List<Expense> Expenses { get; set; }
    public DateTime SelectedDate { get; set; }
    public decimal ExpensesSum { get; set; }

    public IndexModel(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public void OnGet(DateTime? date)
    {
        SelectedDate = date ?? DateTime.Now;
        Expenses = GetAllRecordsForPeriod(SelectedDate);
        ExpensesSum = GetSumForPeriod(SelectedDate);
    }

    private List<Expense> GetAllRecordsForPeriod(DateTime date)
    {
        var expenses = new List<Expense>();
        using (var connection = new SqliteConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                    SELECT Id, Name, Date, Amount 
                    FROM Expenses 
                    WHERE Date >= @StartDate AND Date < @EndDate";
                command.Parameters.AddWithValue("@StartDate", new DateTime(date.Year, date.Month, 1));
                command.Parameters.AddWithValue("@EndDate", new DateTime(date.Year, date.Month, 1).AddMonths(1));

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        expenses.Add(new Expense
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Date = reader.GetDateTime(2),
                            Amount = reader.GetDecimal(3)
                        });
                    }
                }
            }
        }
        return expenses;
    }

    private decimal GetSumForPeriod(DateTime date)
    {
        decimal sum = 0;
        using (var connection = new SqliteConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                    SELECT Amount 
                    FROM Expenses 
                    WHERE Date >= @StartDate AND Date < @EndDate";
                command.Parameters.AddWithValue("@StartDate", new DateTime(date.Year, date.Month, 1));
                command.Parameters.AddWithValue("@EndDate", new DateTime(date.Year, date.Month, 1).AddMonths(1));

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        sum += reader.GetDecimal(0);
                    }
                }
            }
        }
        return sum;
    }
}
