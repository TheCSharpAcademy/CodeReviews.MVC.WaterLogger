using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using WaterDrinkingLogger.TwilightSaw.Models;
using Action = WaterDrinkingLogger.TwilightSaw.Models.Action;

namespace WaterDrinkingLogger.TwilightSaw.Pages
{
    public class CreateModel(IConfiguration configuration) : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost(string name)
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
        INSERT INTO '{name}' (date, quantity) 
        VALUES (@date, @quantity);";

            tableCmd.Parameters.AddWithValue("@date", Action.Date);
            tableCmd.Parameters.AddWithValue("@quantity", Action.Quantity);
            tableCmd.ExecuteNonQuery();
            connection.Close();

            return RedirectToPage("./Home", new { name });
        }

        [BindProperty]
        public Action Action { get; set; }
    }
}
