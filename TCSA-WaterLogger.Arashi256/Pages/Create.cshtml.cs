using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using TCSA_WaterLogger.Arashi256.Models;

namespace TCSA_WaterLogger.Arashi256.Pages
{
    public class CreateModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString = String.Empty;

        public CreateModel(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("ConnectionString") ?? throw new InvalidOperationException("Connection string 'ConnectionString' not found.");
        }

        public IActionResult OnGet()
        {
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
                tableCmd.CommandText = "INSERT INTO drinking_water(Date, Quantity, Unit) VALUES (@Date, @Quantity, @Unit)";
                tableCmd.Parameters.AddWithValue("@Date", DrinkingWater.Date);
                tableCmd.Parameters.AddWithValue("@Quantity", DrinkingWater.Quantity);
                tableCmd.Parameters.AddWithValue("@Unit", DrinkingWater.Unit);
                tableCmd.ExecuteNonQuery();
                connection.Close();
            }
            return RedirectToPage("./Index");
        }

        [BindProperty]
        public required DrinkingWaterModel DrinkingWater { get; set; }
    }
}
