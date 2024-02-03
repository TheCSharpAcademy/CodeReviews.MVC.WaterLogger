using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using PabloRazorPagesTutorial.Models;

namespace PabloRazorPagesTutorial.Pages.WaterDrinkingDir;

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
    public DrinkingWater DrinkingWater { get; set; } = default!;

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        using (var connection = new SqlConnection(_configuration.GetConnectionString("sqlserver")))
        {
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText =
                @$"INSERT INTO DrinkingWater(Quantity,Date)
                    VALUES({DrinkingWater.Quantity},'{DrinkingWater.Date.ToString("yyyy-MM-dd HH:mm:ss.fffffff")}')";
            tableCmd.ExecuteNonQuery();
        }

        return RedirectToPage("./Index");
    }
}
