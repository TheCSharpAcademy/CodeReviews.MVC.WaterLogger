using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using System.Globalization;
using WaterDrinking.Models;

namespace WaterDrinking.Pages;

public class IndexModel : PageModel
{
    private readonly IConfiguration _configuration;

    private readonly ILogger<IndexModel> _logger;

    public List<DrinkingWaterModel> Records { get; set; }

    public IndexModel(IConfiguration configuration, ILogger<IndexModel> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public void OnGet()
    {
        Records = GetAllRecords();
    }

    private List<DrinkingWaterModel> GetAllRecords()
    {
        using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
        {
            connection.Open();

            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = $"SELECT * FROM drinking_water";

            var tableData = new List<DrinkingWaterModel>();
            SqliteDataReader reader = tableCmd.ExecuteReader(); 

            while (reader.Read())
            {
                tableData.Add(new DrinkingWaterModel
                {
                    Id = reader.GetInt32(0),
                    Date = DateTime.Parse(reader.GetString(1), CultureInfo.CurrentUICulture.DateTimeFormat),
                    Quantity = reader.GetInt32(2)
                });
            }
            return tableData;
        }
    }
}
