using Logger.frockett.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

namespace Logger.frockett.Pages
{
    public class CreateModel : PageModel
    {
        private readonly IConfiguration config;

        public CreateModel(IConfiguration configuration)
        {
            config = configuration;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public DailyPushupsModel Pushups { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            using (var connection = new SqliteConnection(config.GetConnectionString("ConnectionString")))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = $"INSERT INTO pushups(date, quantity) VALUES('{Pushups.Date}', {Pushups.Quantity})";
                tableCmd.ExecuteNonQuery();
                connection.Close();
            }

            return RedirectToPage("./Index");
        }
    }
}
