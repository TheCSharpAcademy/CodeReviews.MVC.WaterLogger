using Microsoft.Data.Sqlite;
using System.Data.SQLite;
using System.Data;

namespace WaterLogger.samggannon.Data
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

        internal DrinkingWaterDto GetDrinkingWaterRecordById(string sql)
        {
            DrinkingWaterDto drinkingWaterData = new DrinkingWaterDto();

            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SQLiteCommand(sql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            drinkingWaterData = new DrinkingWaterDto
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Date = DateTime.Parse(reader.GetString(reader.GetOrdinal("Date"))),
                                Quantity = reader.GetInt32(reader.GetOrdinal("Quantity"))
                            };
                        }
                    }
                }
            }

            return drinkingWaterData;
        }
    }
}
