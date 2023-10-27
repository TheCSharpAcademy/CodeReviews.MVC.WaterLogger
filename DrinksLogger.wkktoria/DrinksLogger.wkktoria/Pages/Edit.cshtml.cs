using System.Globalization;
using DrinksLogger.wkktoria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

namespace DrinksLogger.wkktoria.Pages;

public class Edit : PageModel
{
    private readonly IConfiguration _configuration;

    public Edit(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [BindProperty] public Drink Drink { get; set; } = null!;

    public IActionResult OnGet(int id)
    {
        Drink = GetDrinkById(id);

        return Page();
    }

    public IActionResult OnPost(int id)
    {
        if (!ModelState.IsValid) return Page();

        using var connection = new SqliteConnection(_configuration.GetConnectionString("DefaultConnection"));

        try
        {
            connection.Open();

            var command = connection.CreateCommand();

            command.CommandText =
                "UPDATE drinks SET Date = @date, Type = @type, Measurement = @measurement, Quantity = @quantity WHERE Id = @id";
            command.Parameters.AddWithValue("@date", Drink.Date);
            command.Parameters.AddWithValue("@type", Drink.Type);
            command.Parameters.AddWithValue("@measurement", Drink.Measurement);
            command.Parameters.AddWithValue("@quantity", Drink.Quantity);
            command.Parameters.AddWithValue("@id", id);

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

    private Drink GetDrinkById(int id)
    {
        using var connection = new SqliteConnection(_configuration.GetConnectionString("DefaultConnection"));
        var drink = new Drink();

        try
        {
            connection.Open();

            var command = connection.CreateCommand();

            command.CommandText =
                "SELECT * FROM drinks WHERE Id = @id";
            command.Parameters.AddWithValue("@id", id);

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                drink.Id = reader.GetInt32(0);
                drink.Date = DateTime.Parse(reader.GetString(1), CultureInfo.CurrentUICulture.DateTimeFormat);
                drink.Type = reader.GetString(2);
                drink.Measurement = reader.GetString(3);
                drink.Quantity = reader.GetDouble(4);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            connection.Close();
        }

        return drink;
    }
}