using MeditationTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using System.Globalization;

namespace MeditationTracker.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public List<MeditationModel> Records { get; set; }
        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void OnGet()
        {
            Records = GetAllRecords();
            ViewData["Total"] = Records.AsEnumerable().Sum(x => x.Duration);
        }
        private List<MeditationModel> GetAllRecords()
        {
            using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                    $"SELECT * FROM Meditation ";

                var tableData = new List<MeditationModel>();
                SqliteDataReader reader = tableCmd.ExecuteReader();

                while (reader.Read())
                {
                    tableData.Add(
                    new MeditationModel
                    {
                        Id = reader.GetInt32(0),
                        Date = DateTime.Parse(reader.GetString(1), CultureInfo.CurrentUICulture.DateTimeFormat),
                        Duration = reader.GetInt32(2)
                    });
                }

                return tableData;
            }
        }
    }
}