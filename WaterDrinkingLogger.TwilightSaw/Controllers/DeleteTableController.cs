using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace SleepingTracker.TwilightSaw.Controllers
{
   public class DeleteTableController(IConfiguration configuration) : Controller
    {
        [HttpPost]
        [Route("DeleteTable")]
        public IActionResult DeleteTable(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest("Table name is required");

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"DROP TABLE IF EXISTS [{name}]";
                command.ExecuteNonQuery();
            }

            return RedirectToPage("/Index");
        }
    }
}
