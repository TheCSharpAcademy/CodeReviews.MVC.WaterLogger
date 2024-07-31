using Microsoft.Data.Sqlite;
using System.Data.SqlClient;

namespace WeightLogger.samggannon.Data
{
    public class DataAccess
    {
        private readonly DatabaseAccess _dbConnection;
        private readonly string _connectionString;

        public DataAccess()
        {
        }

        public DataAccess(string connectionString)
        {
            _connectionString = connectionString;
            _dbConnection = new DatabaseAccess(connectionString);
        }

        public bool TestConnection()
        {
            try
            {
                using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
