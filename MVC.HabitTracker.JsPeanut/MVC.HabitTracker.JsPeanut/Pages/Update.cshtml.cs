using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using MVC.HabitTracker.JsPeanut.Models;
using System.Globalization;

namespace MVC.HabitTracker.JsPeanut.Pages
{
    public class UpdateModel : PageModel
    {
        public IConfiguration _configuration { get; set; }

        public List<HabitType> HabitTypes { get; set; }

        public UpdateModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult OnGet(int id)
        {
            HabitLog = GetById(id);
            HabitTypes = GetAllHabitTypes();

            return Page();
        }

        [BindProperty]
        public HabitLog HabitLog { get; set; }

        private HabitLog GetById(int id)
        {
            var habitLogRecord = new HabitLog();

            using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                    $"SELECT * FROM habit_logs WHERE Id = {id}";

                SqliteDataReader reader = tableCmd.ExecuteReader();

                while (reader.Read())
                {
                    habitLogRecord.Id = reader.GetInt32(0);
                    habitLogRecord.HabitTypeName = reader.GetString(1);
                    habitLogRecord.Date = DateTime.Parse(reader.GetString(2), CultureInfo.InstalledUICulture);
                    habitLogRecord.Quantity = reader.GetInt32(3);
                }

                return habitLogRecord;
            }
        }

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

        public IActionResult OnPost(int id)
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
                    $"UPDATE habit_logs SET HabitTypeName = '{HabitLog.HabitTypeName}', Date = '{HabitLog.Date}', Quantity = {HabitLog.Quantity} WHERE Id = {id}";

                tableCmd.ExecuteNonQuery();
            }

            return RedirectToPage("./Index");
        }
    }
}
