using System.Globalization;
using CalorieLogger.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

namespace CalorieLogger.Pages;

public class IndexModel : PageModel
{
    private readonly IConfiguration _configuration;

    public IndexModel(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public List<CalorieIntakeModel> Records { get; set; }

    public void OnGet()
    {
        Records = GetAllRecords();
        ViewData["Total"] = Records.AsEnumerable().Sum(x => x.Calories);
    }

    private List<CalorieIntakeModel> GetAllRecords()
    {
        using (
            var connection = new SqliteConnection(
                _configuration.GetConnectionString("DefaultConnection")
            )
        )
        {
            connection.Open();

            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = "SELECT * FROM calorie_intake";
            var tableData = new List<CalorieIntakeModel>();
            var reader = tableCmd.ExecuteReader();

            while (reader.Read())
                tableData.Add(
                    new CalorieIntakeModel
                    {
                        Id = reader.GetInt32(0),
                        Date = DateTime.Parse(
                            reader.GetString(1),
                            CultureInfo.CurrentUICulture.DateTimeFormat
                        ),
                        Food = reader.GetString(2),
                        Calories = reader.GetInt32(3),
                    }
                );
            connection.Close();

            return tableData;
        }
    }
}
