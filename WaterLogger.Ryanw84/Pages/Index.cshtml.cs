using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WaterLogger.Ryanw84.Data;
using WaterLogger.Ryanw84.Models;

namespace WaterLogger.Ryanw84.Pages;

public class IndexModel(WaterLoggerContext db) : PageModel
{
    private readonly WaterLoggerContext _db = db;
    public List<DrinkingWaterModel>? Records { get; set; }

    public void OnGet()
    {
        Records = _db.DrinkingWater.ToList();
        ViewData["Total"] = Records.Sum(x => x.Quantity);
    }

    public IActionResult OnGetExport()
    {
        var records = _db.DrinkingWater.ToList();

        using var workbook = new XLWorkbook();
        var sheet = workbook.Worksheets.Add("Water Log");

        sheet.Cell(1, 1).Value = "Date";
        sheet.Cell(1, 2).Value = "Quantity";
        sheet.Cell(1, 3).Value = "Measure";

        var headerRow = sheet.Row(1);
        headerRow.Style.Font.Bold = true;

        for (int i = 0; i < records.Count; i++)
        {
            sheet.Cell(i + 2, 1).Value = records[i].Date.ToString("dd-MM-yyyy");
            sheet.Cell(i + 2, 2).Value = records[i].Quantity;
            sheet.Cell(i + 2, 3).Value = records[i].Measure;
        }

        sheet.Columns().AdjustToContents();

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        stream.Position = 0;

        return File(
            stream.ToArray(),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            "WaterLog.xlsx"
        );
    }
}
