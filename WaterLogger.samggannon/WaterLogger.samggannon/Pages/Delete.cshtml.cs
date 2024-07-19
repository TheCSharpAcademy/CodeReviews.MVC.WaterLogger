using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WaterLogger.samggannon.Data;
using WaterLogger.samggannon.Models;
using WaterLogger.samggannon.Services;

namespace WaterLogger.samggannon.Pages;

public class DeleteModel : PageModel
{
    private readonly DataAccess _dataFunctions;

    [BindProperty]
    public DrinkingWaterModel drinkingWater { get; set; }

    public DeleteModel(DataAccess dataFunctions)
    {
        _dataFunctions = dataFunctions;
    }

    public IActionResult OnGet(int id)
    {
        drinkingWater = GetById(id);

        if (drinkingWater == null)
        {
            return NotFound();
        }

        return Page();
    }

    public IActionResult OnPost(int id)
    {
        _dataFunctions.DeleteDrinkingWaterRecordById(id);

        return RedirectToPage("./Index");
    }

    private DrinkingWaterModel GetById(int id)
    {
        DrinkingWaterDto tempData = _dataFunctions.GetDrinkingWaterRecordById(id);

        DrinkingWaterModel retData = DtoMapper.MapToModel(tempData);

        return retData;
    }
}
