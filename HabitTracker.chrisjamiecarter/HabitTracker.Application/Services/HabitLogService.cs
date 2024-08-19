using HabitTracker.Application.Repositories;
using HabitTracker.Domain.Entities;

namespace HabitTracker.Application.Services;

/// <summary>
/// Implementation of the HabitLog service. Interacts with the HabitLog repository.
/// </summary>
public class HabitLogService : IHabitLogService
{
    #region Fields

    private readonly IHabitLogRepository _repository;
    
    #endregion
    #region Constructors

    public HabitLogService(IHabitLogRepository repository)
    {
        _repository = repository;
    }

    #endregion
    #region Methods

    public int AddHabitLog(HabitLog habitLog)
    {
        // If habit already has an entry for the date, then merge (increase the quantity).
        var existing = _repository.GetHabitLogByDate(habitLog.HabitId, habitLog.Date);
        if (existing is null)
        {
            return _repository.AddHabitLog(habitLog);

        }
        else
        {
            // Additional date instance. Merge.
            existing.Quantity += habitLog.Quantity;
            return _repository.UpdateHabitLog(existing);
        }
    }

    public int DeleteHabitLog(Guid id)
    {
        return _repository.DeleteHabitLog(id);
    }

    public HabitLog? GetHabitLog(Guid id)
    {
        return _repository.GetHabitLog(id);
    }

    public List<HabitLog> GetHabitLogs()
    {
        return _repository.GetHabitLogs();
    }

    public List<HabitLog> GetHabitLogs(Guid habitId)
    {
        return _repository.GetHabitLogs(habitId);
    }

    public List<HabitLog> GetHabitLogsByDateRange(DateTime from, DateTime to)
    {
        return _repository.GetHabitLogsByDateRange(from, to);
    }

    public List<HabitLog> GetHabitLogsByDateRange(Guid habitId, DateTime from, DateTime to)
    {
        return GetHabitLogsByDateRange(habitId, from, to);
    }

    public int UpdateHabitLog(HabitLog habitLog)
    {
        return _repository.UpdateHabitLog(habitLog);
    }

    #endregion
}
