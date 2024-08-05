using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using WaterLogger.kwm0304.Models;

namespace WaterLogger.kwm0304.Pages
{
    //the value attached to the submit btn in create corresponds to a method name here
    public class CreateModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public CreateModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult OnGet()
        {
            return Page();//just return page bc there's no state
        }

        [BindProperty]
        public DrinkingWaterModel DrinkingWater { get; set; }

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
                   @$"INSERT INTO drinking_water(date, quantity)
                      VALUES('{DrinkingWater.Date}', {DrinkingWater.Quantity})";

                tableCmd.ExecuteNonQuery();
            }
            return RedirectToPage("./Index");
        }
    }
}