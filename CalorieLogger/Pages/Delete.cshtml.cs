using System.Globalization;
using CalorieLogger.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

namespace CalorieLogger.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly IConfiguration _configuration;

        [BindProperty]
        public CalorieIntakeModel CalorieIntakeModel { get; set; }

        public DeleteModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult OnGet(int id)
        {
            CalorieIntakeModel = GetById(id);
            return Page();
        }

        private CalorieIntakeModel GetById(int id)
        {
            var calorieIntakeModel = new CalorieIntakeModel();

            using (
                var connection = new SqliteConnection(
                    _configuration.GetConnectionString("DefaultConnection")
                )
            )
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = $"SELECT * FROM calorie_intake WHERE Id = {id}";

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    calorieIntakeModel.Id = reader.GetInt32(0);
                    calorieIntakeModel.Date = DateTime.Parse(
                        reader.GetString(1),
                        CultureInfo.InvariantCulture
                    );
                    calorieIntakeModel.Food = reader.GetString(2);
                    calorieIntakeModel.Calories = reader.GetInt32(3);
                }

                connection.Close();
            }
            return calorieIntakeModel;
        }

        public IActionResult OnPost(int id)
        {
            using (
                var connection = new SqliteConnection(
                    _configuration.GetConnectionString("DefaultConnection")
                )
            )
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = $"DELETE FROM calorie_intake WHERE Id = {id}";

                command.ExecuteNonQuery();

                connection.Close();
            }

            return RedirectToPage("./Index");
        }
    }
}
