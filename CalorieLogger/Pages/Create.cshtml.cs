using CalorieLogger.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

namespace CalorieLogger.Pages
{
    public class CreateModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public CreateModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public CalorieIntakeModel CalorieIntakeModel { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            using (
                var connection = new SqliteConnection(
                    _configuration.GetConnectionString("DefaultConnection")
                )
            )
            {
                connection.Open();

                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                    @$"INSERT INTO calorie_intake(Date, Food, Calories) 
                      VALUES('{CalorieIntakeModel.Date}',
                              '{CalorieIntakeModel.Food}', 
                              {CalorieIntakeModel.Calories})";

                tableCmd.ExecuteNonQuery();

                connection.Close();
            }
            return RedirectToPage("Index");
        }
    }
}
