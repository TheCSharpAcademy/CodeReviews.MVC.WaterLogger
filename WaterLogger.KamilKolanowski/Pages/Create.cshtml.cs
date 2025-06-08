using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using WaterLogger.KamilKolanowski.Models;

namespace WaterLogger.KamilKolanowski.Pages;

public class Create : PageModel
{
    private readonly IConfiguration _configuration;

    public Create(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public IActionResult OnGet()
    {
        return Page();
    }
    
    [BindProperty]
    public DrinkingWaterModel DrinkingWater { get; set; }

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
            command.CommandText = "INSERT INTO drinking_water (Date, Quantity) VALUES (@date, @quantity)";
            command.Parameters.AddWithValue("@date", DrinkingWater.Date);
            command.Parameters.AddWithValue("@quantity", DrinkingWater.Quantity);
            command.ExecuteNonQuery();
            connection.Close();
        }
        return RedirectToPage("./Index");
    }
}