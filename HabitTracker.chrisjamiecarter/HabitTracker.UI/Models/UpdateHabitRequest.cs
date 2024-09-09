namespace HabitTracker.WebUI.Models;

/// <summary>
/// Specialised model for a request to update a Habit.
/// </summary>
public class UpdateHabitRequest
{
    #region Properties

    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Measure { get; set; }

    public bool IsActive { get; set; }

    #endregion
}
