using CarSpottingTracker.mariusgrHiof.Data;
using CarSpottingTracker.mariusgrHiof.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarSpottingTracker.mariusgrHiof.Pages;

public class UpdateModel : PageModel
{
    private readonly ApplicationDbContext _context;
    [BindProperty]
    public CarSpottedModel CarSpotterModel { get; set; }
    public UpdateModel(ApplicationDbContext context)
    {
        _context = context;
    }
    public IActionResult OnGet(int id)
    {
        CarSpotterModel = GetById(id);

        return Page();
    }

    private CarSpottedModel GetById(int id)
    {
        var carSpott = _context.CarSpotters.FirstOrDefault(cs => cs.Id == id);

        return carSpott;
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            _context.CarSpotters.Update(CarSpotterModel);
            _context.SaveChanges();

            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Fail to update record.");
            Console.WriteLine(ex.Message);

            return Page();
        }
    }
}

