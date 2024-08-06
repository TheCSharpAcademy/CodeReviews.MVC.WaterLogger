using Microsoft.Data.Sqlite;
using System.Data.SQLite;
using System.Data;

namespace WeightLogger.samggannon.Data
{
    public class DatabaseAccess
    {
      
        private readonly string _connectionString;

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

        internal void Update(string sql)
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

        internal void Delete(string sql)
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

        internal DataTable GetDataTable(string sql)
        {
            DataTable dataTable = new DataTable();

            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SQLiteCommand(sql, connection))
                {
                    using (var adapter = new SQLiteDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }

                connection.Close();
            }

            return dataTable;
        }

        internal WeightRecordDto GetWeightLogRecordById(string sql)
        {
            WeightRecordDto weightDto = new WeightRecordDto();

            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SQLiteCommand(sql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            weightDto = new WeightRecordDto
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("log_id")),
                                loggedDate = DateTime.Parse(reader.GetString(reader.GetOrdinal("log_date"))),
                                weight = reader.GetInt32(reader.GetOrdinal("weight"))
                            };
                        }
                    }
                }
            }

            return weightDto;
        }

    }
}
