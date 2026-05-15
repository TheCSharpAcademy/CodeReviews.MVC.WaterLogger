using HabitLoggerMvc.Helpers;
using HabitLoggerMvc.Models;
using HabitLoggerMvc.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

namespace HabitLoggerMvc.Pages.HabitRecords;

public class DeleteHabitLog(IHabitLogRepository habitLogRepository, IRepository<Habit> habitRepository) : PageModel
{
    [BindProperty] public HabitLog HabitLog { get; set; }
    public Habit HabitModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            HabitLog = await habitLogRepository.GetByIdAsync(id);
            HabitModel = await habitRepository.GetByIdAsync(HabitLog.HabitId);
            return Page();
        }
        catch (KeyNotFoundException)
        {
            TempData["ErrorMessage"] = $"Habit log with Id {id} not found";
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
        try
        {
            var logToDelete = await habitLogRepository.GetByIdAsync(HabitLog.Id);
            int habitId = logToDelete.HabitId;
            await habitLogRepository.DeleteAsync(HabitLog.Id);
            return RedirectToPage("/DetailHabit", new { id = habitId });
        }
        catch (SqliteException exception)
        {
            TempData["ErrorMessage"] = exception.BuildUserErrorMessage();
            return RedirectToPage("/Index");
        }
    }
}
