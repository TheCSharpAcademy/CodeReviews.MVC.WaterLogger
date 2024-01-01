using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVC.WaterLogger.K_MYR.Models;
using System.Data.SQLite;

namespace MVC.WaterLogger.K_MYR.Pages
{
    public class DetailsModel : PageModel
    {       
        private readonly IConfiguration _configuration;

        public HabitModel? HabitModel { get; set; }
        public List<RecordModel> Records { get; set; } = [];     

        public DetailsModel(ILogger<DetailsModel> logger, IConfiguration configuration)
        {            
            _configuration = configuration;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            HabitModel = await GetHabit(id);

            if(HabitModel is null)
            {
                TempData["ErrorMessage"] = "Habit couldn't be found!";
                return RedirectToPage("Index");
            }

            Records = [.. (await GetRecords(id)).OrderByDescending(x => x.Date)];

            return Page();
        }

        [BindProperty]
        public RecordModel? Record { get; set; }

        public async Task<IActionResult> OnPostInsertRecord()
        {
            if (!ModelState.IsValid)
            {         
                TempData["ErrorMessage"] = "Record couldn't be added!";         
                return RedirectToPage();
            }

            try
            {
                var sql = "INSERT INTO Records (Date, Quantity, HabitId) VALUES (@Date, @Quantity, @HabitId)";

                using SQLiteConnection connection = new(_configuration.GetConnectionString("ConnectionString"));

                var affectedRows = connection.ExecuteAsync(sql, Record);            

                if(await affectedRows == 1)
                        TempData["SuccessMessage"] = "Record was added successfully!";
                    else                
                        TempData["ErrorMessage"] = "Record couldn't be added!";            
            }

            catch 
            {
                TempData["ErrorMessage"] = "An error occurred while attempting to insert data into the database!";
            }
            
            return RedirectToPage();
        }

         public async Task<IActionResult> OnPostDeleteRecord()
        {
            if (!ModelState.IsValid)
            {         
                TempData["ErrorMessage"] = "Record couldn't be added!";         
                return RedirectToPage();
            }

            try
            {
                var sql = "DELETE FROM Records WHERE Id = @Id";

                using SQLiteConnection connection = new (_configuration.GetConnectionString("ConnectionString"));

                var affectedRows = connection.ExecuteAsync(sql, Record);            

                if(await affectedRows == 1)
                        TempData["SuccessMessage"] = "Record was deleted successfully!";
                    else                
                        TempData["ErrorMessage"] = "Record couldn't be deleted!";            
            }

            catch 
            {
                TempData["ErrorMessage"] = "An error occurred while attempting to delete data from the database!";
            }
            
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUpdateRecord()
        {       
            if (!ModelState.IsValid)
            {         
                TempData["ErrorMessage"] = "Record couldn't be updated!";         
                return RedirectToPage();
            }

            try
            {  
                var sql = "UPDATE Records SET Date = @Date, Quantity = @Quantity WHERE Id = @Id";

                using SQLiteConnection connection = new (_configuration.GetConnectionString("ConnectionString"));

                var affectedRows = connection.ExecuteAsync(sql, Record);

                if(await affectedRows == 1)
                            TempData["SuccessMessage"] = "Record was updated successfully!";
                        else                
                            TempData["ErrorMessage"] = "Record couldn't be updated!";  

                return RedirectToPage();
            }

            catch 
            {
                TempData["ErrorMessage"] = "An error occurred while attempting to insert data into the database!";
            }
            
            return RedirectToPage();
        }

        private async Task<HabitModel?> GetHabit(int id)
        {
            try
            {
                  var sql = "SELECT * FROM Habits WHERE Id = @id";

                using SQLiteConnection connection = new(_configuration.GetConnectionString("ConnectionString"));

                return await connection.QuerySingleOrDefaultAsync<HabitModel>(sql, new { id });
            }

            catch
            {
                TempData["ErrorMessage"] = "An error occurred while attempting to retrieve data from the database!";
                return null;
            }          
        }

        private async Task<IEnumerable<RecordModel>> GetRecords(int id)
        {
            try
            {
                var sql = "SELECT * FROM Records WHERE HabitId = @id";

                using SQLiteConnection connection = new(_configuration.GetConnectionString("ConnectionString"));

                return await connection.QueryAsync<RecordModel>(sql, new { id });
            }

            catch 
            {
                TempData["ErrorMessage"] = "An error occurred while attempting to retrieve data from the database!";
                return [];
            }
        }
    }
}
    

