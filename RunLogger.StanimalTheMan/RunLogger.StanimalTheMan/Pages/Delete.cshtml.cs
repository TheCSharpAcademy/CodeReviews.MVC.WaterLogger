using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using RunLogger.StanimalTheMan.Models;

namespace RunLogger.StanimalTheMan.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly IConfiguration _configuration;

        [BindProperty]
        public MilesRunModel MilesRun { get; set; }

        public DeleteModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult OnGet(int id)
        {
            MilesRun = GetById(id);

            return Page();
        }

        private MilesRunModel GetById(int id)
        {
            var milesRunRecord = new MilesRunModel();

            using (var connection = new
                SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                    $"SELECT * FROM miles_run WHERE Id = {id}";

                SqliteDataReader reader = tableCmd.ExecuteReader();

                while (reader.Read())
                {
                    milesRunRecord.Id = reader.GetInt32(0);
                    milesRunRecord.Date = DateTime.Parse(reader.GetString(1),
                        CultureInfo.CurrentUICulture.DateTimeFormat);
                    milesRunRecord.Distance = reader.GetInt32(2);
                }

                return milesRunRecord;

            }
        }

        public IActionResult OnPost(int id)
        {
            using (var connection = new
                SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText = $"DELETE from miles_run WHERE Id = {id}";

                tableCmd.ExecuteNonQuery();
            }

            return RedirectToPage("./Index");
        }
    }
}
