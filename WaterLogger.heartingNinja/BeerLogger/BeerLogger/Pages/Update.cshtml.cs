using BeerLogger.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using System.Globalization;

namespace BeerLogger.Pages
{
    public class UpdateModel : PageModel
    {
        private readonly IConfiguration _configuration;

        [BindProperty]
        public DrinkingBeerModel DrinkingBeer { get; set; }

        public UpdateModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult OnGet(int id)
        {
            DrinkingBeer = GetById(id);

            return Page();
        }

        private DrinkingBeerModel GetById(int id)
        {
            var drinkingBeerRecord = new DrinkingBeerModel();

            using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                    $"SELECT * FROM drinking_beer WHERE Id = {id}";

                SqliteDataReader reader = tableCmd.ExecuteReader();

                while (reader.Read())
                {

                    drinkingBeerRecord.Id = reader.GetInt32(0);
                    drinkingBeerRecord.Date = DateTime.Parse(reader.GetString(1), CultureInfo.CurrentUICulture.DateTimeFormat);
                    drinkingBeerRecord.Quantity = reader.GetFloat(2);
                }

                return drinkingBeerRecord;

            }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText =
                   $"UPDATE drinking_beer SET date ='{DrinkingBeer.Date}', quantity = {DrinkingBeer.Quantity} WHERE Id = {DrinkingBeer.Id}";

                tableCmd.ExecuteNonQuery();

                connection.Close();
            }

            return RedirectToPage("./Index");
        }
    }
}
