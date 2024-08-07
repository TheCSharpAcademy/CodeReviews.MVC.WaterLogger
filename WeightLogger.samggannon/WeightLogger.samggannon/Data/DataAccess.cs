using Microsoft.Data.Sqlite;
using System.Data;
using System.Data.SqlClient;
using WeightLogger.samggannon.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        internal bool DeleteWeightLog(int id)
        {
            string sql = $"DELETE FROM WeightLogs WHERE log_id = {id}";
            return _dbConnection.Delete(sql);
        }

        internal bool EditWeightLog(int id)
        {
            throw new NotImplementedException();
        }

        internal bool EditWeightLog(decimal weightValue, DateTime? loggedDate)
        {
            throw new NotImplementedException();
        }

        internal DataTable GetWeightHistory()
        {
            string sql = "SELECT * FROM WeightLogs";
            return  _dbConnection.GetDataTable(sql);
        }

        internal WeightRecordDto GetWeightLogById(int id)
        {
            string sql = $"SELECT * FROM WeightLogs WHERE log_id = {id}";
            return _dbConnection.GetWeightLogRecordById(sql);
        }

        internal void LogThisWeight(decimal weight, string logDate)
        {
            string sql = $"INSERT INTO WeightLogs(weight, log_date) VALUES({weight}, '{logDate}')";
            _dbConnection.Insert(sql);
        }
    }
}
