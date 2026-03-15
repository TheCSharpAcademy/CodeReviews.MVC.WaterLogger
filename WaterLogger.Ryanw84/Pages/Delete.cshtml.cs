using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WaterLogger.Ryanw84.Data;
using WaterLogger.Ryanw84.Models;

namespace WaterLogger.Ryanw84.Pages
{
    public class DeleteModel(WaterLoggerContext db) : PageModel
    {
        private readonly WaterLoggerContext _db = db;

        [BindProperty]
        public DrinkingWaterModel? DrinkingWater { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            // FindAsync() translates to: SELECT * FROM drinking_water WHERE Id = @id

            // It also checks EF's local cache first before hitting the database.
            DrinkingWater = await _db.DrinkingWater.FindAsync(id);
            if (DrinkingWater is null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var record = await _db.DrinkingWater.FindAsync(id);
            if (record is not null)
            {
                // Remove() + SaveChangesAsync() translates to: DELETE FROM drinking_water WHERE Id = @id
                _db.DrinkingWater.Remove(record);
                await _db.SaveChangesAsync();
            }
            return RedirectToPage("./Index");
        }
    }
}
