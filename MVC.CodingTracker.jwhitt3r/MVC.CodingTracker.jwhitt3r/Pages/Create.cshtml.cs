using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using MVC.CodingTracker.jwhitt3r.Models;

namespace MVC.CodingTracker.jwhitt3r.Pages
{
    public class CreateModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public CreateModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult OnGet()
        {
            return Page();
        }
        [BindProperty]
        public CodingTrackerModel CodingTracker { get; set; }

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
                tableCmd.CommandText = $"INSERT INTO coding_tracker(date, quantity) VALUES('{CodingTracker.Date}', {CodingTracker.Quantity})";
                tableCmd.ExecuteNonQuery();
                connection.Close();
            }

            return RedirectToPage("./Index");
        }
    }
}
