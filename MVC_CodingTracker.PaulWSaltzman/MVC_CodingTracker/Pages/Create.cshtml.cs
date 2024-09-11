using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using MVC_CodingTracker.Models;
using System.Reflection.Metadata.Ecma335;

namespace MVC_CodingTracker.Pages
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
        public DevelopmentTime DevelopmentTime { get; set; }

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
                    @$"INSERT INTO dev_log(DtStart, DtEnd, Comments) 
                      VALUES(
                          '{DevelopmentTime.DateStart}',
                          '{DevelopmentTime.DateEnd}',
                          '{DevelopmentTime.Comments}')";

                tableCmd.ExecuteNonQuery();

                connection.Close();
            }

            return RedirectToPage("./Index");
        }
        
    }
}
