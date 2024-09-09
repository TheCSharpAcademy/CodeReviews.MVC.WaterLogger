namespace HabitTracker.WebUI.Models;

/// <summary>
/// Specialised model for a request to create a HabitLog.
/// </summary>
public class CreateHabitLogRequest
{
    #region Properties

    public Guid Id { get; private set; } = Guid.NewGuid();

    public Guid HabitId { get; set; }

    public DateTime Date { get; set; }

    public int Quantity { get; set; }

    #endregion
}
