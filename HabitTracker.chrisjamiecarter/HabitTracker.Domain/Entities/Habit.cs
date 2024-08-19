namespace HabitTracker.Domain.Entities;

/// <summary>
/// The domain level Habit object.
/// </summary>
public class Habit
{
    #region Properties

    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Measure { get; set; }

    public bool IsActive { get; set; }

    #endregion
}
