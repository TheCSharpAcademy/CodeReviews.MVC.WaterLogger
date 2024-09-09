using HabitTracker.WebUI.Controllers;
using HabitTracker.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HabitTracker.WebUI.Pages;

/// <summary>
/// Page that creates a Habit from user input.
/// </summary>
public class CreateHabitModel : PageModel
{
    #region Fields

    private readonly IHabitController _habitController;

    #endregion
    #region Constructors

    public CreateHabitModel(IHabitController habitController)
    {
        _habitController = habitController;
    }

    #endregion
    #region Properties

    [BindProperty]
    public HabitDto Habit { get; set; }

    #endregion
    #region Methods

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

        var request = new CreateHabitRequest
        {
            Name = Habit.Name,
            Measure = Habit.Measure,
        };

        if (!_habitController.IsUniqueHabitName(request.Name))
        {
            return RedirectToPage("./Error", new { errorMessage = $"A habit with the name '{Habit.Name}' already exists in the database." });
        }

        var result = _habitController.AddHabit(request);
        if (result)
        {
            return RedirectToPage("./Index");
        }
        else
        {
            return RedirectToPage("./Error", new { errorMessage = "There was an error adding the habit to the database." });
        }
    }

    #endregion
}
