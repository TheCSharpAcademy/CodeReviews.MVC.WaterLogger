using System.Globalization;
using FitnessTracker.StevieTV.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

namespace FitnessTracker.StevieTV.Pages;

public class Edit : PageModel
{
    private readonly IConfiguration _configuration;

    public Edit(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [BindProperty]
    public FitnessItem FitnessItem { get; set; }
    
    public IActionResult OnGet(int id)
    {
        FitnessItem = GetById(id);

        return Page();
    }

    private FitnessItem GetById(int id)
    {
        var fitnessItemRecord = new FitnessItem();
        
        using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
        {
            connection.Open();
            var command = connection.CreateCommand();

            command.CommandText =
                $"SELECT * FROM fitness_tracker WHERE Id = {id}";

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                fitnessItemRecord.Id = reader.GetInt32(0);
                fitnessItemRecord.Date = DateTime.Parse(reader.GetString(1), CultureInfo.CurrentUICulture);
                fitnessItemRecord.Type = reader.GetString(2);
                fitnessItemRecord.Duration = TimeSpan.Parse(reader.GetString(3), CultureInfo.CurrentUICulture);
            }
            connection.Close();

            return fitnessItemRecord;
        }
    }

    public IActionResult OnPost(int id)
    {
        if (!ModelState.IsValid) return Page();
        
        using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
        {
            connection.Open();
            var command = connection.CreateCommand();

            command.CommandText =
                $"UPDATE fitness_tracker SET Date = '{FitnessItem.Date}', Type = '{FitnessItem.Type}', Duration = '{FitnessItem.Duration}' WHERE Id = {FitnessItem.Id}";

            command.ExecuteNonQuery();
            connection.Close();

            return RedirectToPage("./Index");
        }
    }
}