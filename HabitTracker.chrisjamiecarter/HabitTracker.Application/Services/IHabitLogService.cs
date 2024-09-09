using HabitTracker.Domain.Entities;

namespace HabitTracker.Application.Services;

/// <summary>
/// Contract for the HabitLog service.
/// </summary>
public interface IHabitLogService
{
    int AddHabitLog(HabitLog habitLog);
    int DeleteHabitLog(Guid id);
    HabitLog? GetHabitLog(Guid id);
    List<HabitLog> GetHabitLogs();
    List<HabitLog> GetHabitLogs(Guid habitId);
    List<HabitLog> GetHabitLogsByDateRange(DateTime from, DateTime to);
    List<HabitLog> GetHabitLogsByDateRange(Guid habitId, DateTime from, DateTime to);
    int UpdateHabitLog(HabitLog habitLog);
}