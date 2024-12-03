using System;
using System.Globalization;
using Microsoft.Data.Sqlite;
using SQLitePCL;
using Water_Logger.Models;

namespace Water_Logger.Data;

public class Database
{
    private string _sqlConnection;
    public Database(IConfiguration config)
    {
        _sqlConnection = config.GetConnectionString("SQLiteConnection") ?? "";
    }

    public List<DrinkingWater> GetAll()
    {
        List<DrinkingWater> logList = new();
        using var connection = new SqliteConnection(_sqlConnection);
        connection.Open();
        
        var sqlCmd = connection.CreateCommand();
        sqlCmd.CommandText = @"SELECT * FROM drinking_water";

        using var reader = sqlCmd.ExecuteReader();
        if(reader.HasRows)
        {
            while(reader.Read())
            {
                logList.Add(new DrinkingWater()
                {
                    Id = reader.GetInt32(0),
                    Date = DateTime.ParseExact(reader.GetString(1), "dd-MM-yyyy", CultureInfo.InvariantCulture),
                    Quantity = reader.GetDouble(2)
                });
            }
        }

        connection.Close();

        return logList;
    }

    public DrinkingWater Get(int id)
    {

        DrinkingWater log = new();
        using var connection = new SqliteConnection(_sqlConnection);
        connection.Open();

        var sqlCmd = connection.CreateCommand();
        sqlCmd.CommandText = @$"SELECT * FROM drinking_water
            WHERE Id = {id}";

        using var reader = sqlCmd.ExecuteReader();
        if (reader.HasRows)
        {
            while (reader.Read())
            {

                log.Id = reader.GetInt32(0);
                log.Date = DateTime.ParseExact(reader.GetString(1), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                log.Quantity = reader.GetDouble(2);

            }
        }

        connection.Close();

        return log;
    }

    public void Create(DrinkingWater drinkingWater)
    {
        using var connection = new SqliteConnection(_sqlConnection);
        connection.Open();

        var sqlCmd = connection.CreateCommand();
        sqlCmd.CommandText = @$"INSERT INTO drinking_water (Date, Quantity)
            VALUES ('{drinkingWater.Date.ToString("dd-MM-yyyy")}', {drinkingWater.Quantity})";
        sqlCmd.ExecuteNonQuery();

        connection.Close();
    }

    public void Update(DrinkingWater drinkingWater)
    {
        using var connection = new SqliteConnection(_sqlConnection);
        connection.Open();

        var sqlCmd = connection.CreateCommand();
        sqlCmd.CommandText = @$"UPDATE drinking_water
            SET Date = '{drinkingWater.Date.ToString("dd-MM-yyyy")}', 
                Quantity = {drinkingWater.Quantity}
            WHERE Id = {drinkingWater.Id}";

        sqlCmd.ExecuteNonQuery();

        connection.Close();
    }

    public void Delete(int id)
    {
        using var connection = new SqliteConnection(_sqlConnection);
        connection.Open();

        var sqlCmd = connection.CreateCommand();
        sqlCmd.CommandText = @$"DELETE FROM drinking_water
            WHERE Id = {id}";

        sqlCmd.ExecuteNonQuery(); 

        connection.Close();
    }
}
