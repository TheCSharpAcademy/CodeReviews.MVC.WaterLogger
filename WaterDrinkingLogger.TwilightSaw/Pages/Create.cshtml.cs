using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using WaterDrinkingLogger.TwilightSaw.Models;

namespace WaterDrinkingLogger.TwilightSaw.Pages
{
    public class CreateModel(IConfiguration configuration) : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost(string name, int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = $@"
        INSERT INTO 'Records' (date, quantity, measurement, ActionsId) 
        VALUES (@date, @quantity, @measurement, @ActionsId);";

            tableCmd.Parameters.AddWithValue("@date", Record.Date);
            tableCmd.Parameters.AddWithValue("@quantity", Record.Quantity);
            tableCmd.Parameters.AddWithValue("@measurement", Record.Measurement);
            tableCmd.Parameters.AddWithValue("@ActionsId", id);
            tableCmd.ExecuteNonQuery();
            connection.Close();

            return RedirectToPage("./Home", new { name, id });
        }

        [BindProperty]
        public Record Record { get; set; }
    }
}
