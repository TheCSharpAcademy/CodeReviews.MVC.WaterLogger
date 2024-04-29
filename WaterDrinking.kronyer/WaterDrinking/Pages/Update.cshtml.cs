using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using System.Globalization;
using WaterDrinking.Models;

namespace WaterDrinking.Pages;

public class UpdateModel : PageModel
{
    private readonly IConfiguration _configuration;

    public UpdateModel(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [BindProperty]
    public DrinkingWaterModel DrinkingWater { get; set; }

    public IActionResult OnGet(int id)
    {
        DrinkingWater = GetById(id);
        return Page();
    }

    private DrinkingWaterModel GetById(object id)
    {
        var drinkingWaterRecord = new DrinkingWaterModel();
        using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
        {
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = $"SELECT * FROM drinking_water WHERE Id = {id}";

            SqliteDataReader dr = tableCmd.ExecuteReader();

            while (dr.Read())
            {
                drinkingWaterRecord.Id = dr.GetInt32(0);
                drinkingWaterRecord.Date = DateTime.Parse(dr.GetString(1), CultureInfo.CurrentUICulture.DateTimeFormat);
                drinkingWaterRecord.Quantity = dr.GetInt32(2);
            }
            return drinkingWaterRecord;
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

            tableCmd.CommandText = $"UPDATE drinking_water SET date = '{DrinkingWater.Date}', quantity = {DrinkingWater.Quantity} WHERE Id = {DrinkingWater.Id}";

            tableCmd.ExecuteNonQuery();
        }
        return RedirectToPage("./Index");
    }
}
