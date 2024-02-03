using System.Globalization;
using FitnessTracker.StevieTV.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

namespace FitnessTracker.StevieTV.Pages;

public class IndexModel : PageModel
{
    public List<FitnessItem> Records { get; set; }
    private readonly IConfiguration _configuration;

    public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void OnGet()
    {
        Records = GetAllRecords();
        ViewData["Total"] = new TimeSpan(Records.AsEnumerable().Sum(x => x.Duration.Ticks)).ToString(@"hh\:mm");
    }

    private List<FitnessItem> GetAllRecords()
    {
        using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
        {
            connection.Open();
            var command = connection.CreateCommand();

            command.CommandText = "SELECT * FROM fitness_tracker";

            var reader = command.ExecuteReader();
            var tableData = new List<FitnessItem>();

            while (reader.Read())
            {
                tableData.Add(new FitnessItem
                {
                    Id = reader.GetInt32(0),
                    Date = DateTime.Parse(reader.GetString(1), CultureInfo.CurrentUICulture),
                    Type = reader.GetString(2),
                    Duration = TimeSpan.Parse(reader.GetString(3), CultureInfo.CurrentUICulture)
                });
            }
            connection.Close();
            return tableData;
        }
    }
}