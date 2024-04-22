using HabitHub.Data;
using HabitHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

namespace HabitHub.Pages;

public class AddRecordModel : PageModel
{
    [BindProperty] public HabitRecordModel HabitRecord { get; set; }
    [BindProperty] public string HabitToRecord { get; set; }

    public List<string> SavedHabits { get; set; }

    private readonly IConfiguration _configuration;

    public AddRecordModel(IConfiguration configuration)
    {
        _configuration = configuration;
        SavedHabits = new List<string>();
    }

    public IActionResult OnGet()
    {
        using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
        {
            connection.Open();
            HabitsRepository.GetAllHabitNames(connection, SavedHabits);
        }
        return Page();
    }

    public IActionResult OnPostAdd()
    {
        if (!ModelState.IsValid)
            return OnGet();

        using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
        {
            connection.Open();
            HabitsRepository.AddHabitRecord(connection, HabitToRecord, HabitRecord);
        }
        return OnGet();
    }
}
