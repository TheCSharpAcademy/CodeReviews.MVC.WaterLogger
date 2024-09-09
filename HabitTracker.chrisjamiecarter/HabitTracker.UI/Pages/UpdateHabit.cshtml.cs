using HabitTracker.WebUI.Controllers;
using HabitTracker.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HabitTracker.WebUI.Pages;

/// <summary>
/// Page that updates a Habit from user input.
/// </summary>
public class UpdateHabitModel : PageModel
{
    #region Fields

    private readonly IHabitController _habitController;

    #endregion
    #region Constructors

    public UpdateHabitModel(IHabitController habitController)
    {
        _habitController = habitController;
    }

    #endregion
    #region Properties

    [BindProperty]
    public HabitDto? Habit { get; set; }
    
    #endregion
    #region Methods

    public IActionResult OnGet(Guid habitId)
    {
        Habit = _habitController.GetHabit(habitId);

        if (Habit == null)
        {
            return RedirectToPage("./Error", new { errorMessage = $"No habit found with Id: {habitId}" });
        }

        return Page();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var request = new UpdateHabitRequest
        {
            Id = Habit!.Id,
            Name = Habit.Name,
            Measure = Habit.Measure,
            IsActive = Habit.IsActive,
        };

        var result = _habitController.UpdateHabit(request);
        if (result)
        {
            return RedirectToPage("./Index");
        }
        else
        {
            return RedirectToPage("./Error", new { errorMessage = "There was an error updating the habit in the database." });
        }
    }

    #endregion
}
