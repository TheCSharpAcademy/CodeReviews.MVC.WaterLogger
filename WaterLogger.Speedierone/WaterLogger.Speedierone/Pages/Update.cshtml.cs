using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using System.Globalization;
using WaterLogger.Speedierone.Model;

namespace WaterLogger.Speedierone.Pages
{
    public class UpdateModel : PageModel
    {
        private readonly IConfiguration _configuration;

        [BindProperty]
        public MovieLoggerModel MovieLogger { get; set; }

        public UpdateModel(IConfiguration configuration)
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
                    $@"UPDATE MovieLogger SET ""Movie Name"" = '{MovieLogger.MovieName}',
                    ""Movie Genre"" ='{MovieLogger.MovieGenre}',
                    ""Movie Watch Date""='{MovieLogger.MovieWatchDate}' WHERE Id = {MovieLogger.Id}";

                tableCmd.ExecuteNonQuery();
                connection.Close();
            }
            return RedirectToPage("./Index");
        }
    }
}
