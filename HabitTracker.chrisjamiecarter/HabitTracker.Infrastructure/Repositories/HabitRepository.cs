using HabitTracker.Application.Repositories;
using HabitTracker.Domain.Entities;
using HabitTracker.Infrastructure.Contexts;
using HabitTracker.Infrastructure.Extensions;
using HabitTracker.Infrastructure.Queries;
using Microsoft.Data.Sqlite;

namespace HabitTracker.Infrastructure.Repositories;

/// <summary>
/// Repository class that interacts with the Habit table only.
/// </summary>
internal class HabitRepository : IHabitRepository
{
    #region Fields

    private readonly string _connectionString;

    #endregion
    #region Constructors

    public HabitRepository(IDbContext dbContext)
    {
        _connectionString = dbContext.ConnectionString;
    }

    #endregion
    #region Methods

    public int AddHabit(Habit habit)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = HabitQueries.AddHabit;
        command.Parameters.Add("$Id", SqliteType.Text).Value = habit.Id;
        command.Parameters.Add("$Name", SqliteType.Text).Value = habit.Name;
        command.Parameters.Add("$Measure", SqliteType.Text).Value = habit.Measure;

        return command.ExecuteNonQuery();
    }

    public Habit? GetHabit(Guid id)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = HabitQueries.GetHabit;
        command.Parameters.Add("$Id", SqliteType.Text).Value = id;

        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            return new Habit
            {
                Id = reader.GetGuid("Id"),
                Name = reader.GetString("Name"),
                Measure = reader.GetString("Measure"),
                IsActive = reader.GetBoolean("IsActive"),
            };
        }
        else
        {
            return null;
        }
    }

    public Habit? GetHabit(string name)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = HabitQueries.GetHabitByName;
        command.Parameters.Add("$Name", SqliteType.Text).Value = name;

        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            return new Habit
            {
                Id = reader.GetGuid("Id"),
                Name = reader.GetString("Name"),
                Measure = reader.GetString("Measure"),
                IsActive = reader.GetBoolean("IsActive"),
            };
        }
        else
        {
            return null;
        }
    }

    public List<Habit> GetHabits()
    {
        List<Habit> habits = [];

        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = HabitQueries.GetHabits;

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            habits.Add(new Habit
            {
                Id = reader.GetGuid("Id"),
                Name = reader.GetString("Name"),
                Measure = reader.GetString("Measure"),
                IsActive = reader.GetBoolean("IsActive"),
            });
        }

        return habits;
    }

    public List<Habit> GetHabitsByIsActive(bool isActive)
    {
        List<Habit> habits = [];

        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = HabitQueries.GetHabitsByIsActive;
        command.Parameters.Add("$IsActive", SqliteType.Integer).Value = isActive;

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            habits.Add(new Habit
            {
                Id = reader.GetGuid("Id"),
                Name = reader.GetString("Name"),
                Measure = reader.GetString("Measure"),
                IsActive = reader.GetBoolean("IsActive"),
            });
        }

        return habits;
    }

    public int UpdateHabit(Habit habit)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = HabitQueries.UpdateHabit;
        command.Parameters.Add("$Id", SqliteType.Text).Value = habit.Id;
        command.Parameters.Add("$Name", SqliteType.Text).Value = habit.Name;
        command.Parameters.Add("$Measure", SqliteType.Text).Value = habit.Measure;
        command.Parameters.Add("$IsActive", SqliteType.Integer).Value = habit.IsActive;

        return command.ExecuteNonQuery();
    }

    #endregion
}
