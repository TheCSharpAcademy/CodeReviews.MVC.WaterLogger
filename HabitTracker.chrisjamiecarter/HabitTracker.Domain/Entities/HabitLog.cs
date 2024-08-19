namespace HabitTracker.Domain.Entities;

/// <summary>
/// The domain level HabitLog object.
/// </summary>
public class HabitLog
{
    #region Properties

    public Guid Id { get; set; }

    public Guid HabitId { get; set; }

    public DateTime Date { get; set; }

    public int Quantity { get; set; }

    #endregion
}
