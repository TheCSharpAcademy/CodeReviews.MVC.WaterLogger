using HabitHub.Data;
using HabitHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

namespace HabitHub.Pages;

public class ViewHabitsModel : PageModel
{
    [BindProperty] public List<HabitModel> Habits { get; set; }
    [BindProperty] public List<HabitRecordModel> HabitRecords { get; set; }
    [BindProperty] public int RecordToEdit { get; set; }
    [BindProperty] public string HabitToUpdate { get; set; }
    [BindProperty] public HabitRecordModel RecordToUpdate { get; set; }
    [BindProperty] public int RecordToDelete { get; set; }
    [BindProperty] public string HabitToFilterBy { get; set; }
    [BindProperty] public DateTime StartDateToFilterBy { get; set; }
    [BindProperty] public DateTime EndDateToFilterBy { get; set; }
    [BindProperty] public bool OrderByHabit { get; set; }
    [BindProperty] public bool OrderByDate { get; set; }
    [BindProperty] public bool SortAscending { get; set; }

    public List<string> SavedHabits { get; set; }
    public int RecordsTableRowCounter { get; set; } = 0;
    public bool OrderingApplied { get; set; } = false;

    private readonly IConfiguration _configuration;

    public ViewHabitsModel(IConfiguration configuration)
    {
        _configuration = configuration;
        Habits = new List<HabitModel>();
        HabitRecords = new List<HabitRecordModel>();
        SavedHabits = new List<string>();
    }

    public IActionResult OnGet()
    {
        using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
        {
            connection.Open();
            HabitsRepository.GetAllHabits(connection, Habits);
            HabitsRepository.GetAllHabitRecords(connection, HabitRecords);
            HabitsRepository.GetAllHabitNames(connection, SavedHabits);
        }
        return Page();
    }

    public IActionResult OnPostEdit()
    {
        using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
        {
            connection.Open();
            int habitIndex = HabitsRepository.GetHabitId(connection, HabitToUpdate);

            // Make sure habit exists
            if (habitIndex != -1)
                HabitsRepository.UpdateRecord(connection, habitIndex, RecordToUpdate);
        }
        return OnGet();
    }

    public IActionResult OnPostDelete()
    {
        using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
        {
            connection.Open();
            HabitsRepository.DeleteRecord(connection, RecordToDelete);
        }
        return OnGet();
    }

    public IActionResult OnPostFilter()
    {
        // Make sure any criteria were chosen
        if (String.IsNullOrEmpty(HabitToFilterBy) && StartDateToFilterBy.Year == 1 && EndDateToFilterBy.Year == 1)
            return OnGet();

        using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
        {
            connection.Open();

            HabitsRepository.GetAllHabits(connection, Habits);
            HabitsRepository.GetAllHabitRecords(connection, HabitRecords);
            HabitsRepository.GetAllHabitNames(connection, SavedHabits);

            // Filter by habit
            if (!String.IsNullOrEmpty(HabitToFilterBy))
            {
                int habitId = HabitsRepository.GetHabitId(connection, HabitToFilterBy);
                HabitRecords = HabitRecords.Where(x => x.HabitsId == habitId).ToList();
            }

            // Filter by starting date (inclusive)
            if (StartDateToFilterBy.Year != 1)
                HabitRecords = HabitRecords.Where(x => x.Date.CompareTo(StartDateToFilterBy) >= 0).ToList();

            // Filter by ending date (inclusive)
            if (EndDateToFilterBy.Year != 1)
                HabitRecords = HabitRecords.Where(x => x.Date.CompareTo(EndDateToFilterBy) <= 0).ToList();
        }
        return Page();
    }

    /// <summary>
    /// Returns the name of the habit corresponding to the record given.
    /// </summary>
    /// <param name="habitRecord"></param>
    /// <returns></returns>
    public string GetHabitName(HabitRecordModel habitRecord)
    {
        var habit = Habits.FirstOrDefault(x => x.Id == habitRecord.HabitsId);

        if (habit != null)
            return habit.HabitName.ToString();
        else
            return "";
    }
}
