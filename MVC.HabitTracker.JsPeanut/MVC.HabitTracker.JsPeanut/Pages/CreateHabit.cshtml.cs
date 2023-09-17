using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using MVC.HabitTracker.JsPeanut.Models;

namespace MVC.HabitTracker.JsPeanut.Pages
{
    public class CreateHabitModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public CreateHabitModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public HabitType HabitType { get; set; }

        public IActionResult OnPost()
        {
            if (HabitType.Measurability == "Check-in")
            {
                HabitType.UnitOfMeasurement = "Done";
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                    $@"INSERT INTO habit_types(Id, ImagePath, Name, Measurability, UnitOfMeasurement)
                VALUES(1, '{HabitType.ImagePath}', '{HabitType.Name}', '{HabitType.Measurability}', '{HabitType.UnitOfMeasurement}')";

                tableCmd.ExecuteNonQuery();
            }

            return RedirectToPage("./Index");
        }
    }
}
