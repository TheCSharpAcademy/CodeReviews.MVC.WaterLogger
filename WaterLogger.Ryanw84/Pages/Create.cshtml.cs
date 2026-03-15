using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WaterLogger.Ryanw84.Data;
using WaterLogger.Ryanw84.Models;

namespace WaterLogger.Ryanw84.Pages
{
    public class Create(WaterLoggerContext db) : PageModel
    {
        private readonly WaterLoggerContext _db = db;

        [BindProperty]
        public DrinkingWaterModel? DrinkingWater { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || DrinkingWater is null)
            {
                return Page();
            }

            // Add() stages the new record in EF's change tracker.
            // SaveChangesAsync() translates that to: INSERT INTO drinking_water (...) VALUES (...)
            _db.DrinkingWater.Add(DrinkingWater);
            await _db.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
