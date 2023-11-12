using MeditationTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using System.Globalization;

namespace MeditationTracker.Pages
{
    public class UpdateModel : PageModel
    {
        private readonly IConfiguration _configuration;
        [BindProperty]
        public MeditationModel Meditation { get; set; }

        public UpdateModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult OnGet(int id)
        {
            Meditation = GetById(id);

            return Page();
        }
        private MeditationModel GetById(int id)
        {
            var MeditationModelRecord = new MeditationModel();

            using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                    $"SELECT * FROM Meditation WHERE Id = {id}";

                SqliteDataReader reader = tableCmd.ExecuteReader();

                while (reader.Read())
                {

                    MeditationModelRecord.Id = reader.GetInt32(0);
                    MeditationModelRecord.Date = DateTime.Parse(reader.GetString(1), CultureInfo.CurrentUICulture.DateTimeFormat);
                    MeditationModelRecord.Duration = reader.GetInt32(2);
                }

                return MeditationModelRecord;
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
                tableCmd.CommandText =
                   $"UPDATE Meditation SET date = '{Meditation.Date}', duration = '{Meditation.Duration}' WHERE Id = {Meditation.Id}";
                tableCmd.ExecuteNonQuery();
            }

            return RedirectToPage("./Index");

        }
    }
}
