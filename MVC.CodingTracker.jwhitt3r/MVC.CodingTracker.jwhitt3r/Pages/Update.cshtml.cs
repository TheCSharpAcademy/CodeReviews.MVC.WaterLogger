using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVC.CodingTracker.jwhitt3r.Models;
using System.Globalization;
using Microsoft.Data.Sqlite;

namespace MVC.CodingTracker.jwhitt3r.Pages
{
    public class UpdateModel : PageModel
    {
        private readonly IConfiguration _configuration;
        [BindProperty]
        public CodingTrackerModel CodingTracker { get; set; }

        public UpdateModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult OnGet(int id)
        {
            CodingTracker = GetById(id);
            return Page();
        }

        private CodingTrackerModel GetById(int id)
        {
            var codingTrackerRecord = new CodingTrackerModel();
            using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = $"SELECT * FROM coding_tracker WHERE Id = {id}";
                SqliteDataReader reader = tableCmd.ExecuteReader();
                while (reader.Read())
                {
                    codingTrackerRecord.Id = reader.GetInt32(0);
                    codingTrackerRecord.Date = DateTime.Parse(reader.GetString(1), CultureInfo.CurrentUICulture.DateTimeFormat);
                    codingTrackerRecord.Quantity = reader.GetInt32(2);
                }
                return codingTrackerRecord;
            }
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
                tableCmd.CommandText = $"UPDATE coding_tracker SET date ='{CodingTracker.Date}', quantity = {CodingTracker.Quantity} WHERE Id = {CodingTracker.Id}";
                tableCmd.ExecuteNonQuery();
                connection.Close();
            }

            return RedirectToPage("./Index");
        }
    }
}
