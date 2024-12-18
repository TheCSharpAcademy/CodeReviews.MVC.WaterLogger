using Microsoft.Data.Sqlite;
using WaterLogger.Dejmenek.Models;

namespace WaterLogger.Dejmenek.Repositories;

public class MeasureRepository : IMeasureRepository
{
    private readonly IConfiguration _configuration;

    public MeasureRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public List<Measure> GetAllMeasures()
    {
        List<Measure> measures = new List<Measure>();

        using (var connection = new SqliteConnection(_configuration.GetConnectionString("WaterLogger")))
        {
            connection.Open();
            string sql = "SELECT * FROM measures";

            using var command = new SqliteCommand(sql, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                measures.Add(new Measure
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                });
            }
        }

        return measures;
    }
}
