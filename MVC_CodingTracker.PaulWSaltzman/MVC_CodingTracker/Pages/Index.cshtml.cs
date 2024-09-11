using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using MVC_CodingTracker.Models;
using System.Globalization;
using System.Security.Cryptography.Xml;

namespace MVC_CodingTracker.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public List<DevelopmentTime> Records { get; set; }

        public IndexModel(IConfiguration configuration) 
        {
            _configuration = configuration;
        }
        public void OnGet()
        {
            Records = GetAllRecords();
            Records = SetDuration(Records);
            ViewData["Total"] = Records.Aggregate(TimeSpan.Zero, (sum, record) => sum.Add((TimeSpan)record.Duration));
        }

        private List<DevelopmentTime> SetDuration(List<DevelopmentTime> records)
        {
            foreach (var record in records) 
            {
                record.Duration = record.DateEnd - record.DateStart;
            }
            return Records;
        }

        private List<DevelopmentTime> GetAllRecords() 
        {
            using (var connection = new
                SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                    $"SELECT * FROM dev_log";

                var tableData = new List<DevelopmentTime>();
                SqliteDataReader reader = tableCmd.ExecuteReader();

                while (reader.Read()) 
                {
                    tableData.Add(
                        new DevelopmentTime
                        {
                            Id = reader.GetInt32(0),
                            DateStart = DateTime.Parse(reader.GetString(1),
                                CultureInfo.CurrentUICulture.DateTimeFormat),
                            DateEnd = DateTime.Parse(reader.GetString(2),
                                CultureInfo.CurrentUICulture.DateTimeFormat),
                            Comments = reader.GetString(3)

                        });
                }
                return tableData;
            }
        }
    }
}
