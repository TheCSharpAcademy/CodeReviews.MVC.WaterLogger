using Microsoft.Data.Sqlite;

namespace RunningLogger.Database
{
    public class Database
    {
        private readonly IConfiguration _configuration;

        public Database(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void InitDatabase()
        {
            string connectionString = _configuration.GetConnectionString("ConnectionString") ?? throw new Exception("Missing connection string");

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var transaction = connection.BeginTransaction();
                var command = connection.CreateCommand();
                command.CommandText = $@"
                        CREATE TABLE IF NOT EXISTS Logs
                        (
                            LogId INTEGER PRIMARY KEY AUTOINCREMENT,
                            StartDateTime DATETIME NOT NULL,
                            Quantity NUMERIC NOT NULL,
                            UnitId  INTEGER NOT NULL,
                            FOREIGN KEY (UnitId) REFERENCES Units
                        );

                        CREATE TABLE IF NOT EXISTS Units
                        (
                            UnitId INTEGER PRIMARY KEY AUTOINCREMENT,
                            Name TEXT NOT NULL UNIQUE
                        );

                        INSERT INTO Units (Name) VALUES
                        ('Kilometers'), ('Meters'), ('Miles'), ('Yards')
                        ON CONFLICT(Name) DO NOTHING;
                ";

                command.ExecuteNonQuery();
                transaction.Commit();
                connection.Close();
            }
        }

    }
}
