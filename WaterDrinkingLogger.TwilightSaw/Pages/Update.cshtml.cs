using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using System.Globalization;
using WaterDrinkingLogger.TwilightSaw.Models;

namespace WaterDrinkingLogger.TwilightSaw.Pages
{
    public class UpdateModel(IConfiguration configuration) : PageModel
    {
        [BindProperty]
        public DrinkingWater DrinkingWater { get; set; }
        public IActionResult OnGet(int id)
        {
            DrinkingWater = GetById(id);
            return Page();
        }

        private DrinkingWater GetById(int id)
        {
            var connectionString = "Data Source=WaterLogger.db";
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = $"SELECT * FROM drinking_water WHERE Id = {id}";
            var tableData = new DrinkingWater();
            var reader = tableCmd.ExecuteReader();

            while (reader.Read())
            {
                tableData = new DrinkingWater
                {
                    Id = reader.GetInt32(0),
                    Date = DateTime.Parse(reader.GetString(1), CultureInfo.CurrentUICulture.DateTimeFormat),
                    Quantity = reader.GetInt32(2)
                };
            }
            return tableData;
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            var connectionString = "Data Source=WaterLogger.db";
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = $"UPDATE drinking_water SET date = '{DrinkingWater.Date}'," +
                                   $" quantity = {DrinkingWater.Quantity} WHERE Id = {DrinkingWater.Id}";
            tableCmd.ExecuteNonQuery();
            return RedirectToPage("./Index");
        }
    }
}
