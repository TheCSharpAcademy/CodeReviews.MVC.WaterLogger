using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using System.Globalization;
using WaterLogger.Speedierone.Model;

namespace WaterLogger.Speedierone.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly IConfiguration _configuration;

        [BindProperty]
        public MovieLoggerModel MovieLogger { get; set; }

        public DeleteModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult OnGet(int id)
        {
            MovieLogger = GetById(id);

            return Page();
        }

        private MovieLoggerModel GetById(int id)
        {
            var movieLoggerRecord = new MovieLoggerModel();

            using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                    $@"SELECT * FROM MovieLogger WHERE Id = {id}";

                SqliteDataReader reader = tableCmd.ExecuteReader();

                while (reader.Read())
                {
                    movieLoggerRecord.Id = reader.GetInt32(0);
                    movieLoggerRecord.MovieName = reader.GetString(1);
                    movieLoggerRecord.MovieGenre = reader.GetString(2);
                    movieLoggerRecord.MovieWatchDate = DateTime.Parse(reader.GetString(3),CultureInfo.CurrentUICulture.DateTimeFormat);
                }
                connection.Close();
            }
            return movieLoggerRecord;
        }

        public IActionResult OnPost(int id)
        {
            using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                    $@"DELETE FROM MovieLogger WHERE Id = {id}";
                tableCmd.ExecuteNonQuery();
                connection.Close();
            }
            return RedirectToPage("./Index");
        }
    }
}
