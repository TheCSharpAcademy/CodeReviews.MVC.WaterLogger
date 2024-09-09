using HabitTracker.Application.Services;
using HabitTracker.Domain.Entities;
using HabitTracker.WebUI.Models;

namespace HabitTracker.WebUI.Controllers;

/// <summary>
/// Controls all Habit specific interactions from the WebUI to the other layers.
/// </summary>
public class HabitController : IHabitController
{
    #region Fields

    private readonly IHabitService _service;

    #endregion
    #region Constructors

    public HabitController(IHabitService service)
    {
        _service = service;
    }

    #endregion
    #region Methods

    public bool AddHabit(CreateHabitRequest request)
    {
        var habit = new Habit
        {
            Id = request.Id,
            Name = request.Name,
            Measure = request.Measure,
            IsActive = true,
        };

        var result = _service.AddHabit(habit);
        return result > 0;
    }

    public HabitDto? GetHabit(Guid id)
    {
        var habit = _service.GetHabit(id);
        return habit is null ? null : new HabitDto(habit);
    }

    public IReadOnlyList<HabitDto> GetHabits()
    {
        return _service.GetHabits()
            .Select(x => new HabitDto(x))
            .ToList();
    }

    public bool IsUniqueHabitName(string name)
    {
        return _service.IsUniqueHabitName(name);
    }

    public bool UpdateHabit(UpdateHabitRequest request)
    {
        var habit = _service.GetHabit(request.Id);
        if (habit is null)
        {
            return false;
        }

        habit.Name = request.Name;
        habit.Measure = request.Measure;
        habit.IsActive = request.IsActive;

        var result = _service.UpdateHabit(habit);
        return result > 0;
    }

    #endregion
}
