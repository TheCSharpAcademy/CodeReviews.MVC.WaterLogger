namespace HabitTracker.WebUI.Models;

/// <summary>
/// Specialised model for a request to create a Habit.
/// </summary>
public class CreateHabitRequest
{
    #region Properties

    public Guid Id { get; private set; } = Guid.NewGuid();

    public string Name { get; set; }

    public string Measure { get; set; }

    #endregion
}
