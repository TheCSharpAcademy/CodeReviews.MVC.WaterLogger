using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using WaterLogger.jollejonas.Models;

namespace WaterLogger.jollejonas.Pages
{
    public class CreateModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public CreateModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Expense Expense { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            using (var connection = new SqliteConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO Expenses (Name, Date, Amount) VALUES (@Name, @Date, @Amount)";
                    command.Parameters.AddWithValue("@Name", Expense.Name);
                    command.Parameters.AddWithValue("@Date", Expense.Date);
                    command.Parameters.AddWithValue("@Amount", Expense.Amount);
                    command.ExecuteNonQuery();
                }
            }
            return RedirectToPage("/Index");
        }
    }
}
