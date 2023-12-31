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

        public List<HabitModel> Habits { get; set; }
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
            Habits = (await GetAllRecords()).Distinct().ToList();
            
            return Page();
        }        

        [BindProperty]
        public HabitModel Habit { get; set; }       

        public async Task<IActionResult> OnPostInsertHabit()
        {
            var sql = "INSERT INTO Habits (Name, Measurement, Icon) VALUES (@Name, @Measurement, @Icon)";

            using SQLiteConnection connection = new(_configuration.GetConnectionString("ConnectionString"));

            await connection.ExecuteAsync(sql, Habit);

            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnPostDeleteHabit() 
        {
            var sql = "DELETE FROM Habits Where Id = @Id";

            using SQLiteConnection connection = new(_configuration.GetConnectionString("ConnectionString"));

            await connection.ExecuteAsync(sql, new { Habit.Id});           

            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnPostUpdateHabit()
        {
            var sql = "UPDATE Habits SET Name = @Name, Measurement = @Measurement, Icon = @Icon WHERE Id = @Id";

            using SQLiteConnection connection = new(_configuration.GetConnectionString("ConnectionString"));

            await connection.ExecuteAsync(sql, Habit);

            return RedirectToPage("Index");
        }

        private async Task<IEnumerable<HabitModel>> GetAllRecords()
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
    }
}
