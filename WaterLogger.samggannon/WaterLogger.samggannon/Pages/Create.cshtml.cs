using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WaterLogger.samggannon.Data;
using WaterLogger.samggannon.Models;

namespace WaterLogger.samggannon.Pages;

public class CreateModel : PageModel
{
    private readonly IConfiguration _configuration;
    private readonly DataAccess _dataFunctions;

    public CreateModel(IConfiguration configuration)
    {
        _configuration = configuration;
        _dataFunctions = new DataAccess(_configuration.GetConnectionString("ConnectionString"));
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
