using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using WaterDrinkingLogger.TwilightSaw.Models;

namespace WaterDrinkingLogger.TwilightSaw.Pages
{
    public class IndexModel(IConfiguration configuration) : PageModel
    {
        public List<DrinkingWater> Records { get; set; }

        public void OnGet()
        {
            Records = GetAllRecords();
        }

        public List<DrinkingWater> GetAllRecords()
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = $"SELECT * FROM drinking_water";
            var tableData = new List<DrinkingWater>();
            var reader = tableCmd.ExecuteReader();

            while (reader.Read())
            {
                tableData.Add(new DrinkingWater()
                {
                    Id=reader.GetInt32(0),
                    Date = DateTime.Parse(reader.GetString(1), CultureInfo.CurrentUICulture.DateTimeFormat),
                    Quantity = reader.GetInt32(2)
                });
            }

            return tableData;
        }
    }
}
