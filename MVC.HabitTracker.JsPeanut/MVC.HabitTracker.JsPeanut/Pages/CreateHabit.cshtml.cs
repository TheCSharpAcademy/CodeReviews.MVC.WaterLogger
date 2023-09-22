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
            HabitTypes = GetAllHabitTypes();

            return Page();
        }

        public List<HabitType> HabitTypes { get; set; }

        [BindProperty]
        public HabitType HabitType { get; set; }

        private List<HabitType> GetAllHabitTypes()
        {
            using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                connection.Open();

                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText =
                    "SELECT * FROM habit_types";

                var tableData = new List<HabitType>();
                SqliteDataReader reader = tableCmd.ExecuteReader();

                while (reader.Read())
                {
                    tableData.Add(
                        new HabitType
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            ImagePath = reader.GetString(2),
                            Measurability = reader.GetString(3),
                            UnitOfMeasurement = reader.GetString(4)
                        }
                    );
                }

                return tableData;
            }
        }

        public IActionResult OnPost()
        {
            HabitTypes = GetAllHabitTypes();

           if (HabitTypes.Where(x => x.Name == HabitType.Name).ToList().Any()) 
            {
                ModelState.AddModelError(string.Empty, "The name of your habit must be unique.");

                return Page();
            }

            if (HabitType.Measurability == "Check-in")
            {
                HabitType.UnitOfMeasurement = "Done";
            }
            else if (HabitType.Measurability == "Duration")
            {
                HabitType.UnitOfMeasurement = "none";
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
