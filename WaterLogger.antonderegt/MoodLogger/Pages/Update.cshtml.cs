using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MoodLogger.Models;
using MoodLogger.Services;

namespace MoodLogger.Pages;

public class UpdateModel(IMoodDataService moodDataService) : PageModel
{
    private readonly IMoodDataService _moodDataService = moodDataService;
    [BindProperty]
    public Mood MoodRecord { get; set; } = new();

    public IActionResult OnGet(int id)
    {
        MoodRecord = GetById(id);
        return Page();
    }

    private Mood GetById(int id)
    {
        return _moodDataService.GetById(id);
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _moodDataService.Update(MoodRecord);

        return RedirectToPage("./Index");
    }
}