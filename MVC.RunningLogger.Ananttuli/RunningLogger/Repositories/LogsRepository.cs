using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using RunningLogger.Models;
using System.Data;

namespace RunningLogger.Repositories;
public class LogsRepository
{
    private readonly IConfiguration _config;

    public LogsRepository(IConfiguration configuration)
    {
        _config = configuration;
    }

    public List<Log> GetAll()
    {
        try
        {
            using (var connection = new SqliteConnection(_config.GetConnectionString("ConnectionString")))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = $@"
                    SELECT LogId, StartDateTime, Logs.UnitId, Units.Name as UnitName, Quantity
                    FROM Logs
                    LEFT JOIN Units ON Units.UnitId = Logs.UnitId
                ";

                var reader = command.ExecuteReader();

                return ExtractLogFromReader(reader);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);

            return new List<Log>();
        }
    }

    public Log? GetById(int id)
    {
        try
        {
            using (var connection = new SqliteConnection(_config.GetConnectionString("ConnectionString")))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = $@"
                    SELECT LogId, StartDateTime, Logs.UnitId, Units.Name as UnitName, Quantity
                    FROM Logs
                    LEFT JOIN Units ON Units.UnitId = Logs.UnitId
                    Where LogId = @LogId
                ";

                command.Parameters.AddWithValue("@LogId", id);

                var reader = command.ExecuteReader();

                var records = ExtractLogFromReader(reader);

                connection.Close();

                return records?[0] ?? null;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    public bool DeleteById(int id)
    {
        try
        {
            using (var connection = new SqliteConnection(_config.GetConnectionString("ConnectionString")))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = $@"
                    DELETE from Logs
                    WHERE LogId = @LogId;
                ";

                command.Parameters.AddWithValue("@LogId", id);

                command.ExecuteNonQuery();

                connection.Close();
                return true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }

    }

    public bool Create(Log log)
    {
        try
        {
            using (var connection = new SqliteConnection(_config.GetConnectionString("ConnectionString")))
            {
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText =
                    $@"
                        INSERT INTO Logs (StartDateTime, Quantity, UnitId) VALUES (@StartDateTime, @Quantity, @UnitId);
                    ";
                command.Parameters.AddWithValue("@StartDateTime", log.StartDateTime);
                command.Parameters.AddWithValue("@Quantity", log.Quantity);
                command.Parameters.AddWithValue("@UnitId", log.UnitId);

                command.ExecuteNonQuery();

                connection.Close();
            }

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public bool Update(Log log)
    {
        try
        {
            using (var connection = new SqliteConnection(_config.GetConnectionString("ConnectionString")))
            {
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText =
                    $@"
                        UPDATE Logs 
                        Set
                            StartDateTime = @StartDateTime,
                            Quantity = @Quantity,
                            UnitId = @UnitId
                        WHERE LogId = @LogId;
                    ";

                command.Parameters.AddWithValue("@StartDateTime", log.StartDateTime);
                command.Parameters.AddWithValue("@Quantity", log.Quantity);
                command.Parameters.AddWithValue("@UnitId", log.UnitId);
                command.Parameters.AddWithValue("@LogId", log.LogId);

                command.ExecuteNonQuery();

                connection.Close();
            }

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    private List<Log> ExtractLogFromReader(SqliteDataReader reader)
    {
        var list = new List<Log>();

        if (reader == null || !reader.HasRows)
        {
            return list;
        }


        while (reader.Read())
        {
            list.Add(new Log
            {
                LogId = reader.GetFieldValue<int>("LogId"),
                StartDateTime = reader.GetFieldValue<DateTime>("StartDateTime"),
                Quantity = reader.GetFieldValue<decimal>("Quantity"),
                UnitId = reader.GetFieldValue<int>("UnitId"),
                UnitName = reader.GetFieldValue<string>("UnitName")
            });
        }

        return list;
    }
}
