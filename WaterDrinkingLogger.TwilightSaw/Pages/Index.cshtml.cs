using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using WaterDrinkingLogger.TwilightSaw.Models;

namespace SleepingTracker.TwilightSaw.Pages
{
    public class IndexModel(IConfiguration configuration) : PageModel
    {
        public List<string> ButtonNames { get; set; }
        public void OnGet()
        {
            ButtonNames = GetTables();
        }
        private List<string> GetTables()
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = $@"SELECT name FROM sqlite_master 
                                    WHERE type = 'table' AND name != 'sqlite_sequence'
                                    ORDER BY name;";
            var tableName = new List<string>();
            var reader = tableCmd.ExecuteReader();

            while (reader.Read())
            {
                tableName.Add(reader.GetString(0));
            }
            connection.Close();
            return tableName;
        }
    }
}
