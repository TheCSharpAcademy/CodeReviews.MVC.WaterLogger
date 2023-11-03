using DrinksLogger.wkktoria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

namespace DrinksLogger.wkktoria.Pages;

public class Add : PageModel
{
    private readonly IConfiguration _configuration;

    public Add(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [BindProperty] public Drink Drink { get; set; } = null!;

    public IActionResult OnGet()
    {
        return Page();
    }

    public IActionResult OnPost()
    {
        using var connection = new SqliteConnection(_configuration.GetConnectionString("DefaultConnection"));

        try
        {
            connection.Open();

            var command = connection.CreateCommand();

            command.CommandText =
                "INSERT INTO drinks(Date, Type, Measurement, Quantity) VALUES(@date, @type, @measurement, @quantity)";
            command.Parameters.AddWithValue("@date", Drink.Date);
            command.Parameters.AddWithValue("@type", Drink.Type);
            command.Parameters.AddWithValue("@measurement", Drink.Measurement);
            command.Parameters.AddWithValue("@quantity", Drink.Quantity);

            command.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            connection.Close();
        }

        return RedirectToPage("/Index");
    }
}