using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MoodLogger.Models;
using MoodLogger.Services;

namespace MoodLogger.Pages;

public class CreateModel(IMoodDataService moodDataService) : PageModel
{
    private readonly IMoodDataService _moodDataService = moodDataService;
    [BindProperty]
    public Mood MoodRecord { get; set; } = new() { Date = DateTime.Now };

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

        _moodDataService.AddMoodRecord(MoodRecord);

        return RedirectToPage("./Index");
    }
}