using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WaterLogger.Ryanw84.Data;
using WaterLogger.Ryanw84.Models;

namespace WaterLogger.Ryanw84.Pages;

public class UpdateModel(WaterLoggerContext db) : PageModel
{
    private readonly WaterLoggerContext _db = db;

    [BindProperty]
    public DrinkingWaterModel DrinkingWater { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        // FindAsync() translates to: SELECT * FROM drinking_water WHERE Id = @id
        var record = await _db.DrinkingWater.FindAsync(id);
        if (record is null)
        {
            return NotFound();
        }
        DrinkingWater = record;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Update() tells EF's change tracker this entity has been modified.
        // SaveChangesAsync() translates that to: UPDATE drinking_water SET ... WHERE Id = @id
        _db.DrinkingWater.Update(DrinkingWater);
        await _db.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}

