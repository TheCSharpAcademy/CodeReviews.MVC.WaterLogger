using System.ComponentModel.DataAnnotations;
using HabitTracker.Domain.Entities;

namespace HabitTracker.WebUI.Models;

/// <summary>
/// DTO version of the Habit object.
/// </summary>
public class HabitDto
{
    #region Constructors

    public HabitDto()
    {
    }

    public HabitDto(Habit model)
    {
        Id = model.Id;
        Name = model.Name;
        Measure = model.Measure;
        IsActive = model.IsActive;
    }

    #endregion
    #region Properties

    public Guid Id { get; init; }

    public string Name { get; init; }

    public string Measure { get; init; }

    [Display(Name = "Active")]
    public bool IsActive { get; init; }

    #endregion
}
