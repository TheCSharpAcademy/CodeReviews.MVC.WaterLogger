using HabitTracker.WebUI.Controllers;
using HabitTracker.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HabitTracker.WebUI.Pages;

/// <summary>
/// Page that displays a list of HabitLogs.
/// </summary>
public class ViewHabitLogsModel : PageModel
{
    #region Fields

    private readonly IHabitController _habitController;
    private readonly IHabitLogController _habitLogController;

    #endregion
    #region Constructors

    public ViewHabitLogsModel(IHabitController habitController, IHabitLogController habitLogController)
    {
        _habitController = habitController;
        _habitLogController = habitLogController;
    }

    #endregion
    #region Properties

    public string CurrentSort { get; set; }

    public string DateSort { get; set; }

    public string QuantitySort { get; set; }

    public HabitDto? Habit { get; set; }

    public IReadOnlyList<HabitLogDto> HabitLogs { get; set; } = [];

    #endregion
    #region Methods

    public IActionResult OnGet(Guid habitId, string sortOrder)
    {
        Habit = _habitController.GetHabit(habitId);
        if (Habit is null)
        {
            return RedirectToPage("./Error", new { errorMessage = $"No habit found with Id: {habitId}" });
        }

        HabitLogs = _habitLogController.GetHabitLogs(habitId);

        // Sort:
        CurrentSort = string.IsNullOrEmpty(sortOrder) ? "date_asc" : sortOrder;
        DateSort = string.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
        QuantitySort = sortOrder == "quantity_asc" ? "quantity_desc" : "quantity_asc";

        HabitLogs = sortOrder switch
        {
            "date_desc" => HabitLogs.OrderByDescending(h => h.Date).ToList(),
            "quantity_asc" => HabitLogs.OrderBy(h => h.Quantity).ToList(),
            "quantity_desc" => HabitLogs.OrderByDescending(h => h.Quantity).ToList(),
            _ => HabitLogs.OrderBy(h => h.Date).ToList(),
        };

        ViewData["HabitLogsCount"] = HabitLogs.Count;

        return Page();
    }

    #endregion
}
