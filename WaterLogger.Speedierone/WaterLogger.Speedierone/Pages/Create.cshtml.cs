using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using WaterLogger.Speedierone.Model;

namespace WaterLogger.Speedierone.Pages
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
        public MovieLoggerModel MovieLogger { get; set; }

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
                    $@"INSERT INTO MovieLogger(""Movie Name"", ""Movie Genre"", ""Movie Watch Date"")
                        VALUES(
                                '{MovieLogger.MovieName}',
                                '{MovieLogger.MovieGenre}',
                                '{MovieLogger.MovieWatchDate}')";

                tableCmd.ExecuteNonQuery();

                connection.Close();
            }
            return RedirectToPage("./Index");
        }
    }
}
