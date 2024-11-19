using System.Data;
using Microsoft.Data.Sqlite;
using ProgressTracker.hasona23.Models;

namespace ProgressTracker.hasona23.Services;

public class SubtaskService : ISubtaskService
{
    private readonly IConfiguration _configuration;

    public SubtaskService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private string ConnectionString => _configuration.GetConnectionString("DefaultConnection") ?? string.Empty;

    public List<SubtaskModel> GetSubtasks(int progressId)
    {
        List<SubtaskModel> subtasks = new();
        try
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            string getCommand = "SELECT * FROM SubTasks WHERE ProgressId = @progressId";
            using var command = connection.CreateCommand();
            command.CommandText = getCommand;
            command.Parameters.AddWithValue("@progressId", progressId);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                subtasks.Add(new SubtaskModel
                {
                    Title = reader["Title"].ToString(),
                    IsCompleted = (reader.GetInt32("IsCompleted") == 0)? false:true,
                    Id = reader.GetInt32("Id"),
                    ProgressId = progressId,
                });
            }

            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error Getting Subtasks: {e.Message}");
        }
        return subtasks;
    }

    public void AddSubtask(SubtaskModel subtask)
    {
        try
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            string addCommand = "INSERT INTO SubTasks VALUES (NULL,@title, @isCompleted, @progressId)";
            using var command = connection.CreateCommand();
            command.CommandText = addCommand;
            command.Parameters.AddWithValue("@title", subtask.Title);
            command.Parameters.AddWithValue("@isCompleted", subtask.IsCompleted);
            command.Parameters.AddWithValue("@progressId", subtask.ProgressId);
            command.ExecuteNonQuery();
            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error Adding Subtask: {e.Message}");
        }
    }

    public void DeleteSubtask(int subtaskId)
    {
        try
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            string deleteCommand = "DELETE FROM SubTasks WHERE Id = @subtaskId";
            using var command = connection.CreateCommand();
            command.CommandText = deleteCommand;
            command.Parameters.AddWithValue("@subtaskId", subtaskId);
            command.ExecuteNonQuery();
            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error Deleting Subtask: {e.Message}");
        }
    }
    public void DeleteProgressSubtask(int progressId)
    {
        try
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            string deleteCommand = "DELETE FROM SubTasks WHERE ProgressId = @progressId";
            using var command = connection.CreateCommand();
            command.CommandText = deleteCommand;
            command.Parameters.AddWithValue("@progressId", progressId);
            command.ExecuteNonQuery();
            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error Deleting Progress Subtasks Subtask: {e.Message}");
        }
    }
    public void UpdateSubtask(SubtaskModel subtask)
    {
        try
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            string updateCommand =
                "UPDATE SubTasks SET Title = @title, IsCompleted = @isCompleted WHERE Id = @subtaskId";
            using var command = connection.CreateCommand();
            command.CommandText = updateCommand;
            command.Parameters.AddWithValue("@title", subtask.Title);
            command.Parameters.AddWithValue("@isCompleted", subtask.IsCompleted?1:0);
            command.Parameters.AddWithValue("@subtaskId", subtask.Id);
            command.ExecuteNonQuery();
            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error Updating Subtask: {e.Message}");
        }
    }
}