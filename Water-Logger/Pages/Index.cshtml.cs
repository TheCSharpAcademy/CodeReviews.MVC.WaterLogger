using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Water_Logger.Data;
using Water_Logger.Models;

namespace Water_Logger.Pages;

public class IndexModel : PageModel
{
    [BindProperty]
    public List<DrinkingWater> DrinkWaterLogs { get; set;} = new();
    private Database _db;
    public IndexModel(Database db)
    {
        _db = db;
    }

    public void OnGet()
    {
        DrinkWaterLogs = _db.GetAll();
    }
}
