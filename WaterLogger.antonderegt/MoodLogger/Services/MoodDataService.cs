using System.Globalization;
using Microsoft.Data.Sqlite;
using MoodLogger.Models;

namespace MoodLogger.Services;

public class MoodDataService(IConfiguration configuration) : IMoodDataService
{
    private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new ApplicationException("DefaultConnection not found");

    public void Initialize()
    {
        using SqliteConnection connection = new(_connectionString);
        connection.Open();
        using SqliteCommand command = connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS mood (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Date TEXT NOT NULL,
                MoodLevel INTEGER NOT NULL
            );
        ";
        command.ExecuteNonQuery();
    }

    public void AddMoodRecord(Mood moodRecord)
    {
        using SqliteConnection connection = new(_connectionString);
        connection.Open();
        using SqliteCommand command = connection.CreateCommand();

        command.CommandText = @"
            INSERT INTO mood(Date, MoodLevel) VALUES (@date, @moodLevel);
        ";
        command.Parameters.Add("@date", SqliteType.Text).Value = moodRecord?.Date;
        command.Parameters.Add("@moodLevel", SqliteType.Integer).Value = moodRecord?.MoodLevel;

        command.ExecuteNonQuery();
    }

    public List<Mood> GetAllRecords()
    {
        using SqliteConnection connection = new(_connectionString);
        connection.Open();
        using SqliteCommand command = connection.CreateCommand();
        command.CommandText = @"
            SELECT * FROM mood;
        ";

        List<Mood> moodRecords = [];

        SqliteDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            moodRecords.Add(new Mood()
            {
                Id = reader.GetInt32(0),
                Date = DateTime.Parse(reader.GetString(1), CultureInfo.CurrentUICulture.DateTimeFormat),
                MoodLevel = reader.GetInt32(2)
            });
        }

        return moodRecords;
    }

    public Mood GetById(int id)
    {
        using SqliteConnection connection = new(_connectionString);
        connection.Open();
        using SqliteCommand command = connection.CreateCommand();
        command.CommandText = @"
            SELECT * FROM mood WHERE Id = @id;
        ";
        command.Parameters.Add("@id", SqliteType.Integer).Value = id;

        SqliteDataReader reader = command.ExecuteReader();

        Mood moodRecord = new();
        while (reader.Read())
        {
            moodRecord.Id = reader.GetInt32(0);
            moodRecord.Date = DateTime.Parse(reader.GetString(1), CultureInfo.CurrentUICulture.DateTimeFormat);
            moodRecord.MoodLevel = reader.GetInt32(2);
        }

        return moodRecord;
    }

    public void Update(Mood moodRecord)
    {
        using SqliteConnection connection = new(_connectionString);
        connection.Open();
        using SqliteCommand command = connection.CreateCommand();

        command.CommandText = @"
            UPDATE mood SET Date = @date, MoodLevel = @moodLevel WHERE Id = @id;
        ";
        command.Parameters.Add("@date", SqliteType.Text).Value = moodRecord?.Date;
        command.Parameters.Add("@moodLevel", SqliteType.Integer).Value = moodRecord?.MoodLevel;
        command.Parameters.Add("@id", SqliteType.Integer).Value = moodRecord?.Id;

        command.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        using SqliteConnection connection = new(_connectionString);
        connection.Open();
        using SqliteCommand command = connection.CreateCommand();

        command.CommandText = @"
            DELETE FROM mood WHERE Id = @id;
        ";
        command.Parameters.Add("@id", SqliteType.Integer).Value = id;

        command.ExecuteNonQuery();
    }
}