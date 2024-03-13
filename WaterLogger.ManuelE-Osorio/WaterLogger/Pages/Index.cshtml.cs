using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WaterLogger.Models;
using WaterLogger.Data;

namespace WaterLogger.Pages;

public class IndexModel(DrinkingWaterContext context) : PageModel
{
    private readonly DrinkingWaterContext _context = context;

    [BindProperty(SupportsGet = true)]
    public int? IdSearchValue { get; set; }

    public IList<DrinkingWater> DrinkingWater { get;set; } = default!;

    public async Task OnGetAsync()
    {
        var drinkingWater = from m in _context.DrinkingWater
            select m;
        if (IdSearchValue is not null)
        {
            drinkingWater = drinkingWater.Where(s => s.Id == IdSearchValue);
        }

        DrinkingWater = await drinkingWater.ToListAsync();
    }
}

