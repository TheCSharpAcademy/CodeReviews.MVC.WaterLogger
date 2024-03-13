using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WaterLogger.Models;
using WaterLogger.Data;

namespace WaterLogger.Pages;
public class DeleteModel(DrinkingWaterContext context) : PageModel
{
    private readonly DrinkingWaterContext _context = context;

    [BindProperty]
    public DrinkingWater DrinkingWater { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var drinkingwater = await _context.DrinkingWater.FirstOrDefaultAsync(m => m.Id == id);

        if (drinkingwater == null)
        {
            return NotFound();
        }
        else
        {
            DrinkingWater = drinkingwater;
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var drinkingwater = await _context.DrinkingWater.FindAsync(id);
        if (drinkingwater != null)
        {
            DrinkingWater = drinkingwater;
            _context.DrinkingWater.Remove(DrinkingWater);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
