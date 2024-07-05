using Microsoft.Data.Sqlite;
using WaterLogger.Models;
using WaterLogger.DTOs;
using System.Globalization;

namespace WaterLogger.Data;

public class MyLogDbContext
{
    private readonly string _connectionString;

    public MyLogDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    private SqliteConnection GetConnection() => new SqliteConnection(_connectionString);

    public List<MyLog> GetAll()
    {
        List<MyLog> records = new List<MyLog>();
        var connection = GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = @"SELECT 
                                mg.*, hb.Habit as HabitName, hb.HabitUnitId, hu.HabitUnit
                                FROM MyLogs as mg 
                                inner join 
                                    Habits as hb on mg.HabitId = hb.Id
                                inner join 
                                    HabitUnits as hu on hb.HabitUnitId = hu.Id";
        var reader = command.ExecuteReader();
        while (reader.Read())
        {
            records.Add(MapEntityFromReader(reader));
        }
        connection.Close();
        return records;
    }

    public MyLog GetById(int id)
    {
        var connection = GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = @"SELECT 
                                mg.*, hb.Habit as HabitName, hb.HabitUnitId, hu.HabitUnit
                                FROM MyLogs as mg 
                                inner join 
                                    Habits as hb on mg.HabitId = hb.Id
                                inner join 
                                    HabitUnits as hu on hb.HabitUnitId = hu.Id
                                WHERE 
                                    mg.Id = @Id";
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

    internal MyLogUpdateDTO GetByIdForUpdate(int id)
    {
        var connection = GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT * from MyLogs WHERE Id = @Id";
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

    public void Add(MyLogAddDTO record)
    {
        var connection = GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = $"INSERT INTO MyLogs(Date, HabitId, Quantity) VALUES (@Date, @HabitId, @Quantity)";
        command.Parameters.AddWithValue("@Date", record.Date.ToString("yyyy-MM-dd"));
        command.Parameters.AddWithValue("@HabitId", record.HabitId);
        command.Parameters.AddWithValue("@Quantity", record.Quantity);
        command.ExecuteNonQuery();
        connection.Close();
    }

    public void Update(MyLogUpdateDTO record)
    {
        var connection = GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = @$"UPDATE MyLogs
                                SET 
                                    Date = @Date,
                                    HabitId = @HabitId,
                                    Quantity = @Quantity
                                WHERE Id = @Id";
        command.Parameters.AddWithValue("@Date", record.Date.ToString("yyyy-MM-dd"));
        command.Parameters.AddWithValue("@HabitId", record.HabitId);
        command.Parameters.AddWithValue("@Quantity", record.Quantity);
        command.Parameters.AddWithValue("@Id", record.Id);
        command.ExecuteNonQuery();
        connection.Close();
    }

    public void Delete(int id)
    {
        var connection = GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = @$"DELETE FROM MyLogs
                                WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", id);
        command.ExecuteNonQuery();
        connection.Close();
    }

    private MyLog MapEntityFromReader(SqliteDataReader reader)
    {
        return new MyLog
        {
            Id = Convert.ToInt32(reader["Id"].ToString()),
            Date = DateTime.ParseExact(reader["Date"].ToString(), "yyyy-MM-dd", new CultureInfo("en-US"), DateTimeStyles.None),
            Quantity = Convert.ToDouble(reader["Quantity"].ToString()),
            HabitId = Convert.ToInt32(reader["HabitId"].ToString()),
            Habit = new Habit
            {
                Id = Convert.ToInt32(reader["HabitId"].ToString()),
                HabitName = reader["HabitName"].ToString(),
                HabitUintId = Convert.ToInt32(reader["HabitUnitId"].ToString()),
                HabitUnit = new HabitUnit
                {
                    Id = Convert.ToInt32(reader["HabitUnitId"].ToString()),
                    Unit = reader["HabitUnit"].ToString()
                }
            }
        };
    }

    private MyLogUpdateDTO MapEntityFromReaderForUpdate(SqliteDataReader reader)
    {
        return new MyLogUpdateDTO
        {
            Id = Convert.ToInt32(reader["Id"].ToString()),
            Date = DateTime.ParseExact(reader["Date"].ToString(), "yyyy-MM-dd", new CultureInfo("en-US"), DateTimeStyles.None),
            Quantity = Convert.ToDouble(reader["Quantity"].ToString()),
            HabitId = Convert.ToInt32(reader["HabitId"].ToString()),
        };
    }

}