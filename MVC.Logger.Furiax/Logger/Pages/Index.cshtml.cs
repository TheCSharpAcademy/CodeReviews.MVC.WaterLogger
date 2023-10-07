using Logger.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using System.Globalization;

namespace Logger.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public List<RideModel> Rides { get; set; }

        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnGet()
        {
            Rides = GetAllRides();
        }

        private List<RideModel> GetAllRides()
        {
            using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = "SELECT * FROM bikerides";

                var rides = new List<RideModel>();
                SqliteDataReader reader = tableCmd.ExecuteReader();

                while (reader.Read())
                {
                    rides.Add(new RideModel
                    {
                        Id = reader.GetInt32(0),
                        Date = DateTime.Parse(reader.GetString(1),
                            CultureInfo.CurrentUICulture.DateTimeFormat),
                        Distance = reader.GetDouble(2),
                        Duration = reader.GetTimeSpan(3)
                    });
                }
                return rides;
            }
        }
    }
}