using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace SleepingTracker.TwilightSaw.Controllers
{
   public class TableController(IConfiguration configuration) : Controller
    {
        [HttpPost]
        [Route("DeleteAction")]
        public IActionResult DeleteTable(int id)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = $"DELETE FROM Records WHERE ActionsId = {id}";
            command.ExecuteNonQuery();
            command.CommandText = $"DELETE FROM Actions WHERE ActionsId = {id}";
            command.ExecuteNonQuery();

            return RedirectToPage("/Index");
        }

        [HttpPost]
        [Route("Delete")]
        public IActionResult Delete(string name, int recordId, int id)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = $"DELETE FROM Records WHERE Id = {recordId}";
            tableCmd.ExecuteNonQuery();

            return RedirectToPage("/Home", new { name, id });
        }
    }
}
