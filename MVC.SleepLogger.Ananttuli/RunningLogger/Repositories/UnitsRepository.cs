using System.Data;
using Microsoft.Data.Sqlite;
using RunningLogger.Models;

namespace RunningLogger.Repositories;

public class UnitsRepository
{
    private readonly IConfiguration _config;

    public UnitsRepository(IConfiguration config)
    {
        _config = config;
    }

    public List<Unit> GetAll()
    {
        using (var connection = new SqliteConnection(_config.GetConnectionString("ConnectionString")))
        {
            connection.Open();

            var command = connection.CreateCommand();

            command.CommandText =
                $@"
                Select * from Units;
                ";

            var reader = command.ExecuteReader();

            var list = ExtractUnitFromReader(reader);

            connection.Close();

            return list;
        }
    }

    public Unit? GetUnitById(int unitId)
    {

        using (var connection = new SqliteConnection(_config.GetConnectionString("ConnectionString")))
        {
            connection.Open();

            var command = connection.CreateCommand();

            command.CommandText =
                $@"
                Select * from Units
                WHERE UnitId = @UnitId;
                ";

            command.Parameters.AddWithValue("UnitId", unitId);

            var reader = command.ExecuteReader();

            var list = ExtractUnitFromReader(reader);

            connection.Close();

            return list?[0] ?? null;
        }
    }


    private List<Unit> ExtractUnitFromReader(SqliteDataReader reader)
    {
        var list = new List<Unit>();

        while (reader.Read())
        {
            list.Add(new Unit
            {
                UnitId = reader.GetFieldValue<int>("UnitId"),
                Name = reader.GetFieldValue<string>("Name")
            });
        }

        return list;
    }
}

