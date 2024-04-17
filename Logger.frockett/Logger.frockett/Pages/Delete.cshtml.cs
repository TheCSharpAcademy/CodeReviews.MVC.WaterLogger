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
    public DailyPushupsModel pushups { get; set; }

    public DeleteModel(IConfiguration config)
    {
        this.config = config;
    }

    public IActionResult OnGet(int id)
    {
        pushups = GetById(id);
        return Page();
    }

    private DailyPushupsModel GetById(int id)
    {
        var pushups = new DailyPushupsModel();

        using (var connection = new SqliteConnection(config.GetConnectionString("ConnectionString")))
        {
            connection.Open();
            var cmd = connection.CreateCommand();
            cmd.CommandText = $"SELECT * FROM pushups WHERE Id = {id}";

            SqliteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                pushups.Id = reader.GetInt32(0);
                pushups.Date = DateTime.Parse(reader.GetString(1), CultureInfo.CurrentUICulture.DateTimeFormat);
                pushups.Quantity = reader.GetInt32(2);
            }
            connection.Close();
            return pushups;
        }
    }

    public IActionResult OnPost(int id)
    {
        using (var connection = new SqliteConnection(config.GetConnectionString("ConnectionString")))
        {
            connection.Open();
            var cmd = connection.CreateCommand();
            cmd.CommandText = $"DELETE from pushups WHERE Id = {id}";
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        return RedirectToPage("./Index");
    }


}
