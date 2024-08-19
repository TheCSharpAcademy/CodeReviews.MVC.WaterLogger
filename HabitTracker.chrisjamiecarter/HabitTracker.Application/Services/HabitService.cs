using HabitTracker.Application.Repositories;
using HabitTracker.Domain.Entities;

namespace HabitTracker.Application.Services;

/// <summary>
/// Implementation of the Habit service. Interacts with the Habit repository.
/// </summary>
public class HabitService : IHabitService
{
    #region Fields
    
    private readonly IHabitRepository _repository;

    #endregion
    #region Constructors
    
    public HabitService(IHabitRepository repository)
    {
        _repository = repository;
    }

    #endregion
    #region Methods
    
    public int AddHabit(Habit habit)
    {
        return _repository.AddHabit(habit);
    }

    public Habit? GetHabit(Guid id)
    {
        return _repository.GetHabit(id);
    }

    public List<Habit> GetHabits()
    {
        return _repository.GetHabits();
    }

    public bool IsUniqueHabitName(string name)
    {
        return _repository.GetHabit(name) is null;
    }

    public int UpdateHabit(Habit habit)
    {
        return _repository.UpdateHabit(habit);
    }

    #endregion
}
