using System.Globalization;
using CalorieLogger.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

namespace CalorieLogger.Pages
{
    public class UpdateModel : PageModel
    {
        private readonly IConfiguration _configuration;

        [BindProperty]
        public CalorieIntakeModel CalorieIntakeModel { get; set; }

        public UpdateModel(IConfiguration configuration)
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

                var command = connection.CreateCommand();
                command.CommandText =
                    @$"UPDATE calorie_intake 
                       SET Date = @Date, Food = @Food, Calories = @Calories 
                       WHERE Id = {id}";

                command.Parameters.AddWithValue(
                    "@Date",
                    CalorieIntakeModel.Date.ToString("MM/dd/yyyy")
                );
                command.Parameters.AddWithValue("@Food", CalorieIntakeModel.Food);
                command.Parameters.AddWithValue("@Calories", CalorieIntakeModel.Calories);
                command.ExecuteNonQuery();

                connection.Close();
            }

            return RedirectToPage("./Index");
        }
    }
}
