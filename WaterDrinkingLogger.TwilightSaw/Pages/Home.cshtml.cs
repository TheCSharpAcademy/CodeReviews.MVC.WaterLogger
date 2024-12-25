using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using WaterDrinkingLogger.TwilightSaw.Models;

namespace WaterDrinkingLogger.TwilightSaw.Pages
{
    public class HomeModel(IConfiguration configuration) : PageModel
    {
        public List<DrinkingWater> Records { get; set; }

        public string Name { get; set; }

        public void OnGet(string name)
        {
            Records = GetAllRecords(name);
            Name = name;
        }

        public List<DrinkingWater> GetAllRecords(string name)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            var tableCmd = connection.CreateCommand();
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Table name cannot be null or empty.");
            }
            tableCmd.CommandText = $"SELECT * FROM '{name}'";
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
