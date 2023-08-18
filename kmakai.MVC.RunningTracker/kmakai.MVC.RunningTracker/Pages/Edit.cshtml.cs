using kmakai.MVC.RunningTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

namespace kmakai.MVC.RunningTracker.Pages
{
    public class EditModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public EditModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public RunModel Run { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Run = GetRun(id);

            return Page();
        }

        private RunModel GetRun(int? id)
        {
            RunModel run = null;

            using (var connection = new SqliteConnection(_configuration.GetConnectionString("RunningTrackerConnection")))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = $"SELECT * FROM Runs WHERE Id = {id}";
                command.ExecuteNonQuery();

                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        run = new RunModel
                        {
                            Id = reader.GetInt32(0),
                            Date = reader.GetDateTime(1),
                            Miles = reader.GetDouble(2),
                            Minutes = reader.GetInt32(3)
                        };
                    }
                }

                reader.Close();

            }

            if (run == null)
                throw new Exception($"Run with id {id} not found.");

            return run;
        }

        public IActionResult OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            using(var connection = new SqliteConnection(_configuration.GetConnectionString("RunningTrackerConnection")))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = $"UPDATE Runs SET Date = '{Run.Date}', Miles = '{Run.Miles}', Minutes = '{Run.Minutes}' WHERE Id = {Run.Id}";

                command.ExecuteNonQuery();

            }

            return RedirectToPage("/Index");
        }
    }
}
