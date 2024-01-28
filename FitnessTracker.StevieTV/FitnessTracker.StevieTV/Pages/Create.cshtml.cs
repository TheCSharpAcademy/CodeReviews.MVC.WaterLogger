using System.Globalization;
using FitnessTracker.StevieTV.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

namespace FitnessTracker.StevieTV.Pages;

public class Create : PageModel
{
    private readonly IConfiguration _configuration;

    public Create(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [BindProperty]
    public FitnessItem FitnessItem { get; set; }
    
    public IActionResult OnGet()
    {
        return Page();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid) return Page();

        using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
        {
            connection.Open();
            var command = connection.CreateCommand();

            command.CommandText =
                $"INSERT INTO fitness_tracker(Date, Type, Duration) VALUES ('{FitnessItem.Date}', '{FitnessItem.Type}', '{FitnessItem.Duration}')";

            command.ExecuteNonQuery();
            connection.Close();

            return RedirectToPage("./Index");
        }
    }
}