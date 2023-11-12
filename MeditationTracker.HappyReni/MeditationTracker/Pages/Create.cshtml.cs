using MeditationTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

namespace MeditationTracker.Pages
{
    public class CreateModel : PageModel
    {
        private readonly IConfiguration _configuration;
        [BindProperty]
        public MeditationModel Meditation { get; set; }
        public CreateModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                   @$"INSERT INTO Meditation(date, duration)
                      VALUES('{Meditation.Date}', {Meditation.Duration})";

                tableCmd.ExecuteNonQuery();
            }

            return RedirectToPage("./Index");

        }
    }
}
