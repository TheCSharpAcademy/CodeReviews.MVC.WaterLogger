using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WaterLogger.Models;
using WaterLogger.Data;

namespace WaterLogger.Pages;
public class EditModel(DrinkingWaterContext context) : PageModel
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

        var drinkingwater =  await _context.DrinkingWater.FirstOrDefaultAsync(m => m.Id == id);
        if (drinkingwater == null)
        {
            return NotFound();
        }
        DrinkingWater = drinkingwater;
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Attach(DrinkingWater).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!DrinkingWaterExists(DrinkingWater.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Index");
    }

    private bool DrinkingWaterExists(int id)
    {
        return _context.DrinkingWater.Any(e => e.Id == id);
    }
}

