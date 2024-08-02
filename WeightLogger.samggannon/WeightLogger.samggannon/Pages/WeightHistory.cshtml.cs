using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeightLogger.samggannon.Data;
using WeightLogger.samggannon.Models;

namespace WeightLogger.samggannon.Pages;

public class WeightHistoryModel : PageModel
{
    [BindProperty]
    public Weight logWeight { get; set; }

    [BindProperty]
    public bool needsToConvert { get; set; }

    // no bind
    public List<Weight> WeightLogs = new List<Weight>();

    //db accessor
    private readonly DataAccess _dataFunctions;

    public WeightHistoryModel(DataAccess dataFunctions)
    {
        _dataFunctions = dataFunctions;
    }
    
    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if(!ModelState.IsValid)
        {
            return Page();
        }

        if (needsToConvert)
        {
            logWeight.weight = ConvertWeightToKg(logWeight.weight);
            _dataFunctions.LogThisWeight(logWeight.weight, logWeight.loggedDate.ToString());

            return RedirectToPage("./Index");
        }
        else
        {
            _dataFunctions.LogThisWeight(logWeight.weight, logWeight.loggedDate.ToString());
            return RedirectToPage("./Index");
        }
    }

    public decimal ConvertWeightToKg(decimal weightInPounds)
    {
        decimal kg = weightInPounds / 2.20462m;
        return kg;
    }
}
