using HabitTracker.WebUI.Models;

namespace HabitTracker.WebUI.Controllers;

/// <summary>
/// Contract for the HabitLogController.
/// </summary>
public interface IHabitLogController
{
    bool AddHabitLog(CreateHabitLogRequest request);
    bool DeleteHabitLog(Guid id);
    HabitLogDto? GetHabitLog(Guid id);
    IReadOnlyList<HabitLogDto> GetHabitLogs();
    IReadOnlyList<HabitLogDto> GetHabitLogs(Guid habitId);
    IReadOnlyList<HabitLogDto> GetHabitLogsByDateRange(DateTime from, DateTime to);
    IReadOnlyList<HabitLogDto> GetHabitLogsByDateRange(Guid habitId, DateTime from, DateTime to);
    bool UpdateHabitLog(UpdateHabitLogRequest request);
}