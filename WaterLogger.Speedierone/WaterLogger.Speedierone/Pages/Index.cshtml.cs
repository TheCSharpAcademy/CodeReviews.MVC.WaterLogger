using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using System.Globalization;
using WaterLogger.Speedierone.Model;

namespace WaterLogger.Speedierone.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;
       
        public List<MovieLoggerModel> Movies { get; set; }

        public IndexModel(IConfiguration configuration)
        {    
            _configuration = configuration;
        }

        public void OnGet()
        {
            Movies = GetAllRecords();
        }

        private List<MovieLoggerModel> GetAllRecords()
        {
            using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                    $@"SELECT * FROM MovieLogger";
                var tableData = new List<MovieLoggerModel>();
                SqliteDataReader reader = tableCmd.ExecuteReader();

                while (reader.Read())
                {
                    tableData.Add(new MovieLoggerModel()
                    {
                        Id = reader.GetInt32(0),
                        MovieName = reader.GetString(1),
                        MovieGenre = reader.GetString(2),
                        MovieWatchDate = DateTime.Parse(reader.GetString(3), CultureInfo.CurrentUICulture.DateTimeFormat)
                    });
                }
                return tableData;
            }
        }
    }
}
