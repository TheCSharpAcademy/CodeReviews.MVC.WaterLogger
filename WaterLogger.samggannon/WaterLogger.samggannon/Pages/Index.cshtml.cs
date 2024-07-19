using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using WaterLogger.samggannon.Data;
using WaterLogger.samggannon.Models;

namespace WaterLogger.samggannon.Pages;

public class IndexModel : PageModel
{
    private readonly DataAccess _dataFunctions;

    [BindProperty]
    public List<DrinkingWaterModel> Records { get; set; }

    public IndexModel(DataAccess dataFunctions)
    {
        _dataFunctions = dataFunctions;
    }

    public void OnGet()
    {
        Records = GetAllRecords();
    }

    private List<DrinkingWaterModel> GetAllRecords()
    {
        List<DrinkingWaterModel> retData = new List<DrinkingWaterModel>();
        DataTable dtDrinkingWaterData = new DataTable();
        dtDrinkingWaterData = _dataFunctions.GetAllDrinkingWaterRecords();
        foreach(DataRow dr in dtDrinkingWaterData.Rows)
        {
            DrinkingWaterModel drinkingWaterData = new DrinkingWaterModel();
            drinkingWaterData.Id = Convert.ToInt32(dr["Id"]);
            drinkingWaterData.Date = Convert.ToDateTime(dr["Date"]);
            drinkingWaterData.Quantity = Convert.ToInt32(dr["Quantity"]);

            retData.Add(drinkingWaterData);
        }

        return retData;
    }
}
