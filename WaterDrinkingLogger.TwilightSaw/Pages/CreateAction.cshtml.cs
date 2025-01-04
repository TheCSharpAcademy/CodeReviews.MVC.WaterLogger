using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

namespace WaterDrinkingLogger.TwilightSaw.Pages;

public class CreateActionModel(IConfiguration configuration) : PageModel
{
    public IActionResult OnGet()
    {
        return Page();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid) return Page();

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        var tableCmd = connection.CreateCommand();
        tableCmd.CommandText = $@"
    INSERT INTO 'Actions' (name) 
  VALUES (@name);";

        tableCmd.Parameters.AddWithValue("@name", Name);
        tableCmd.ExecuteNonQuery();
        connection.Close();

        return RedirectToPage("./Index");
    }

    [BindProperty] public string Name { get; set; }
}