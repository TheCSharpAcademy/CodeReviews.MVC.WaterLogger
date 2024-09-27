using System.Globalization;
using Microsoft.Data.Sqlite;
using WaterLogger.DTOs;
using WaterLogger.Models;

namespace WaterLogger.Data;

public class DailyExpensesDbContext
{
    private readonly string _connectionString;

    public DailyExpensesDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    private SqliteConnection GetConnection() => new SqliteConnection(_connectionString);

    public List<DailyExpense> GetAll()
    {
        List<DailyExpense> records = new List<DailyExpense>();
        var connection = GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM DailyExpenses";
        var reader = command.ExecuteReader();
        while (reader.Read())
        {
            records.Add(MapExpenseFromReader(reader));
        }
        connection.Close();
        return records;
    }

    public DailyExpense GetById(int id)
    {
        var connection = GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM DailyExpenses WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", id);
        using (var reader = command.ExecuteReader())
        {
            if (reader.Read() == false)
                return null;

            var record = MapExpenseFromReader(reader);
            connection.Close();
            return record;
        }
    }

    public void Add(DailyExpenseAddDTO record)
    {
        var connection = GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = $"INSERT INTO DailyExpenses(Date, Amount, ExpenseType) VALUES (@Date, @Amount, @ExpenseType)";
        command.Parameters.AddWithValue("@Date", record.Date.ToString("yyyy-MM-dd"));
        command.Parameters.AddWithValue("@Amount", record.Amount);
        command.Parameters.AddWithValue("@ExpenseType", record.ExpenseType);
        command.ExecuteNonQuery();
        connection.Close();
    }

    public void Update(DailyExpense record)
    {
        var connection = GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = @$"UPDATE DailyExpenses
                                SET 
                                    Date = @Date, 
                                    Amount = @Amount,
                                    ExpenseType = @ExpenseType
                                WHERE Id = @Id";
        command.Parameters.AddWithValue("@Date", record.Date.ToString("yyyy-MM-dd"));
        command.Parameters.AddWithValue("@Amount", record.Amount);
        command.Parameters.AddWithValue("@ExpenseType", record.ExpenseType);
        command.Parameters.AddWithValue("@Id", record.Id);
        command.ExecuteNonQuery();
        connection.Close();
    }

    public void Delete(int id)
    {
        var connection = GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = @$"DELETE FROM DailyExpenses
                                WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", id);
        command.ExecuteNonQuery();
        connection.Close();
    }

    private DailyExpense MapExpenseFromReader(SqliteDataReader reader)
    {
        return new DailyExpense
        {
            Id = Convert.ToInt32(reader["Id"].ToString()),
            Date = DateTime.ParseExact(reader["Date"].ToString(), "yyyy-MM-dd", new CultureInfo("en-US"), DateTimeStyles.None),
            Amount = Convert.ToDouble(reader["Amount"].ToString()),
            ExpenseType = reader["ExpenseType"].ToString()
        };
    }

}