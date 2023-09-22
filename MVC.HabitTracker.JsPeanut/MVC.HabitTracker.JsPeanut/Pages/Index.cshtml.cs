using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using MVC.HabitTracker.JsPeanut.Models;
using System.Globalization;

namespace MVC.HabitTracker.JsPeanut.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public List<HabitLog> HabitLogs { get; set; }

        public List<HabitType> HabitTypes { get; set; }

        public string SumMessage { get; set; }

        public string? SearchString { get; set; }

        public List<HabitLog> SummableHabits { get; set; }
        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnGet(string sorttype, string? searchstring)
        {
            HabitLogs = GetAllLogs();
            HabitTypes = GetAllHabitTypes();

            switch (sorttype)
            {
                case "Today":
                    HabitLogs = HabitLogs.Where(x => x.Date.Day == DateTime.Now.Day).Select(x => x).ToList();
                    break;
                case "Yesterday":
                    HabitLogs = HabitLogs.Where(x => x.Date.Day == (DateTime.Now.Day - 1)).Select(x => x).ToList();
                    break;
                case "DateAdded":
                    HabitLogs = HabitLogs.OrderByDescending(x => x.Date).ToList();
                    break;
                case "AddedOrder":
                    HabitLogs = GetAllLogs();
                    break;
            }

            if (!string.IsNullOrEmpty(searchstring))
            {
                DateTime searchDate;
                if (DateTime.TryParse(searchstring, out searchDate))
                {
                    HabitLogs = HabitLogs.Where(x => x.Date.Date == searchDate.Date).ToList();
                }
            }
        }

        private List<HabitLog> GetAllLogs()
        {
            using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                connection.Open();

                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText =
                    "SELECT * FROM habit_logs";

                var tableData = new List<HabitLog>();
                SqliteDataReader reader = tableCmd.ExecuteReader();

                while (reader.Read())
                {
                    tableData.Add(
                        new HabitLog
                        {
                            Id = reader.GetInt32(0),
                            HabitTypeName = reader.GetString(1),
                            Date = DateTime.Parse(reader.GetString(2), CultureInfo.InstalledUICulture),
                            StartTime = reader.IsDBNull(3) ? DateTime.MinValue : DateTime.Parse(reader.GetString(3)),
                            EndTime = reader.IsDBNull(4) ? DateTime.MinValue : DateTime.Parse(reader.GetString(4)),
                            Time = reader.IsDBNull(5) ? TimeSpan.Zero : TimeSpan.Parse(reader.GetString(5)),
                            Quantity = reader.GetInt32(6)
                        }
                    );
                }

                return tableData;
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

        public void OnPost(string habittypename) 
        {
            HabitLogs = GetAllLogs();
            HabitTypes = GetAllHabitTypes();
            List<HabitLog> SummableHabits = HabitLogs.Where(x => x.HabitTypeName == habittypename).ToList();
            HabitType HabitType = HabitTypes.Where(x => x.Name == habittypename).First();
            string measurability = HabitType.Measurability;
            int sum = 0;
            TimeSpan sum2 = TimeSpan.Zero;
            foreach (var record in SummableHabits)
            {
                if (measurability == "quantifiable")
                {
                    sum += record.Quantity;
                }
                else if (measurability == "duration")
                {
                    sum2 += record.Time.GetValueOrDefault();
                }
                else
                {
                    sum += record.Quantity;
                }
            }

            if (sum != 0)
            {
                SumMessage = $"The sum of your {habittypename} habit is equal to {sum}.";
            }
            else if (sum2 != TimeSpan.Zero)
            {
                SumMessage = $"The sum of your {habittypename} habit is equal to {sum2}.";
            }
        }
    }
}