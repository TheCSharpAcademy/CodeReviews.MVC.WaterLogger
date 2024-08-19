using HabitTracker.Application.Repositories;
using HabitTracker.Domain.Constants;
using HabitTracker.Domain.Entities;
using HabitTracker.Infrastructure.Contexts;
using HabitTracker.Infrastructure.Extensions;
using HabitTracker.Infrastructure.Queries;
using Microsoft.Data.Sqlite;

namespace HabitTracker.Infrastructure.Repositories;

/// <summary>
/// Repository class that interacts with the HabitLog table only.
/// </summary>
internal class HabitLogRepository : IHabitLogRepository
{
    #region Fields

    private readonly string _connectionString;

    #endregion
    #region Constructors

    public HabitLogRepository(IDbContext dbContext)
    {
        _connectionString = dbContext.ConnectionString;
    }

    #endregion
    #region Methods

    public int AddHabitLog(HabitLog habitLog)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = HabitLogQueries.AddHabitLog;
        command.Parameters.Add("$Id", SqliteType.Text).Value = habitLog.Id;
        command.Parameters.Add("$HabitId", SqliteType.Text).Value = habitLog.HabitId;
        command.Parameters.Add("Date", SqliteType.Text).Value = habitLog.Date.ToString(FormatString.ISO8601);
        command.Parameters.Add("Quantity", SqliteType.Integer).Value = habitLog.Quantity;

        return command.ExecuteNonQuery();
    }

    public int DeleteHabitLog(Guid id)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = HabitLogQueries.DeleteHabitLog;
        command.Parameters.Add("$Id", SqliteType.Text).Value = id;

        return command.ExecuteNonQuery();
    }

    public HabitLog? GetHabitLog(Guid id)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = HabitLogQueries.GetHabitLog;
        command.Parameters.Add("$Id", SqliteType.Text).Value = id;

        using SqliteDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            return new HabitLog
            {
                Id = reader.GetGuid("Id"),
                HabitId = reader.GetGuid("HabitId"),
                Date = reader.GetDateTime("Date"),
                Quantity = reader.GetInt32("Quantity"),
            };
        }
        else
        {
            return null;
        }
    }

    public HabitLog? GetHabitLogByDate(Guid habitId, DateTime date)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = HabitLogQueries.GetHabitLogByDate;
        command.Parameters.Add("$HabitId", SqliteType.Text).Value = habitId;
        command.Parameters.Add("$Date", SqliteType.Text).Value = date.ToString(FormatString.ISO8601);

        using SqliteDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            return new HabitLog
            {
                Id = reader.GetGuid("Id"),
                HabitId = reader.GetGuid("HabitId"),
                Date = reader.GetDateTime("Date"),
                Quantity = reader.GetInt32("Quantity"),
            };
        }
        else
        {
            return null;
        }
    }

    public List<HabitLog> GetHabitLogs()
    {
        List<HabitLog> output = [];

        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = HabitLogQueries.GetHabitLogs;

        using SqliteDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            output.Add(new HabitLog
            {
                Id = reader.GetGuid("Id"),
                HabitId = reader.GetGuid("HabitId"),
                Date = reader.GetDateTime("Date"),
                Quantity = reader.GetInt32("Quantity"),
            });
        }

        return output;
    }

    public List<HabitLog> GetHabitLogs(Guid habitId)
    {
        List<HabitLog> output = [];

        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = HabitLogQueries.GetHabitLogsByHabitId;
        command.Parameters.Add("$HabitId", SqliteType.Text).Value = habitId;

        using SqliteDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            output.Add(new HabitLog
            {
                Id = reader.GetGuid("Id"),
                HabitId = reader.GetGuid("HabitId"),
                Date = reader.GetDateTime("Date"),
                Quantity = reader.GetInt32("Quantity"),
            });
        }

        return output;
    }

    public List<HabitLog> GetHabitLogsByDateRange(DateTime from, DateTime to)
    {
        List<HabitLog> output = [];

        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = HabitLogQueries.GetHabitLogsByDateRange;
        command.Parameters.Add("$DateFrom", SqliteType.Text).Value = from.ToString(FormatString.ISO8601);
        command.Parameters.Add("$DateTo", SqliteType.Text).Value = to.ToString(FormatString.ISO8601);

        using SqliteDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            output.Add(new HabitLog
            {
                Id = reader.GetGuid("Id"),
                HabitId = reader.GetGuid("HabitId"),
                Date = reader.GetDateTime("Date"),
                Quantity = reader.GetInt32("Quantity"),
            });
        }

        return output;
    }

    public List<HabitLog> GetHabitLogsByDateRange(Guid habitId, DateTime from, DateTime to)
    {
        List<HabitLog> output = [];

        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = HabitLogQueries.GetHabitLogsByHabitIdAndDateRange;
        command.Parameters.Add("$HabitId", SqliteType.Text).Value = habitId;
        command.Parameters.Add("$DateFrom", SqliteType.Text).Value = from.ToString(FormatString.ISO8601);
        command.Parameters.Add("$DateTo", SqliteType.Text).Value = to.ToString(FormatString.ISO8601);

        using SqliteDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            output.Add(new HabitLog
            {
                Id = reader.GetGuid("Id"),
                HabitId = reader.GetGuid("HabitId"),
                Date = reader.GetDateTime("Date"),
                Quantity = reader.GetInt32("Quantity"),
            });
        }

        return output;
    }

    public int UpdateHabitLog(HabitLog habitLog)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = HabitLogQueries.UpdateHabitLog;
        command.Parameters.Add("$Id", SqliteType.Text).Value = habitLog.Id;
        command.Parameters.Add("$Date", SqliteType.Text).Value = habitLog.Date.ToString(FormatString.ISO8601);
        command.Parameters.Add("$Quantity", SqliteType.Integer).Value = habitLog.Quantity;

        return command.ExecuteNonQuery();
    }

    #endregion
}
