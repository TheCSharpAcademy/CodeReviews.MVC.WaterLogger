using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Water_Logger.Data;
using Water_Logger.Models;

namespace Water_Logger.Pages
{
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public required DrinkingWater LoggingData { get; set; }

        private Database _db;
        public DeleteModel(Database db)
        {
            _db = db;
        }
        public void OnGet(int Id)
        {
            LoggingData = _db.Get(Id);
        }

        public IActionResult OnPost()
        {
            _db.Delete(LoggingData.Id);

            TempData["success"] = "Log Deleted Successfully!!";

            return RedirectToPage("/Index");
        }
    }
}
