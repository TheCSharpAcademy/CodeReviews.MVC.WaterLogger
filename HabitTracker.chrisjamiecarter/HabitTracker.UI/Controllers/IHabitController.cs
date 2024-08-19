using HabitTracker.WebUI.Models;

namespace HabitTracker.WebUI.Controllers;

/// <summary>
/// Contract for the HabitController.
/// </summary>
public interface IHabitController
{
    bool AddHabit(CreateHabitRequest request);
    HabitDto? GetHabit(Guid id);
    IReadOnlyList<HabitDto> GetHabits();
    bool IsUniqueHabitName(string name);
    bool UpdateHabit(UpdateHabitRequest request);
}