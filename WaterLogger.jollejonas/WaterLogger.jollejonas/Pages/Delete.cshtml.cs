using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using WaterLogger.jollejonas.Models;

namespace WaterLogger.jollejonas.Pages;

public class DeleteModel : PageModel
{
    private readonly IConfiguration _configuration;
    [BindProperty]
    public Expense Expenses { get; set; }

    public DeleteModel(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IActionResult OnGet(int id)
    {
        Expenses = GetByID(id);

        return Page();
    }

    public IActionResult OnPost()
    {
        DeleteRecord(Expenses.Id);
        return RedirectToPage("/Index");
    }

    private void DeleteRecord(int id)
    {
        using (var connection = new SqliteConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM Expenses WHERE Id = @Id";
                command.Parameters.AddWithValue("@Id", id);
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
