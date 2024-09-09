namespace HabitTracker.WebUI.Models;

/// <summary>
/// Specialised model for a request to update a HabitLog.
/// </summary>
public class UpdateHabitLogRequest
{
    #region Properties

    public Guid Id { get; set; }

    public DateTime Date { get; set; }

    public int Quantity { get; set; }

    #endregion
}
