using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVC.WaterLogger.K_MYR.Models;
using System.Data.SQLite;

namespace MVC.WaterLogger.K_MYR.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly ILogger<DetailsModel> _logger;

        private readonly IConfiguration _configuration;

        public HabitModel HabitModel { get; set; }
        public List<RecordModel> Records { get; set; }        

        public DetailsModel(ILogger<DetailsModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            HabitModel = await GetHabit(id);
            Records = (await GetRecords(id)).ToList();

            return Page();
        }

        [BindProperty]
        public RecordModel Record { get; set; }

        public async Task<IActionResult> OnPostInsertRecord()
        {
            var sql = "INSERT INTO Records (Date, Quantity, HabitId) VALUES (@Date, @Quantity, @HabitId)";

            using SQLiteConnection connection = new(_configuration.GetConnectionString("ConnectionString"));

            await connection.ExecuteAsync(sql, Record);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteRecord()
        {
            var sql = "DELETE FROM Records WHERE Id = @Id";

            using SQLiteConnection connection = new (_configuration.GetConnectionString("ConnectionString"));

            await connection.ExecuteAsync(sql, Record);

            return RedirectToPage();
        }

        private async Task<HabitModel> GetHabit(int id)
        {
            var sql = "SELECT * FROM Habits WHERE Id = @id";

            using SQLiteConnection connection = new(_configuration.GetConnectionString("ConnectionString"));

            return await connection.QuerySingleAsync<HabitModel>(sql, new { id });
        }

        private async Task<IEnumerable<RecordModel>> GetRecords(int id)
        {
            var sql = "SELECT * FROM Records WHERE HabitId = @id";

            using SQLiteConnection connection = new(_configuration.GetConnectionString("ConnectionString"));

            return await connection.QueryAsync<RecordModel>(sql, new { id });
        }
    }
}
    

