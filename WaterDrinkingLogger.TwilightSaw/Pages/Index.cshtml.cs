using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

namespace ActionTracker.TwilightSaw.Pages
{
    public class IndexModel(IConfiguration configuration) : PageModel
    {
        public List<(string, int)> Variables { get; set; }
        public void OnGet()
        {
            Variables = GetTables();
        }
        private List<(string, int)> GetTables()
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = $@"SELECT name, ActionsId FROM Actions";
            var name = new List<(string, int)>();
            var reader = tableCmd.ExecuteReader();

            while (reader.Read())
            {
                name.Add((reader.GetString(0), reader.GetInt32(1)));
            }
            connection.Close();
            return name;
        }
    }
}
