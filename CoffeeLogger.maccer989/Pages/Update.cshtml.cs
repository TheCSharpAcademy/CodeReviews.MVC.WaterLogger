using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using CoffeeLogger.Models;

namespace CoffeeLogger.Pages
{

    public class UpdateModel : PageModel
    {
        private readonly IConfiguration _configuration;

        [BindProperty]
        public DrinkingCoffeeModel DrinkingCoffee { get; set; }

        public UpdateModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult OnGet(int id)
        {
            DrinkingCoffee = GetById(id);

            return Page();
        }

        private DrinkingCoffeeModel GetById(int id)
        {
            var drinkingCoffeeRecord = new DrinkingCoffeeModel();

            using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                    $"SELECT * FROM drinking_coffee WHERE Id = {id}";

                SqliteDataReader reader = tableCmd.ExecuteReader();

                while (reader.Read())
                {

                    drinkingCoffeeRecord.Id = reader.GetInt32(0);
                    drinkingCoffeeRecord.Date = DateTime.Parse(reader.GetString(1), CultureInfo.CurrentUICulture.DateTimeFormat);
                    drinkingCoffeeRecord.Quantity = reader.GetInt32(2);
                }

                return drinkingCoffeeRecord;

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
                   $"UPDATE drinking_coffee SET date ='{DrinkingCoffee.Date}', quantity = {DrinkingCoffee.Quantity} WHERE Id = {DrinkingCoffee.Id}";

                tableCmd.ExecuteNonQuery();
            }

            return RedirectToPage("./Index");
        }
    }
}
