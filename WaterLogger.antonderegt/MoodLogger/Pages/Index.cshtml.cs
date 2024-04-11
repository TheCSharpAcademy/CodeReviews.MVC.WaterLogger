using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MoodLogger.Models;
using MoodLogger.Services;

namespace MoodLogger.Pages;

public class IndexModel(IMoodDataService moodDataService) : PageModel
{
    private readonly IMoodDataService _moodDataService = moodDataService;
    [BindProperty]
    public List<Mood> Records { get; set; } = [];

    public void OnGet()
    {
        Records = GetAllRecords();
        if (Records == null || Records.Count == 0)
        {
            ViewData["Average"] = 0;
            return;
        }

        ViewData["Average"] = $"{Records.Average(record => record.MoodLevel):N2}";
    }

    private List<Mood> GetAllRecords()
    {
        return _moodDataService.GetAllRecords();
    }
}
