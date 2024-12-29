using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

namespace WaterDrinkingLogger.TwilightSaw.Pages;

public class CreateTableModel(IConfiguration configuration) : PageModel
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
    CREATE TABLE [{Table}] (
        Id INTEGER PRIMARY KEY AUTOINCREMENT,
        date TEXT,
        quantity DOUBLE,
        measurement TEXT
    );
";

        tableCmd.Parameters.AddWithValue("@text", Table);
        tableCmd.ExecuteNonQuery();
        connection.Close();

        return RedirectToPage("./Index");
    }

    [BindProperty] public string Table { get; set; }
}