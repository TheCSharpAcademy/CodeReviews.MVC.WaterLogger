using Microsoft.Data.Sqlite;

namespace WaterLogger.samggannon.Data
{
    public class DatabaseAccess
    {
        private readonly string _connectionString;

        public DatabaseAccess() { }

        public DatabaseAccess(string connectionString)
        {
            _connectionString = connectionString;
            SQLitePCL.Batteries.Init();
        }

        public void Insert(string sql)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = sql;
                tableCmd.ExecuteNonQuery();

                connection.Close();
            }
        }
    }
}
