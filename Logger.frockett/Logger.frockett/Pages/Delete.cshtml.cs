using Logger.frockett.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using System.Globalization;

namespace Logger.frockett.Pages;

public class DeleteModel : PageModel
{
    private readonly IConfiguration config;

    [BindProperty]
    public DrinkingWaterModel DrinkingWater { get; set; }

    public DeleteModel(IConfiguration config)
    {
        this.config = config;
    }

    public IActionResult OnGet(int id)
    {
        DrinkingWater = GetById(id);
        return Page();
    }

    private DrinkingWaterModel GetById(int id)
    {
        var drinkingWaterRecord = new DrinkingWaterModel();

        using (var connection = new SqliteConnection(config.GetConnectionString("ConnectionString")))
        {
            connection.Open();
            var cmd = connection.CreateCommand();
            cmd.CommandText = $"SELECT * FROM drinking_water WHERE Id = {id}";

            SqliteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                drinkingWaterRecord.Id = reader.GetInt32(0);
                drinkingWaterRecord.Date = DateTime.Parse(reader.GetString(1), CultureInfo.CurrentUICulture.DateTimeFormat);
                drinkingWaterRecord.Quantity = reader.GetInt32(2);
            }
            connection.Close();
            return drinkingWaterRecord;
        }
    }

    public IActionResult OnPost(int id)
    {
        using (var connection = new SqliteConnection(config.GetConnectionString("ConnectionString")))
        {
            connection.Open();
            var cmd = connection.CreateCommand();
            cmd.CommandText = $"DELETE from drinking_water WHERE Id = {id}";
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        return RedirectToPage("./Index");
    }


}
