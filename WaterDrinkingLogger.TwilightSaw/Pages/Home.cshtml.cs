using System.Globalization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using WaterDrinkingLogger.TwilightSaw.Models;

namespace WaterDrinkingLogger.TwilightSaw.Pages
{
    public class HomeModel(IConfiguration configuration) : PageModel
    {
        public List<Record> Records { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }

        public void OnGet(int id, string name)
        {
            Id = id;
            Name = name;
            Records = GetAllRecords(Id);
        }

        public List<Record> GetAllRecords(int id)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            var tableCmd = connection.CreateCommand();

            tableCmd.CommandText = $"SELECT * FROM 'Records' WHERE ActionsId = '{id}'";
            var recordData = new List<Record>();
            var reader = tableCmd.ExecuteReader();

            while (reader.Read())
            {
                recordData.Add(new Record()
                {
                    Id=reader.GetInt32(0),
                    Date = DateTime.Parse(reader.GetString(1), CultureInfo.CurrentUICulture.DateTimeFormat),
                    Quantity = reader.GetDouble(2),
                    Measurement = reader.GetString(3),
                    ActionsId = reader.GetInt32(4),
                });
            }

            return recordData;
        }
    }
}
