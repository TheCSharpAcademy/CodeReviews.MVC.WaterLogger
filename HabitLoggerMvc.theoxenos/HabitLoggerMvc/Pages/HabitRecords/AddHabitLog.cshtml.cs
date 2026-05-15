using HabitLoggerMvc.Helpers;
using HabitLoggerMvc.Models;
using HabitLoggerMvc.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

namespace HabitLoggerMvc.Pages.HabitRecords;

public class AddHabitLog(IHabitLogRepository habitLogRepository, IRepository<Habit> habitRepository) : PageModel
{
    [BindProperty] public HabitLog HabitLog { get; set; } = new();
    public Habit HabitModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            HabitModel = await habitRepository.GetByIdAsync(id);
            HabitLog.HabitId = id;
            return Page();
        }
        catch (KeyNotFoundException)
        {
            TempData["ErrorMessage"] = $"Habit with Id {id} not found";
            return RedirectToPage("/Index");
        }
        catch (SqliteException exception)
        {
            TempData["ErrorMessage"] = exception.BuildUserErrorMessage();
            return RedirectToPage("/Index");
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            HabitModel = await habitRepository.GetByIdAsync(HabitLog.HabitId);
            return Page();
        }

        try
        {
            await habitLogRepository.AddAsync(HabitLog);
            return RedirectToPage("/DetailHabit", new { id = HabitLog.HabitId });
        }
        catch (SqliteException exception)
        {
            TempData["ErrorMessage"] = exception.BuildUserErrorMessage();
            HabitModel = await habitRepository.GetByIdAsync(HabitLog.HabitId);
            return Page();
        }
    }
}
