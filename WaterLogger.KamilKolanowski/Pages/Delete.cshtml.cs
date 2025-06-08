using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using WaterLogger.KamilKolanowski.Models;

namespace WaterLogger.KamilKolanowski.Pages;

public class DeleteModel : PageModel
{
    private readonly IConfiguration _configuration;
    [BindProperty]
    public DrinkingWaterModel DrinkingWater { get; set; }
    
    public DeleteModel(IConfiguration configuration)
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
            SqliteDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                drinkingWaterRecord.Id = reader.GetInt32(0);
                drinkingWaterRecord.Date = DateTime.Parse(reader.GetString(1), CultureInfo.CurrentUICulture.DateTimeFormat);
                drinkingWaterRecord.Quantity = reader.GetInt32(2);
            }
            
            connection.Close();
        }

        return drinkingWaterRecord;
    }

    public IActionResult OnPost(int id)
    {
        using (var connection = new SqliteConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM drinking_water WHERE Id = @id";
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
            connection.Close();
        }
        
        return RedirectToPage("./Index");
    }
}