using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WaterLogger.samggannon.Data;
using WaterLogger.samggannon.Models;

namespace WaterLogger.samggannon.Pages;

public class CreateModel : PageModel
{
    private readonly DataAccess _dataFunctions;

    public CreateModel(DataAccess dataFunctions)
    {
        _dataFunctions = dataFunctions;
    }

    [BindProperty]
    public DrinkingWaterModel DrinkingWater { get; set; }

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

        _dataFunctions.InsertDrinkingWaterRecord(DrinkingWater.Date.ToString(), DrinkingWater.Quantity);

        return RedirectToPage("./Index");
    }
}
