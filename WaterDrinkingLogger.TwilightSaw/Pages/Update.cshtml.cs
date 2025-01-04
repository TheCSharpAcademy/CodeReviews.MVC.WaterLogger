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
        public Record Record { get; set; }
        public IActionResult OnGet(int recordId, int actionId, string name)
        {
            Record = GetById(recordId);
            return Page();
        }

        private Record GetById(int id)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = $"SELECT * FROM Records WHERE Id = {id}";
            var tableData = new Record();
            var reader = tableCmd.ExecuteReader();

            while (reader.Read())
            {
                tableData = new Record
                {
                    Id = reader.GetInt32(0),
                    Date = DateTime.Parse(reader.GetString(1), CultureInfo.CurrentUICulture.DateTimeFormat),
                    Quantity = reader.GetDouble(2)
                };
            }
            connection.Close();
            return tableData;
        }

        public IActionResult OnPost(string name, int actionId)
        {
            if (!ModelState.IsValid) return Page();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = $"UPDATE Records SET Date = @date, quantity = @quantity, measurement = @measurement WHERE Id = @id";
            tableCmd.Parameters.AddWithValue("@date", Record.Date);
            tableCmd.Parameters.AddWithValue("@quantity", Record.Quantity);
            tableCmd.Parameters.AddWithValue("@measurement", Record.Measurement);
            tableCmd.Parameters.AddWithValue("@id", Record.Id);
            tableCmd.ExecuteNonQuery();
            connection.Close();
            return RedirectToPage("./Home", new { name, id = actionId});
        }
    }
}
