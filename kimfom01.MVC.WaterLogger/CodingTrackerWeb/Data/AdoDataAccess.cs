using System.Globalization;
using CodingTrackerWeb.Models;
using Microsoft.Data.Sqlite;

namespace CodingTrackerWeb.Data;

public class AdoDataAccess : IDataAccess
{
    private readonly IConfiguration _configuration;

    public AdoDataAccess(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void InsertRecord(CodingHour codingHour)
    {
        using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
        {
            using (var command = connection.CreateCommand())
            {
                connection.Open();

                command.CommandText = @$"INSERT INTO coding_hours(Date, StartTime, EndTime, Duration)
                                            VALUES('{codingHour.Date}', '{codingHour.StartTime}', '{codingHour.EndTime}', '{GetDuration(codingHour.StartTime, codingHour.EndTime)}')";

                command.ExecuteNonQuery();
            }
        }
    }

    public List<CodingHour> GetAllRecords()
    {
        var codingHoursModels = new List<CodingHour>();

        using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
        {
            using (var command = connection.CreateCommand())
            {
                connection.Open();

                command.CommandText = "SELECT * FROM coding_hours";

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var tempModel = new CodingHour
                    {
                        Id = reader.GetInt32(0),
                        Date = (string)reader["Date"],
                        StartTime = (string)reader["StartTime"],
                        EndTime = (string)reader["EndTime"],
                        Duration = (string)reader["Duration"]
                    };

                    codingHoursModels.Add(tempModel);
                }
            }
        }

        return codingHoursModels;
    }

    public void UpdateRecord(int id, CodingHour codingHour)
    {
        using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
        {
            using (var command = connection.CreateCommand())
            {
                connection.Open();

                command.CommandText = @$"UPDATE coding_hours
                                            SET Date = '{codingHour.Date}', StartTime = '{codingHour.StartTime}', EndTime = '{codingHour.EndTime}', Duration = '{GetDuration(codingHour.StartTime, codingHour.EndTime)}'
                                            WHERE Id = {id}";

                command.ExecuteNonQuery();
            }
        }
    }

    public void DeleteRecord(int id)
    {
        using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
        {
            using (var command = connection.CreateCommand())
            {
                connection.Open();

                command.CommandText = @$"DELETE FROM coding_hours
                                            WHERE Id = {id}";

                command.ExecuteNonQuery();
            }
        }
    }

    public CodingHour GetById(int id)
    {
        using var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString"));
        {
            using (var command = connection.CreateCommand())
            {
                var codingHours = new CodingHour();

                connection.Open();

                command.CommandText = @$"SELECT * FROM coding_hours
                                            WHERE Id = {id}";

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    codingHours.Id = reader.GetInt32(0);
                    codingHours.Date = (string)reader["Date"];
                    codingHours.StartTime = (string)reader["StartTime"];
                    codingHours.EndTime = (string)reader["EndTime"];
                    codingHours.Duration = (string)reader["Duration"];
                }

                return codingHours;
            }
        }
    }

    private string GetDuration(string startTime, string endTime)
    {
        DateTime parsedStartTime = DateTime.ParseExact(startTime, "HH:mm", null, DateTimeStyles.None);
        DateTime parsedEndTime = DateTime.ParseExact(endTime, "HH:mm", null, DateTimeStyles.None);

        return parsedEndTime.Subtract(parsedStartTime).ToString();
    }
}