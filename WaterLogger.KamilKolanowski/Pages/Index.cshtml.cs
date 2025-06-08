using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using WaterLogger.KamilKolanowski.Models;

namespace WaterLogger.KamilKolanowski.Pages;

public class IndexModel : PageModel
{
    private readonly IConfiguration _configuration;
    public List<DrinkingWaterModel> Records { get; set; }
    
    public IndexModel(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void OnGet()
    {
        Records = GetAllRecords();
    }

    private List<DrinkingWaterModel> GetAllRecords()
    {
        using (var connection = new SqliteConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT Id, Date, Quantity FROM drinking_water";
            var tableData = new List<DrinkingWaterModel>();
            SqliteDataReader reader = command.ExecuteReader();
            
                while (reader.Read())
                {
                    tableData.Add(new DrinkingWaterModel
                    {
                        Id = reader.GetInt32(0),
                        Date = DateTime.Parse(reader.GetString(1), CultureInfo.CurrentUICulture.DateTimeFormat),
                        Quantity = reader.GetInt32(2)
                    });
                }
            
            connection.Close();
            return tableData;
        }
    }
}
