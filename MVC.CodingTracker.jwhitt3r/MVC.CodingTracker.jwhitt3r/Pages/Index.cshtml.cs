using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using MVC.CodingTracker.jwhitt3r.Models;
using System.Globalization;

namespace MVC.CodingTracker.jwhitt3r.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public List<CodingTrackerModel> Records { get; set; }

        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnGet()
        {
            Records = GetAllRecords();
            ViewData["Total"] = Records.AsEnumerable().Sum(x => x.Quantity);
        }

        private List<CodingTrackerModel> GetAllRecords()
        {
            using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = $"SELECT * FROM coding_tracker";
                var tableData = new List<CodingTrackerModel>();
                SqliteDataReader reader = tableCmd.ExecuteReader();

                while (reader.Read())
                {
                    tableData.Add(new CodingTrackerModel { Id = reader.GetInt32(0), Date = DateTime.Parse(reader.GetString(1), CultureInfo.CurrentUICulture.DateTimeFormat), Quantity = reader.GetInt32(2) });
                }

                return tableData;
            }
        }
    }
}