using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using MVC.HabitTracker.JsPeanut.Models;

namespace MVC.HabitTracker.JsPeanut.Pages
{
    public class LogHabitModel : PageModel
    {
        public IConfiguration _configuration { get; set; }

        public List<HabitType> HabitTypes { get; set; }

        public LogHabitModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult OnGet()
        {
            HabitTypes = GetAllHabitTypes();
            return Page();
        }

        [BindProperty]
        public HabitLog HabitLog { get; set; }

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
                            ImagePath = reader.GetString(0),
                            Name = reader.GetString(1),
                            UnitOfMeasurement = reader.GetString(2)
                        }
                    );
                }

                return tableData;
            }
        }

        public IActionResult OnPost()
        {
            HabitTypes = GetAllHabitTypes();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                    $@"INSERT INTO habit_logs(HabitTypeName, Date, Quantity)
                        VALUES('{HabitLog.HabitTypeName}', '{HabitLog.Date}', {HabitLog.Quantity})";

                tableCmd.ExecuteNonQuery();
            }

            return RedirectToPage("./Index");
        }
    }
}
