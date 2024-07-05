using Microsoft.Data.Sqlite;
using WaterLogger.Models;
using WaterLogger.DTOs;

namespace WaterLogger.Data;

public class HabitUnitDbContext
{
    private readonly string _connectionString;

    public HabitUnitDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    private SqliteConnection GetConnection() => new SqliteConnection(_connectionString);

    public List<HabitUnit> GetAll()
    {
        List<HabitUnit> records = new List<HabitUnit>();
        var connection = GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM HabitUnits";
        var reader = command.ExecuteReader();
        while (reader.Read())
        {
            records.Add(MapExpenseFromReader(reader));
        }
        connection.Close();
        return records;
    }

    public HabitUnit GetById(int id)
    {
        var connection = GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM HabitUnits WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", id);
        using (var reader = command.ExecuteReader())
        {
            if (reader.Read() == false)
                return null;

            var record = MapExpenseFromReader(reader);
            connection.Close();
            return record;
        }
    }

    public void Add(HabitUnitAddDTO record)
    {
        var connection = GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = $"INSERT INTO HabitUnits(HabitUnit) VALUES (@HabitUnit)";
        command.Parameters.AddWithValue("@HabitUnit", record.Unit);
        command.ExecuteNonQuery();
        connection.Close();
    }

    public void Update(HabitUnit record)
    {
        var connection = GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = @$"UPDATE HabitUnits
                                SET 
                                    HabitUnit = @HabitUnit
                                WHERE Id = @Id";
        command.Parameters.AddWithValue("@HabitUnit", record.Unit);
        command.Parameters.AddWithValue("@Id", record.Id);
        command.ExecuteNonQuery();
        connection.Close();
    }

    public void Delete(int id)
    {
        var connection = GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = @$"DELETE FROM HabitUnits
                                WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", id);
        command.ExecuteNonQuery();
        connection.Close();
    }

    private HabitUnit MapExpenseFromReader(SqliteDataReader reader)
    {
        return new HabitUnit
        {
            Id = Convert.ToInt32(reader["Id"].ToString()),
            Unit = reader["HabitUnit"].ToString()
        };
    }

}