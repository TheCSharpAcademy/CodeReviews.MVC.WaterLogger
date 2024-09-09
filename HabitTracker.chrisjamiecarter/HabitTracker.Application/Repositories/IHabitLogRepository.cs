using HabitTracker.Domain.Entities;

namespace HabitTracker.Application.Repositories;

/// <summary>
/// Contract for the HabitLog repository.
/// </summary>
public interface IHabitLogRepository
{
    int AddHabitLog(HabitLog habitLog);
    int DeleteHabitLog(Guid id);
    HabitLog? GetHabitLog(Guid id);
    HabitLog? GetHabitLogByDate(Guid habitId, DateTime date);
    List<HabitLog> GetHabitLogs();
    List<HabitLog> GetHabitLogs(Guid habitId);
    List<HabitLog> GetHabitLogsByDateRange(DateTime from, DateTime to);
    List<HabitLog> GetHabitLogsByDateRange(Guid habitId, DateTime from, DateTime to);
    int UpdateHabitLog(HabitLog habitLog);
}
