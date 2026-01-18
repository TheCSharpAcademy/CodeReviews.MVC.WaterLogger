using HabitGains.Infrastructure.Database.ConnectionFactory;
using Microsoft.Data.Sqlite;

namespace HabitGains.Infrastructure.Database.Initializer;

public class DbInitializer(IDbConnectionFactory connectionFactory) : IDbInitializer
{
    public async Task RunAsync()
    {
        using SqliteConnection connection = await connectionFactory.CreateConnectionAsync();
        using SqliteCommand command = connection.CreateCommand();

        command.CommandText = """
            CREATE TABLE IF NOT EXISTS habit (
                Id TEXT PRIMARY KEY,
                Name TEXT NOT NULL,
                Measurement TEXT NOT NULL,
                Favorite INTEGER NOT NULL,
                CreatedAt TEXT NOT NULL,
                UpdatedAt TEXT NULL
                )
            """;

        await command.ExecuteNonQueryAsync();

        command.CommandText = """
            CREATE TABLE IF NOT EXISTS entry (
                Id TEXT PRIMARY KEY,
                HabitId TEXT NOT NULL,
                Date TEXT NOT NULL,
                Quantity REAL NOT NULL,
                CreatedAt TEXT NOT NULL,
                UpdatedAt TEXT NULL,
                FOREIGN KEY (HabitId) REFERENCES habit(Id)
                    ON UPDATE CASCADE
                    ON DELETE CASCADE
            )
            """;

        await command.ExecuteNonQueryAsync();
    }
}
