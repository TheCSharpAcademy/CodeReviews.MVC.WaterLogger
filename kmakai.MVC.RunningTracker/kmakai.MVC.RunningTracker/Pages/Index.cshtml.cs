using kmakai.MVC.RunningTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

namespace kmakai.MVC.RunningTracker.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _configuration;

        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [BindProperty]
        public List<RunModel> Runs { get; set; } = default!;
        public void OnGet()
        {
           Runs = new List<RunModel>();
            using (var connection = new SqliteConnection(_configuration.GetConnectionString("RunningTrackerConnection")) ?? throw new InvalidOperationException("Connection string 'runningtracker' not found."))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = "SELECT * FROM Runs";

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Runs.Add(new RunModel
                    {
                        Id = reader.GetInt32(0),
                        Date = reader.GetDateTime(1),
                        Miles = reader.GetDouble(2),
                        Minutes = reader.GetDouble(3)
                    });
                }
            }
        }
    }
}