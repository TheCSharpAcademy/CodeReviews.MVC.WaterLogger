using System.Globalization;
using DrinksLogger.wkktoria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

namespace DrinksLogger.wkktoria.Pages;

public class IndexModel : PageModel
{
    private readonly IConfiguration _configuration;

    public IndexModel(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public List<Drink> Drinks { get; set; } = null!;

    public IActionResult OnGet()
    {
        Drinks = GetAllDrinks();

        return Page();
    }

    private List<Drink> GetAllDrinks()
    {
        using var connection = new SqliteConnection(_configuration.GetConnectionString("DefaultConnection"));
        var drinks = new List<Drink>();

        try
        {
            connection.Open();

            var command = connection.CreateCommand();

            command.CommandText =
                "SELECT * FROM drinks";

            var reader = command.ExecuteReader();

            while (reader.Read())
                drinks.Add(
                    new Drink
                    {
                        Id = reader.GetInt32(0),
                        Date = DateTime.Parse(reader.GetString(1),
                            CultureInfo.CurrentUICulture.DateTimeFormat),
                        Type = reader.GetString(2),
                        Measurement = reader.GetString(3),
                        Quantity = reader.GetDouble(4)
                    });
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            connection.Close();
        }

        return drinks;
    }
}