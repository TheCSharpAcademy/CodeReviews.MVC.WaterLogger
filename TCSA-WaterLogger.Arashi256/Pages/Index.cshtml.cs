using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using System.Globalization;
using TCSA_WaterLogger.Arashi256.Models;

namespace TCSA_WaterLogger.Arashi256.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public List<DrinkingWaterModel> Records { get; set; } = new List<DrinkingWaterModel>();
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("ConnectionString") ?? throw new InvalidOperationException("Connection string 'ConnectionString' not found.");
        }

        public void OnGet()
        {
            Records = GetAllRecords();
            ViewData["Total"] = Records.AsEnumerable().Sum(x => x.Quantity);
        }

        private List<DrinkingWaterModel> GetAllRecords()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = "SELECT * FROM drinking_water";
                var tableData = new List<DrinkingWaterModel>();
                SqliteDataReader reader = tableCmd.ExecuteReader();
                while (reader.Read())
                {
                    tableData.Add(new DrinkingWaterModel
                    {
                        Id = reader.GetInt32(0),
                        Date = DateTime.Parse(reader.GetString(1), CultureInfo.CurrentUICulture.DateTimeFormat),
                        Quantity = reader.GetFloat(2),
                        Unit = reader.GetString(3)
                    });
                }
                connection.Close();
                return tableData;
            }
        }
    }
}
