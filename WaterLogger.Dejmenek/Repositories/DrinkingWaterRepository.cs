using Microsoft.Data.Sqlite;
using WaterLogger.Dejmenek.Models;

namespace WaterLogger.Dejmenek.Repositories;

public class DrinkingWaterRepository : IDrinkingWaterRepository
{
    private readonly IConfiguration _configuration;

    public DrinkingWaterRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Create(DrinkingWater drinkingWater)
    {
        using (var connection = new SqliteConnection(_configuration.GetConnectionString("WaterLogger")))
        {
            connection.Open();
            string sql = @"INSERT INTO drinking_water (Quantity, Date, MeasureId)
                               VALUES (@Quantity, @Date, @MeasureId)
            ";

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@Quantity", drinkingWater.Quantity);
            command.Parameters.AddWithValue("@Date", drinkingWater.Date);
            command.Parameters.AddWithValue("@MeasureId", drinkingWater.MeasureId);

            command.ExecuteNonQuery();
        }
    }

    public List<DrinkingWater> GetAllRecords()
    {
        var data = new List<DrinkingWater>();
        using (var connection = new SqliteConnection(_configuration.GetConnectionString("WaterLogger")))
        {
            connection.Open();
            string sql = "SELECT * FROM drinking_water d JOIN measures m ON d.MeasureId = m.Id";

            using var command = new SqliteCommand(sql, connection);

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                data.Add(new DrinkingWater
                {
                    Id = reader.GetInt32(0),
                    Quantity = reader.GetDouble(1),
                    Date = DateTime.Parse(reader.GetString(2)),
                    MeasureId = reader.GetInt32(3),
                    Measure = new Measure
                    {
                        Id = reader.GetInt32(3),
                        Name = reader.GetString(reader.GetOrdinal("Name"))
                    }
                });
            }
        }

        return data;
    }

    public void Delete(int id)
    {
        using (var connection = new SqliteConnection(_configuration.GetConnectionString("WaterLogger")))
        {
            connection.Open();
            string sql = "DELETE FROM drinking_water WHERE Id = @Id";

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@Id", id);

            command.ExecuteNonQuery();
        }
    }

    public DrinkingWater GetById(int id)
    {
        var drinkingWaterRecord = new DrinkingWater();

        using (var connection = new SqliteConnection(_configuration.GetConnectionString("WaterLogger")))
        {
            connection.Open();
            string sql = "SELECT * FROM drinking_water d JOIN measures m ON d.MeasureId = m.Id WHERE d.Id = @Id";

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@Id", id);

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                drinkingWaterRecord.Id = reader.GetInt32(0);
                drinkingWaterRecord.Quantity = reader.GetDouble(1);
                drinkingWaterRecord.Date = DateTime.Parse(reader.GetString(2));
                drinkingWaterRecord.MeasureId = reader.GetInt32(3);
                drinkingWaterRecord.Measure = new Measure
                {
                    Id = reader.GetInt32(3),
                    Name = reader.GetString(reader.GetOrdinal("Name"))
                };
            }
        }

        return drinkingWaterRecord;
    }

    public void Update(DrinkingWater drinkingWater)
    {
        using (var connection = new SqliteConnection(_configuration.GetConnectionString("WaterLogger")))
        {
            connection.Open();
            string sql = @"UPDATE drinking_water
                              SET Date = @Date, Quantity = @Quantity, MeasureId = @MeasureId
                              WHERE Id = @Id
            ";

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@Id", drinkingWater.Id);
            command.Parameters.AddWithValue("@Date", drinkingWater.Date);
            command.Parameters.AddWithValue("@Quantity", drinkingWater.Quantity);
            command.Parameters.AddWithValue("@MeasureId", drinkingWater.MeasureId);

            command.ExecuteNonQuery();
        }
    }
}
