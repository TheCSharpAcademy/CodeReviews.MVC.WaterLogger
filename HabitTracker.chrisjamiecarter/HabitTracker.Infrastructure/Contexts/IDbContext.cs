namespace HabitTracker.Infrastructure.Contexts;

/// <summary>
/// Contract for the database context.
/// </summary>
internal interface IDbContext
{
    string ConnectionString { get; }
    void EnsureCreated();
}