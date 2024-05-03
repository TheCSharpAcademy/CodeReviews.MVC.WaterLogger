using HabitHub.Models;
using Microsoft.Data.Sqlite;

namespace HabitHub.Data;

public static class HabitsRepository
{
    /// <summary>
    /// Checks if the habit already exists in the 'habits' table.
    /// Returns the existing habit name if so, an empty string otherwise.
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="habitToAdd"></param>
    /// <returns></returns>
    public static string CheckHabitExists(SqliteConnection connection, HabitModel habitToAdd)
    {
        string existingHabitName = "";

        var tableCmd = connection.CreateCommand();
        tableCmd.CommandText =
           $@"SELECT * FROM habits
               WHERE habit_name = '{habitToAdd.HabitName}';";
        tableCmd.CommandType = System.Data.CommandType.Text;

        SqliteDataReader reader = tableCmd.ExecuteReader();
        while (reader.Read())
        {
            existingHabitName = reader["habit_name"].ToString();
        }
        reader.Close();

        return existingHabitName;
    }

    /// <summary>
    /// Adds a new record to the 'habits' table.
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="habit"></param>
    public static void AddHabit(SqliteConnection connection, HabitModel habitToAdd)
    {
        var tableCmd = connection.CreateCommand();
        tableCmd.CommandText =
            $@"INSERT INTO habits (habit_name)
                       VALUES('{habitToAdd.HabitName}');";
        tableCmd.ExecuteNonQuery();
    }

    /// <summary>
    /// Retrieves all data from the 'habits' table.
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="habits"></param>
    public static void GetAllHabits(SqliteConnection connection, List<HabitModel> habits)
    {
        var tableCmd = connection.CreateCommand();
        tableCmd.CommandText =
            @"SELECT * FROM habits;";
        tableCmd.CommandType = System.Data.CommandType.Text;

        SqliteDataReader reader = tableCmd.ExecuteReader();
        while (reader.Read())
        {
            habits.Add(new HabitModel
            {
                Id = int.Parse(reader["id"].ToString()),
                HabitName = reader["habit_name"].ToString()
            });
        }
        reader.Close();
    }

    /// <summary>
    /// Retrieves all habit names form the 'habits' table.
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="habitNames"></param>
    public static void GetAllHabitNames(SqliteConnection connection, List<string> habitNames)
    {
        var tableCmd = connection.CreateCommand();
        tableCmd.CommandText =
            @"SELECT habit_name FROM habits;";
        tableCmd.CommandType = System.Data.CommandType.Text;

        SqliteDataReader reader = tableCmd.ExecuteReader();
        while (reader.Read())
        {
            habitNames.Add(Convert.ToString(reader["habit_name"]));
        }
        reader.Close();
    }

    /// <summary>
    /// Returns the id of the habit based on its name, or -1 if no matching habit exists.
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="habitName"></param>
    /// <returns></returns>
    public static int GetHabitId(SqliteConnection connection, string habitName)
    {
        int habitIndex = -1;

        var tableCmd = connection.CreateCommand();
        tableCmd.CommandText =
            $@"SELECT id FROM habits
               WHERE habit_name LIKE '%{habitName}%'
               LIMIT 1;";
        tableCmd.CommandType = System.Data.CommandType.Text;

        SqliteDataReader reader = tableCmd.ExecuteReader();
        while (reader.Read())
        {
            habitIndex = int.Parse(reader["id"].ToString());
            break;
        }
        reader.Close();

        return habitIndex;
    }

    /// <summary>
    /// Deletes the record from the 'habits' table based on the habit name given.
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="habitName"></param>
    public static void DeleteHabit(SqliteConnection connection, string habitToDelete)
    {
        var tableCmd = connection.CreateCommand();
        tableCmd.CommandText =
            $@"DELETE FROM habits
               WHERE habit_name LIKE '%{habitToDelete}%';";
        tableCmd.ExecuteNonQuery();
    }

    /// <summary>
    /// Adds habit record to the 'habit_records' table.
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="habitToRecord"></param>
    /// <param name="habitRecord"></param>
    public static void AddHabitRecord(SqliteConnection connection, string habitToRecord, HabitRecordModel habitRecord)
    {
        var tableCmd = connection.CreateCommand();
        tableCmd.CommandText =
            @$"INSERT INTO habit_records(habits_id, amount, unit, date)
               VALUES((SELECT id
                       FROM habits
                       WHERE habit_name = '{habitToRecord}'),
                       '{habitRecord.Amount}', '{habitRecord.Unit}', '{habitRecord.Date}');";
        tableCmd.ExecuteNonQuery();
    }

    /// <summary>
    /// Retrieves all data from the 'habit_records' table.
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="habitRecords"></param>
    public static void GetAllHabitRecords(SqliteConnection connection, List<HabitRecordModel> habitRecords)
    {
        var tableCmd = connection.CreateCommand();
        tableCmd.CommandText =
            @"SELECT * FROM habit_records;";
        tableCmd.CommandType = System.Data.CommandType.Text;

        SqliteDataReader reader = tableCmd.ExecuteReader();
        while (reader.Read())
        {
            habitRecords.Add(new HabitRecordModel
            {
                Id = int.Parse(reader["id"].ToString()),
                HabitsId = int.Parse(reader["habits_id"].ToString()),
                Amount = float.Parse(reader["amount"].ToString()),
                Unit = reader["unit"].ToString(),
                Date = DateTime.Parse(reader["date"].ToString())
            });
        }
        reader.Close();
    }

    /// <summary>
    /// Updates the record in the 'habit_records' table.
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="habitIndex"></param>
    /// <param name="recordToUpdate"></param>
    public static void UpdateRecord(SqliteConnection connection, int habitId, HabitRecordModel recordToUpdate)
    {
        var tableCmd = connection.CreateCommand();
        tableCmd.CommandText =
            $@"UPDATE habit_records
               SET habits_id = {habitId}, amount = {recordToUpdate.Amount}, unit = '{recordToUpdate.Unit}', date = '{recordToUpdate.Date}'
               WHERE id = {recordToUpdate.Id}";
        tableCmd.ExecuteNonQuery();
    }

    /// <summary>
    /// Deletes the record from the 'habit_records' table based on the record id given.
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="recordToDelete"></param>
    public static void DeleteRecord(SqliteConnection connection, int recordToDelete)
    {
        var tableCmd = connection.CreateCommand();
        tableCmd.CommandText =
            $@"DELETE FROM habit_records
               WHERE id = {recordToDelete};";
        tableCmd.ExecuteNonQuery();
    }
}
