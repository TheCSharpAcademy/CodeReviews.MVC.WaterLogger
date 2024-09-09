using HabitTracker.Infrastructure.Queries;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace HabitTracker.Infrastructure.Contexts;

/// <summary>
/// Ensures both the database and the schema is created. Then holds the connection string.
/// </summary>
internal class DbContext : IDbContext
{
    #region Constructors

    public DbContext(IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("Default");

        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString, nameof(connectionString));

        ConnectionString = connectionString;

        EnsureCreated();
    }

    #endregion
    #region Properties

    public string ConnectionString { get; }

    #endregion
    #region Methods

    public void EnsureCreated()
    {
        CreateHabitTable();
        CreateHabitLogTable();
    }

    private void CreateHabitTable()
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = TableQueries.CreateHabitTable;
        command.ExecuteNonQuery();
    }

    private void CreateHabitLogTable()
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = TableQueries.CreateHabitLogTable;
        command.ExecuteNonQuery();
    }

    #endregion
}
