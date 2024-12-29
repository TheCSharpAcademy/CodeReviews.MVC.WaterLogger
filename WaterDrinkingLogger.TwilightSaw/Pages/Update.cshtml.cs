using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using System.Globalization;
using Action = WaterDrinkingLogger.TwilightSaw.Models.Action;

namespace WaterDrinkingLogger.TwilightSaw.Pages
{
    public class UpdateModel(IConfiguration configuration) : PageModel
    {
        [BindProperty]
        public Action Action { get; set; }
        public IActionResult OnGet(int id, string name)
        {
            Action = GetById(id, name);
            return Page();
        }

        private Action GetById(int id, string name)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = $"SELECT * FROM [{name}] WHERE Id = {id}";
            var tableData = new Action();
            var reader = tableCmd.ExecuteReader();

            while (reader.Read())
            {
                tableData = new Action
                {
                    Id = reader.GetInt32(0),
                    Date = DateTime.Parse(reader.GetString(1), CultureInfo.CurrentUICulture.DateTimeFormat),
                    Quantity = reader.GetDouble(2)
                };
            }
            connection.Close();
            return tableData;
        }

        public IActionResult OnPost(string name)
        {
            if (!ModelState.IsValid) return Page();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = $"UPDATE [{name}] SET Date = @date, quantity = @quantity, measurement = @measurement WHERE Id = @id";
            tableCmd.Parameters.AddWithValue("@date", Action.Date);
            tableCmd.Parameters.AddWithValue("@quantity", Action.Quantity);
            tableCmd.Parameters.AddWithValue("@measurement", Action.Measurement);
            tableCmd.Parameters.AddWithValue("@id", Action.Id);
            tableCmd.ExecuteNonQuery();
            connection.Close();
            return RedirectToPage("./Home", new { name });
        }
    }
}
