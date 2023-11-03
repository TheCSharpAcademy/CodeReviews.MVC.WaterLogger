using CarSpottingTracker.mariusgrHiof.Data;
using CarSpottingTracker.mariusgrHiof.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarSpottingTracker.mariusgrHiof.Pages;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;
    public List<CarSpottedModel> CarsSpotted { get; set; }

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        CarsSpotted = GetCars();
        return Page();
    }

    private List<CarSpottedModel> GetCars()
    {
        return _context.CarSpotters.ToList();
    }
}
