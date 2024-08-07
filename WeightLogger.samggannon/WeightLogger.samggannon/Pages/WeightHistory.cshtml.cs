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
            thisWeight.weightValue = Convert.ToDecimal(dr["weight"]);

            // Check and convert the log_date
            string logDateString = dr["log_date"].ToString();
            DateTime logDate;
            if (logDateString == "0" || string.IsNullOrWhiteSpace(logDateString) || !DateTime.TryParse(logDateString, out logDate))
            {
                logDate = DateTime.MinValue;
            }

            thisWeight.loggedDate = logDate;

            WeightLogs.Add(thisWeight);

            WeightLogs = WeightLogs.OrderBy(w => w.loggedDate).ToList();
        }

    }

    public IActionResult OnPost()
    {
        if(!ModelState.IsValid)
        {
            ModelState.AddModelError(string.Empty, "Unable to Create record");
            return Page();
        }

        _dataFunctions.LogThisWeight(logWeight.weightValue, logWeight.loggedDate.ToString());
        return RedirectToPage("./Index");
    }
}
