using HabitTracker.Domain.Entities;

namespace HabitTracker.Application.Services;

/// <summary>
/// Contract for the Habit service.
/// </summary>
public interface IHabitService
{
    int AddHabit(Habit habit);
    Habit? GetHabit(Guid id);
    List<Habit> GetHabits();
    bool IsUniqueHabitName(string name);
    int UpdateHabit(Habit habit);
}
