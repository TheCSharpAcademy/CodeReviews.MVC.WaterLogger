using Microsoft.Data.Sqlite;

namespace HabitGains.Infrastructure.Database.ConnectionFactory;

public interface IDbConnectionFactory
{
    Task<SqliteConnection> CreateConnectionAsync(CancellationToken token = default);
}
