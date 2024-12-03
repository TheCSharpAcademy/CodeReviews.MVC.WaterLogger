using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Water_Logger.Data;
using Water_Logger.Models;

namespace Water_Logger.Pages
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public required DrinkingWater LoggingData { get; set; }
        private Database _db;
        public EditModel(Database db)
        {
            _db = db;
        }
        public void OnGet(int id)
        {
            LoggingData = _db.Get(id);

        }

        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            _db.Update(LoggingData);

            TempData["success"] = "Log Edited Sucessfully!!";
            return RedirectToPage("/Index");
        }
    }
}
