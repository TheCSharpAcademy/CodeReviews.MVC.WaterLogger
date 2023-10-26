using CarSpottingTracker.mariusgrHiof.Data;
using CarSpottingTracker.mariusgrHiof.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace CarSpottingTracker.mariusgrHiof.Pages;

public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;
    [BindProperty]
    public CarSpottedModel CarSpotterModel { get; set; }

    public CreateModel(ApplicationDbContext context)
    {
        _context = context;
    }
    public IActionResult OnGet()
    {
        return Page();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            _context.CarSpotters.Add(CarSpotterModel);
            _context.SaveChanges();

            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {

            Console.WriteLine("Fail to add record.");
            Console.WriteLine(ex.Message);
            return Page();
        }
    }
}

