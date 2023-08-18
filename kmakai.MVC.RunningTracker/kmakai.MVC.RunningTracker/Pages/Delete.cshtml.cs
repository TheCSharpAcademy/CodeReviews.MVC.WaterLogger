using kmakai.MVC.RunningTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

namespace kmakai.MVC.RunningTracker.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public DeleteModel(IConfiguration configuration)
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

                return run;
            }
        }

        public IActionResult OnPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var connection = new SqliteConnection(_configuration.GetConnectionString("RunningTrackerConnection")))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = $"DELETE FROM Runs WHERE Id = {id}";
                command.ExecuteNonQuery();
            }

            return RedirectToPage("/Index");
        }
    }
}
