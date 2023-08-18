using kmakai.MVC.RunningTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

namespace kmakai.MVC.RunningTracker.Pages
{
    public class CreateModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public CreateModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public RunModel Run { get; set; } = default!;


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


            using (var connection = new SqliteConnection(_configuration.GetConnectionString("RunningTrackerConnection")) ?? throw new InvalidOperationException("Connection string 'runningtracker' not found."))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = $"INSERT INTO Runs (Date, Miles, Minutes) VALUES ('{Run.Date}', '{Run.Miles}', '{Run.Minutes}')";

                command.ExecuteNonQuery();
            }

            return RedirectToPage("/Index");
        }
    }
}
