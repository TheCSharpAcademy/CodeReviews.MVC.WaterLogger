using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using WaterLogger.jollejonas.Models;

namespace WaterLogger.jollejonas.Pages;

public class UpdateModel : PageModel
{
    private readonly IConfiguration _configuration;
    [BindProperty]
    public Expense Expense { get; set; }
    public UpdateModel(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IActionResult OnGet(int id)
    {
        Expense = GetByID(id);
        return Page();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        UpdateRecord(Expense);

        return RedirectToPage("/Index");
    }

    private void UpdateRecord(Expense expense)
    {
        using (var connection = new SqliteConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE Expenses SET Name = @Name, Date = @Date, Amount = @Amount WHERE Id = @Id";
                command.Parameters.AddWithValue("@Name", expense.Name);
                command.Parameters.AddWithValue("@Date", expense.Date);
                command.Parameters.AddWithValue("@Amount", expense.Amount);
                command.Parameters.AddWithValue("@Id", expense.Id);
                command.ExecuteNonQuery();
            }
        }
    }

    private Expense GetByID(int id)
    {
        using (var connection = new SqliteConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT Id, Name, Date, Amount FROM Expenses WHERE Id = @Id";
                command.Parameters.AddWithValue("@Id", id);
                using (var reader = command.ExecuteReader())
                {
                    reader.Read();
                    if (!reader.HasRows)
                    {
                        return null;
                    }
                    return new Expense
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Date = reader.GetDateTime(2),
                        Amount = reader.GetDecimal(3)
                    };
                }
            }
        }
    }
}
