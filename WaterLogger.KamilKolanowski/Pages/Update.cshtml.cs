using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using WaterLogger.KamilKolanowski.Models;

namespace WaterLogger.KamilKolanowski.Pages;

public class UpdateModel : PageModel
{
    private readonly IConfiguration _configuration;

    [BindProperty]
    public DrinkingWaterModel DrinkingWater { get; set; }

    public UpdateModel(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IActionResult OnGet(int id)
    {
        DrinkingWater = GetById(id);
        return Page();
    }

    private DrinkingWaterModel GetById(int id)
    {
        var drinkingWaterRecord = new DrinkingWaterModel();

        using (var connection = new SqliteConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT Id, Date, Quantity FROM drinking_water WHERE Id = @id";
            command.Parameters.AddWithValue("@id", id);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                drinkingWaterRecord.Id = reader.GetInt32(0);
                drinkingWaterRecord.Date = DateTime.Parse(reader.GetString(1), CultureInfo.InvariantCulture);
                drinkingWaterRecord.Quantity = reader.GetInt32(2);
            }
        }

        return drinkingWaterRecord;
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        using (var connection = new SqliteConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "UPDATE drinking_water SET date = @date, quantity = @quantity WHERE Id = @id";
            command.Parameters.AddWithValue("@date", DrinkingWater.Date.ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@quantity", DrinkingWater.Quantity);
            command.Parameters.AddWithValue("@id", DrinkingWater.Id);
            command.ExecuteNonQuery();
        }

        return RedirectToPage("./Index");
    }
}
