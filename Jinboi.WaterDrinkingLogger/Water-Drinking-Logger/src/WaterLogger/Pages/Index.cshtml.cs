using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using System.Globalization;
using WaterLogger.Models;

namespace WaterLogger.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public List<DrinkingWaterModel> Records { get; set; }

        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void OnGet()
        {
            Records = GetAllRecords();
            ViewData["Total"] = Records.AsEnumerable().Sum(x => x.Quantity);
        }
        private List<DrinkingWaterModel> GetAllRecords()
        {
            using (var connection = new
                SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                    $"SELECT * FROM drinking_water ";

                var tableData = new List<DrinkingWaterModel>();
                SqliteDataReader reader = tableCmd.ExecuteReader();

                while (reader.Read())
                {
                    tableData.Add(
                    new DrinkingWaterModel
                    {
                        Id = reader.GetInt32(0),
                        Date = DateTime.Parse(reader.GetString(1), CultureInfo.CurrentUICulture.DateTimeFormat),
                        Quantity = reader.GetInt32(2)
                    }); 
                }

                return tableData;
            }
        }
    }
}
