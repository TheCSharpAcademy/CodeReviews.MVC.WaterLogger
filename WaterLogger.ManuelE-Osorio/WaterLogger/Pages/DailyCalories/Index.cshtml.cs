using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WaterLogger.Models;
using WaterLogger.Data;
using System.Data;

namespace WaterLogger.Pages.DailyCalories;

public class IndexModel(DrinkingWaterContext context) : PageModel
{
    private readonly DrinkingWaterContext _context = context;

    [BindProperty(SupportsGet = true)]
    public int? IdSearchValue { get; set; }

    public List<DailyCaloriesModel> DailyCalories { get;set; } = default!;

    public async Task OnGetAsync()
    {
        var dailyCalories = from m in _context.DailyCalories
            select m;

        if (IdSearchValue is not null)
            dailyCalories = dailyCalories.Where(s => s.Id == IdSearchValue);

        DailyCalories = await dailyCalories.ToListAsync();    
        ViewData["Total"] = DailyCalories.Sum(p => p.Quantity);
    }
}

