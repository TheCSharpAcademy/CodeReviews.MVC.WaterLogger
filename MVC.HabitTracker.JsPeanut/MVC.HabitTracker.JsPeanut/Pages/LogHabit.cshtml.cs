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

            var measurability = HabitTypes.Where(x => x.Name == HabitLog.HabitTypeName).First().Measurability;

            if (measurability == "duration")
            {
                HabitLog.Time = HabitLog.EndTime - HabitLog.StartTime;
                HabitLog.Quantity = 1;
                HabitLog.Date = HabitLog.StartTime.GetValueOrDefault();
            }
            else
            {
                HabitLog.Time = TimeSpan.Zero;
            }

            if (measurability == "check-in")
            {
                HabitLog.StartTime = DateTime.MinValue;
                HabitLog.EndTime = DateTime.MinValue;
                HabitLog.Time = TimeSpan.Zero;
                HabitLog.Quantity = 1;
            }

            if (measurability == "quantifiable")
            {
                HabitLog.StartTime = DateTime.MinValue;
                HabitLog.EndTime = DateTime.MinValue;
                HabitLog.Time = TimeSpan.Zero;
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
                    $@"INSERT INTO habit_logs(HabitTypeName, Date, StartTime, EndTime, Time, Quantity)
                        VALUES('{HabitLog.HabitTypeName}', '{HabitLog.Date}', '{HabitLog.StartTime}', '{HabitLog.EndTime}', '{HabitLog.Time}', {HabitLog.Quantity})";

                tableCmd.ExecuteNonQuery();
            }

            return RedirectToPage("./Index");
        }
    }
}
