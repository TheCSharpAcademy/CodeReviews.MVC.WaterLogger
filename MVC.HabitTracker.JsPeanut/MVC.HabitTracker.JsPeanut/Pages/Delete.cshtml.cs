using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using MVC.HabitTracker.JsPeanut.Models;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MVC.HabitTracker.JsPeanut.Pages
{
    public class DeleteModel : PageModel
    {
        public IConfiguration _configuration { get; set; }

        public List<HabitType> HabitTypes { get; set; }

        [BindProperty]
        public HabitLog HabitLog { get; set; }

        public DeleteModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult OnGet(int id)
        {
            HabitTypes = GetAllHabitTypes();
            HabitLog = GetById(id);
            return Page();
        }

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
                    habitLogRecord.StartTime = reader.IsDBNull(3) ? DateTime.MinValue : DateTime.Parse(reader.GetString(3));
                    habitLogRecord.EndTime = reader.IsDBNull(4) ? DateTime.MinValue : DateTime.Parse(reader.GetString(4));
                    habitLogRecord.Time = reader.IsDBNull(5) ? TimeSpan.Zero : TimeSpan.Parse(reader.GetString(5));
                    habitLogRecord.Quantity = reader.GetInt32(6);
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

        public IActionResult OnPost(int id)
        {
            using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                connection.Open();

                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText =
                    $"DELETE FROM habit_logs WHERE Id = {id}";

                tableCmd.ExecuteNonQuery();
            }

            return RedirectToPage("./Index");
        }
    }
}
