using System.Globalization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using RunLogger.StanimalTheMan.Models;

namespace RunLogger.StanimalTheMan.Pages;

public class IndexModel : PageModel
{
    private readonly IConfiguration _configuration;

    public List<MilesRunModel> Records { get; set; }

    public IndexModel(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void OnGet()
    {
        Records = GetAllRecords();
        ViewData["TotalDistanceRun"] = Records.AsEnumerable().Sum(record => record.Distance);
    }

    private List<MilesRunModel> GetAllRecords()
    {
        using (var connection = new
            SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
        {
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText =
                $"SELECT * FROM miles_run ";

            var tableData = new List<MilesRunModel>();
            SqliteDataReader reader = tableCmd.ExecuteReader();

            while (reader.Read())
            {
                tableData.Add(
                new MilesRunModel
                {
                    Id = reader.GetInt32(0),
                    Date = DateTime.Parse(reader.GetString(1),
                        CultureInfo.CurrentUICulture.DateTimeFormat),
                    Distance = reader.GetDecimal(2)
                });
            }

            return tableData;
        }
    }
}

