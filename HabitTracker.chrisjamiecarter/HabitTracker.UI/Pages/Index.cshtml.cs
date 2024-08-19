using HabitTracker.WebUI.Controllers;
using HabitTracker.WebUI.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HabitTracker.WebUI.Pages;

/// <summary>
/// Home Page. Displays active habits.
/// </summary>
public class IndexModel : PageModel
{
    #region Fields

    private readonly IHabitController _habitController;

    #endregion
    #region Constructors

    public IndexModel(IHabitController habitController)
    {
        _habitController = habitController;
    }

    #endregion
    #region Properties

    public string SelectedFilter { get; set; }

    public string CurrentSort { get; set; }

    public string NameSort { get; set; }

    public string MeasureSort { get; set; }

    public string IsActiveSort { get; set; }

    public IReadOnlyList<HabitDto> Habits { get; set; } = [];

    public IReadOnlyList<HabitDto> FilteredHabits { get; set; } = [];

    #endregion
    #region Methods

    public void OnGet(string isActiveFilter, string sortOrder)
    {
        Habits = _habitController.GetHabits();

        // Filter:
        if (isActiveFilter is null)
        {
            isActiveFilter = "true";
        }

        SelectedFilter = isActiveFilter;

        if (isActiveFilter.Equals("all"))
        {
            FilteredHabits = Habits;
        }
        else
        {
            var isActive = bool.Parse(isActiveFilter);
            FilteredHabits = Habits.Where(h => h.IsActive == isActive).ToList();
        }

        // Sort:
        CurrentSort = string.IsNullOrEmpty(sortOrder) ? "name_asc" : sortOrder;
        NameSort = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        MeasureSort = sortOrder == "measure_asc" ? "measure_desc" : "measure_asc";
        IsActiveSort = sortOrder == "active_asc" ? "active_desc" : "active_asc";

        FilteredHabits = sortOrder switch
        {
            "name_desc" => FilteredHabits.OrderByDescending(h => h.Name).ToList(),
            "measure_asc" => FilteredHabits.OrderBy(h => h.Measure).ToList(),
            "measure_desc" => FilteredHabits.OrderByDescending(h => h.Measure).ToList(),
            "active_asc" => FilteredHabits.OrderBy(h => h.IsActive).ToList(),
            "active_desc" => FilteredHabits.OrderByDescending(h => h.IsActive).ToList(),
            _ => FilteredHabits.OrderBy(h => h.Name).ToList(),
        };

        ViewData["HabitsCount"] = Habits.Count;
    }

    #endregion
}
