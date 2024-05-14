using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using System.Globalization;
using WaterDrinking.Models;

namespace WaterDrinking.Pages;

public class DeleteModel : PageModel
{
    private readonly IConfiguration _configuration;

    public DeleteModel(IConfiguration configuration)
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

    public IActionResult OnPost(int id)
    {
        using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
        {
            connection.Open();
            var tableCmd = connection.CreateCommand();

            tableCmd.CommandText = $"DELETE FROM drinking_water WHERE Id = {id}";
            tableCmd.ExecuteNonQuery();
        }
        return RedirectToPage("./Index");
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
}
