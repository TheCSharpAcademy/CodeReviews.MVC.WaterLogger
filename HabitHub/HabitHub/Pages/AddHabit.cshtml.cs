using HabitHub.Data;
using HabitHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

namespace HabitHub.Pages;

public class AddHabitModel : PageModel
{
    [BindProperty] public HabitModel Habit { get; set; }
    [BindProperty] public string HabitToDelete { get; set; }

    public List<string> SavedHabits { get; set; }
    public string HabitWarning { get; set; } = "";

    private readonly IConfiguration _configuration;

    public AddHabitModel(IConfiguration configuration)
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
        {
            // Validate only the Add habit form
            foreach (var state in ModelState)
            {
                if (state.Key == Habit.HabitName.ToString() && state.Value.ValidationState == ModelValidationState.Invalid)
                    return OnGet();
            }
        }

        using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
        {
            connection.Open();

            // Make sure habit doesn't exist yet
            if (String.IsNullOrEmpty(HabitsRepository.CheckHabitExists(connection, Habit)))
            {
                HabitsRepository.AddHabit(connection, Habit);
                HabitWarning = "";
            }
            else
            {
                HabitWarning = "Habit already exists!";
            }
        }
        return OnGet();
    }

    public IActionResult OnPostDelete()
    {
        if (!ModelState.IsValid)
        {
            // Validate only the Delete habit form
            foreach (var state in ModelState)
            {
                if (state.Key == nameof(HabitToDelete) && state.Value.ValidationState == ModelValidationState.Invalid)
                    return OnGet();
            }
        }

        using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
        {
            connection.Open();
            HabitsRepository.DeleteHabit(connection, HabitToDelete);
        }
        return OnGet();
    }
}
