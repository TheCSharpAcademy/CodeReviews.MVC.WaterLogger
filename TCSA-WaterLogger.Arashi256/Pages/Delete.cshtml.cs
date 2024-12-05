using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using TCSA_WaterLogger.Arashi256.Models;

namespace TCSA_WaterLogger.Arashi256.Pages
{
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public DrinkingWaterModel DrinkingWater { get; set; } = new DrinkingWaterModel();
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DeleteModel(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("ConnectionString") ?? throw new InvalidOperationException("Connection string 'ConnectionString' not found.");
        }

        public IActionResult OnGet(int id)
        {
            DrinkingWater = GetById(id);
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = "DELETE FROM drinking_water WHERE Id = @Id";
                tableCmd.Parameters.AddWithValue("@Id", id);
                tableCmd.ExecuteNonQuery();
                connection.Close();
            }
            return RedirectToPage("./Index");
        }

        private DrinkingWaterModel GetById(int id)
        {
            var drinkingWaterRecord = new DrinkingWaterModel();
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = "SELECT * FROM drinking_water WHERE Id = @Id";
                tableCmd.Parameters.AddWithValue("@Id", id);
                var tableData = new List<DrinkingWaterModel>();
                SqliteDataReader reader = tableCmd.ExecuteReader();
                while (reader.Read())
                {
                    drinkingWaterRecord.Id = reader.GetInt32(0);
                    drinkingWaterRecord.Date = DateTime.Parse(reader.GetString(1));
                    drinkingWaterRecord.Quantity = reader.GetFloat(2);
                }
                connection.Close();
                return drinkingWaterRecord;
            }
        }
    }
}