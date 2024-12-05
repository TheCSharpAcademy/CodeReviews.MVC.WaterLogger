using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using TCSA_WaterLogger.Arashi256.Models;

namespace TCSA_WaterLogger.Arashi256.Pages
{
    public class UpdateModel : PageModel
    {
        [BindProperty]
        public DrinkingWaterModel DrinkingWater { get; set; } = new DrinkingWaterModel();
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public UpdateModel(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("ConnectionString") ?? throw new InvalidOperationException("Connection string 'ConnectionString' not found.");
        }
        public IActionResult OnGet(int id)
        {
            DrinkingWater = GetById(id);
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = "UPDATE drinking_water SET Date = @Date, Quantity = @Quantity, Unit = @Unit WHERE Id = @Id";
                tableCmd.Parameters.AddWithValue("@Id", DrinkingWater.Id);
                tableCmd.Parameters.AddWithValue("@Date", DrinkingWater.Date);
                tableCmd.Parameters.AddWithValue("@Quantity", DrinkingWater.Quantity);
                tableCmd.Parameters.AddWithValue("@Unit", DrinkingWater.Unit);
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
                    drinkingWaterRecord.Unit = reader.GetString(3);
                }
                connection.Close();
                return drinkingWaterRecord;
            }
        }
    }
}
