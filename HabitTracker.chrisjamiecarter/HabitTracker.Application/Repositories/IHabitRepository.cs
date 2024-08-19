using HabitTracker.Domain.Entities;

namespace HabitTracker.Application.Repositories;

/// <summary>
/// Contract for the Habit repository.
/// </summary>
public interface IHabitRepository
{
    int AddHabit(Habit habit);
    Habit? GetHabit(Guid id);
    Habit? GetHabit(string name);
    List<Habit> GetHabits();
    List<Habit> GetHabitsByIsActive(bool isActive);
    int UpdateHabit(Habit habit);
}
