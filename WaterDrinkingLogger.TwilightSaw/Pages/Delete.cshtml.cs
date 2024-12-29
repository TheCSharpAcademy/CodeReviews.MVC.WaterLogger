using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using System.Globalization;
using Action = WaterDrinkingLogger.TwilightSaw.Models.Action;

namespace WaterDrinkingLogger.TwilightSaw.Pages
{
    public class DeleteModel(IConfiguration configuration) : PageModel
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
                    Quantity = reader.GetInt32(2)
                };
            }
            connection.Close();
            return tableData;
        }

        public IActionResult OnPost(int id, string name)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = $"DELETE FROM [{name}] WHERE Id = {id}";
            tableCmd.ExecuteNonQuery();
            return RedirectToPage("./Home", new {name});
        }
    }
}
