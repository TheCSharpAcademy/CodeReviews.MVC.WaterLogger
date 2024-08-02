using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
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
        GetWeightHistoy();
    }

    private void GetWeightHistoy()
    {
        DataTable dtWeightHistory = _dataFunctions.GetWeightHistory();

        foreach (DataRow dr in dtWeightHistory.Rows)
        {
            Weight thisWeight = new Weight();
            thisWeight.Id = Convert.ToInt32(dr["log_id"]);
            thisWeight.weight = Convert.ToDecimal(dr["weight"]);

            // Check and convert the log_date
            string logDateString = dr["log_date"].ToString();
            DateTime logDate;
            if (logDateString == "0" || string.IsNullOrWhiteSpace(logDateString) || !DateTime.TryParse(logDateString, out logDate))
            {
                // Handle invalid date, set to a default value if needed
                logDate = DateTime.MinValue; // or any other default date
            }

            thisWeight.loggedDate = logDate;

            WeightLogs.Add(thisWeight);
        }

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
