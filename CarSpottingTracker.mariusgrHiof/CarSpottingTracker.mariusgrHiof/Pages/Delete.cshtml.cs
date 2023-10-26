using CarSpottingTracker.mariusgrHiof.Data;
using CarSpottingTracker.mariusgrHiof.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarSpottingTracker.mariusgrHiof.Pages;

public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;
    [BindProperty]
    public CarSpottedModel CarSpottedModel { get; set; }
    public DeleteModel(ApplicationDbContext context)
    {
        _context = context;
    }
    public IActionResult OnGet(int id)
    {
        CarSpottedModel = GetById(id);

        return Page();
    }

    private CarSpottedModel GetById(int id)
    {
        var carSpotted = _context.CarSpotters.FirstOrDefault(cs => cs.Id == id);

        return carSpotted;
    }

    public IActionResult OnPost()
    {
        try
        {
            _context.CarSpotters.Remove(CarSpottedModel);
            _context.SaveChanges();

            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Fail to delete record.");
            Console.WriteLine(ex.Message);

            return Page();
        }
    }
}

