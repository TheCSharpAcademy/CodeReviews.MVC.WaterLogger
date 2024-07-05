using Microsoft.Data.Sqlite;
using WaterLogger.Models;
using WaterLogger.DTOs;

namespace WaterLogger.Data;

public class HabitDbContext
{
    private readonly string _connectionString;

    public HabitDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    private SqliteConnection GetConnection() => new SqliteConnection(_connectionString);

    public List<Habit> GetAll()
    {
        List<Habit> records = new List<Habit>();
        var connection = GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT hb.*, hu.HabitUnit FROM Habits as hb inner join HabitUnits as hu on hb.HabitUnitId = hu.Id";
        var reader = command.ExecuteReader();
        while (reader.Read())
        {
            records.Add(MapEntityFromReader(reader));
        }
        connection.Close();
        return records;
    }

    public Habit GetById(int id)
    {
        var connection = GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT hb.*, hu.HabitUnit FROM Habits as hb inner join HabitUnits as hu on hb.HabitUnitId = hu.Id WHERE hb.Id = @Id";
        command.Parameters.AddWithValue("@Id", id);
        using (var reader = command.ExecuteReader())
        {
            if (reader.Read() == false)
                return null;

            var record = MapEntityFromReader(reader);
            connection.Close();
            return record;
        }
    }

    internal HabitUpdateDTO GetByIdForUpdate(int id)
    {
        var connection = GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT * from Habits WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", id);
        using (var reader = command.ExecuteReader())
        {
            if (reader.Read() == false)
                return null;

            var record = MapEntityFromReaderForUpdate(reader);
            connection.Close();
            return record;
        }
    }

    public void Add(HabitAddDTO record)
    {
        var connection = GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = $"INSERT INTO Habits(Habit, HabitUnitId) VALUES (@Habit, @HabitUnitId)";
        command.Parameters.AddWithValue("@Habit", record.HabitName);
        command.Parameters.AddWithValue("@HabitUnitId", record.HabitUintId);
        command.ExecuteNonQuery();
        connection.Close();
    }

    public void Update(HabitUpdateDTO record)
    {
        var connection = GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = @$"UPDATE Habits
                                SET 
                                    Habit = @Habit,
                                    HabitUnitId = @HabitUnitId
                                WHERE Id = @Id";
        command.Parameters.AddWithValue("@Habit", record.HabitName);
        command.Parameters.AddWithValue("@HabitUnitId", record.HabitUintId);
        command.Parameters.AddWithValue("@Id", record.Id);
        command.ExecuteNonQuery();
        connection.Close();
    }

    public void Delete(int id)
    {
        var connection = GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = @$"DELETE FROM Habits
                                WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", id);
        command.ExecuteNonQuery();
        connection.Close();
    }

    private Habit MapEntityFromReader(SqliteDataReader reader)
    {
        return new Habit
        {
            Id = Convert.ToInt32(reader["Id"].ToString()),
            HabitName = reader["Habit"].ToString(),
            HabitUintId = Convert.ToInt32(reader["HabitUnitId"].ToString()),
            HabitUnit = new HabitUnit
            {
                Id = Convert.ToInt32(reader["HabitUnitId"].ToString()),
                Unit =  reader["HabitUnit"].ToString()
            }
        };
    }

    private HabitUpdateDTO MapEntityFromReaderForUpdate(SqliteDataReader reader)
    {
        return new HabitUpdateDTO
        {
            Id = Convert.ToInt32(reader["Id"].ToString()),
            HabitName = reader["Habit"].ToString(),
            HabitUintId = Convert.ToInt32(reader["HabitUnitId"].ToString()),
        };
    }

}