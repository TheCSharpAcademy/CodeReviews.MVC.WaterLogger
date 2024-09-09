using HabitTracker.WebUI.Controllers;
using HabitTracker.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HabitTracker.WebUI.Pages;

/// <summary>
/// Page that updates a HabitLog from user input.
/// </summary>
public class UpdateHabitLogModel : PageModel
{
    #region Fields

    private readonly IHabitController _habitController;
    private readonly IHabitLogController _habitLogController;

    #endregion
    #region Constructors

    public UpdateHabitLogModel(IHabitController habitController, IHabitLogController habitLogController)
    {
        _habitController = habitController;
        _habitLogController = habitLogController;
    }

    #endregion
    #region Properties

    public HabitDto? Habit { get; set; }

    [BindProperty]
    public HabitLogDto? HabitLog { get; set; }

    #endregion
    #region Methods

    public IActionResult OnGet(Guid habitLogId)
    {
        HabitLog = _habitLogController.GetHabitLog(habitLogId);
        if (HabitLog == null)
        {
            return RedirectToPage("./Error", new { errorMessage = $"No habit log found with Id: {habitLogId}" });
        }

        Habit = _habitController.GetHabit(HabitLog.HabitId);
        if (Habit is null)
        {
            return RedirectToPage("./Error", new { errorMessage = $"No habit found with Id: {HabitLog.HabitId}" });
        }

        return Page();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var request = new UpdateHabitLogRequest
        {
            Id = HabitLog!.Id,
            Date = HabitLog.Date,
            Quantity = HabitLog.Quantity,
        };

        var result = _habitLogController.UpdateHabitLog(request);
        if (result)
        {
            return RedirectToPage("./ViewHabitLogs", new { habitId = HabitLog.HabitId });
        }
        else
        {
            return RedirectToPage("./Error", new { errorMessage = "There was an error updating the habit log in the database." });
        }
    }

    #endregion
}
