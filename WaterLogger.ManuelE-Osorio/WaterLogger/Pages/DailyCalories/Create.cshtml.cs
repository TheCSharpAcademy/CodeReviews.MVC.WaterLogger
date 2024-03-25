using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WaterLogger.Models;
using WaterLogger.Data;

namespace WaterLogger.Pages.DailyCalories;
public class CreateModel(DrinkingWaterContext context) : PageModel
{
    private readonly DrinkingWaterContext _context = context;

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public DailyCaloriesModel DailyCalories { get; set; } = default!;

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        _context.DailyCalories.Add(DailyCalories);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}