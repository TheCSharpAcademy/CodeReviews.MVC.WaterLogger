using Microsoft.Data.Sqlite;
using ProgressTracker.hasona23.Models;

namespace ProgressTracker.hasona23.Services;

public class ProgressService:IProgressService
{
    private readonly IConfiguration _configuration;

    public ProgressService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    private string ConnectionString => _configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
    public List<ProgressModel> GetProgresses()
    {
        List<ProgressModel> progresses = new List<ProgressModel>();
        try
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            string getCommand = "SELECT * FROM Progresses";
            using var command = connection.CreateCommand();
            command.CommandText = getCommand;
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                progresses.Add(new ProgressModel(int.Parse(reader["Id"].ToString()), (string)reader["Title"]));
            }

            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error Getting Progresses: {e}");
        }
        return progresses;
    }

    public void DeleteProgresses(int progressId)
    {
        try
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            string deleteCommand = "DELETE FROM Progresses WHERE Id = @Id";
            using var command = connection.CreateCommand();
            command.CommandText = deleteCommand;
            command.Parameters.AddWithValue("@Id", progressId);
            command.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error Deleting Progresses: {e.Message}");
        }
    }

    public void AddProgress(ProgressModel progress)
    {
        try
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            string addCommand = "INSERT INTO Progresses VALUES (NULL,@Title)";
            using var command = connection.CreateCommand();
            command.CommandText = addCommand;
            command.Parameters.AddWithValue("@Title", progress.Title);
            command.ExecuteNonQuery();
            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error Adding Progress: {e.Message}");
        }
    }

    public void UpdateProgress(ProgressModel progress)
    {
        try
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            string updateCommand = "UPDATE  Progresses SET Title = @title WHERE Id = @Id";
            using var command = connection.CreateCommand();
            command.CommandText = updateCommand;
            command.Parameters.AddWithValue("@Id", progress.Id);
            command.Parameters.AddWithValue("@title", progress.Title);
            command.ExecuteNonQuery();
            connection.Close();
            
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error Updating Progress: {e.Message}");
        }
    }

    public ProgressModel GetProgress(int progressId)
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();
        string getCommand = "SELECT * FROM Progresses WHERE Id = @Id";
        using var command = connection.CreateCommand();
        command.CommandText = getCommand;
        command.Parameters.AddWithValue("@Id", progressId);
        var reader = command.ExecuteReader();
        ProgressModel progress=new();
        while (reader.Read())
        {
            progress = new ProgressModel(int.Parse(reader["Id"].ToString()), (string)reader["Title"]);
        }
        connection.Close();
        return progress;
    }
}