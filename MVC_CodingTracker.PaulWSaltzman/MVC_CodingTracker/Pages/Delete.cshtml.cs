using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using MVC_CodingTracker.Models;
using System.Globalization;

namespace MVC_CodingTracker.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly IConfiguration _configuration;

        [BindProperty]
        public DevelopmentTime DevelopmentTime { get; set; }

        public DeleteModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult OnGet(int id)
        {
            DevelopmentTime = GetById(id);

            return Page();
        }

        private DevelopmentTime GetById(int id) 
        { 
            var developmentTimeRecord = new DevelopmentTime();

            using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = $"SELECT * FROM dev_log WHERE id = {id}";


                SqliteDataReader reader = tableCmd.ExecuteReader();

                while (reader.Read()) 
                {
                    developmentTimeRecord.Id = reader.GetInt32(0);
                    developmentTimeRecord.DateStart = DateTime.Parse(reader.GetString(1), CultureInfo.CurrentUICulture.DateTimeFormat);
                    developmentTimeRecord.DateEnd = DateTime.Parse(reader.GetString(2), CultureInfo.CurrentUICulture.DateTimeFormat);
                    developmentTimeRecord.Comments = reader.GetString(3);
                }
                return developmentTimeRecord;
            }
        }

        public IActionResult OnPost(int id)
        {
            using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText = $"DELETE from dev_log WHERE Id = {id}";

                tableCmd.ExecuteNonQuery();
            }

            return RedirectToPage("./Index");
        }
    }
}
