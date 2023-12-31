using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVC.WaterLogger.K_MYR.Models;
using System.Data.SQLite;


namespace MVC.WaterLogger.K_MYR.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _configuration;
        [TempData]
        public string? SuccessMessage { get; set; }
        public List<HabitModel> Habits { get; set; } = [];
        public List<string> Icons { get; } =
        [
            "activity.svg",
            "ball-1.svg",
            "beer-mug-1.svg",
            "bicycle-bike.svg",
            "book-reading-3.svg",
            "brain-cognitive.svg",
            "campfire.svg",
            "coffee-mug-2.svg",
            "collaborations-idea-4.svg",
            "curly-brackets.svg",
            "dumbell.svg",
            "fork-knife.svg",
            "incognito-mode-1.svg",
            "office-worker-2.svg",
            "snorkle-1.svg",
            "task-list.svg",
            "water-glass-3.svg",
            "workspace-desk-1.svg"
        ];
       
       
        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<IActionResult> OnGet()
        {
            Habits = (await GetHabits()).Distinct().ToList() ?? [];

            return Page();
        }

        [BindProperty]
        public HabitModel? Habit { get; set; }

        public async Task<IActionResult> OnPostInsertHabit()
        {
            if (!ModelState.IsValid)
            {         
                TempData["ErrorMessage"] = "Habit couldn't be added!";         
                return RedirectToPage();
            }

            try
            {
                var sql = "INSERT INTO Habits (Name, Measurement, Icon) VALUES (@Name, @Measurement, @Icon)";

                using SQLiteConnection connection = new(_configuration.GetConnectionString("ConnectionString"));

                var affectedRows = connection.ExecuteAsync(sql, Habit);

                if(await affectedRows == 1)
                    TempData["SuccessMessage"] = "Habit was added successfully!";
                else                
                    TempData["ErrorMessage"] = "Habit couldn't be added!";            
            }

            catch 
            {
                TempData["ErrorMessage"] = "Habit couldn't be added!";
            }
            
            return RedirectToPage();
                        
        }

        public async Task<IActionResult> OnPostDeleteHabit()
        {
            if(Habit is null || !await HabitWithIdExists(Habit.Id))
            {
                TempData["ErrorMessage"] = "Habit couldn't be deleted!";
                return RedirectToPage();                
            }

            try
            {                                              
                var sql = "DELETE FROM Habits Where Id = @Id";

                using SQLiteConnection connection = new(_configuration.GetConnectionString("ConnectionString"));

                var affectedRows = connection.ExecuteAsync(sql, new { Habit.Id });

                if(await affectedRows == 1)
                    TempData["SuccessMessage"] = "Habit was deleted successfully!";
                else                
                    TempData["ErrorMessage"] = "Habit couldn't be deleted!";            
            }

            catch 
            {
                TempData["ErrorMessage"] = "Habit couldn't be deleted!";
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUpdateHabit()
        {
            if (!ModelState.IsValid || !await HabitWithIdExists(Habit!.Id))
            {
                TempData["ErrorMessage"] = "Habit couldn't be updated!";                                   
                return RedirectToPage();
            } 

            try
            {   
                var sql = "UPDATE Habits SET Name = @Name, Measurement = @Measurement, Icon = @Icon WHERE Id = @Id";

                using SQLiteConnection connection = new(_configuration.GetConnectionString("ConnectionString"));

                var affectedRows = connection.ExecuteAsync(sql, Habit);

                if(await affectedRows == 1)
                    TempData["SuccessMessage"] = "Habit was updated successfully!";
                else                
                    TempData["ErrorMessage"] = "Habit couldn't be updated!";            
            }

            catch 
            {
                TempData["ErrorMessage"] = "Habit couldn't be updated!";
            }

            return RedirectToPage();
        }

        private async Task<IEnumerable<HabitModel>> GetHabits()
        {
            var sql = @"SELECT h.Id, h.Name, h.Measurement, h.Icon, r.Id, r.Date, r.Quantity 
                        FROM Habits h 
                        LEFT JOIN Records r ON h.Id = r.HabitId AND r.Date >= @StartDate";

            var habitDictionary = new Dictionary<int, HabitModel>();

            using SQLiteConnection connection = new(_configuration.GetConnectionString("ConnectionString"));

            return await connection.QueryAsync<HabitModel, RecordModel, HabitModel>(
                sql, (habit, record) =>
                {
                    if (!habitDictionary.TryGetValue(habit.Id, out HabitModel? habitModel))
                    {
                        habitModel = habit;
                        habitDictionary.Add(habit.Id, habitModel);
                    }

                    if (record != null)
                    {
                        habitModel.Records.Add(record);
                    }

                    return habitModel;
                },
                new { StartDate = DateTime.Now.AddDays(-6) });
        }
   
        private async Task<bool> HabitWithIdExists(int id)
        {
             var sql = "SELECT 1 FROM Habits WHERE Id = @id";

            using SQLiteConnection connection = new(_configuration.GetConnectionString("ConnectionString"));

            return (await connection.QuerySingleOrDefaultAsync<HabitModel>(sql, new { id })) is not null;
        }
    }
}
