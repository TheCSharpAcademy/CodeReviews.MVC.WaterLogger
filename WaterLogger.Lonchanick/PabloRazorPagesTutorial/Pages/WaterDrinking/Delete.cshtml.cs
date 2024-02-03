using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using PabloRazorPagesTutorial.Models;

namespace PabloRazorPagesTutorial.Pages.WaterDrinkingDir;

public class DeleteModel : PageModel
{
    private readonly IConfiguration _configuration;
    public DeleteModel(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [BindProperty]
    public DrinkingWater DrinkingWater { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        using (var connection = new SqlConnection(_configuration.GetConnectionString("sqlserver")))
        {
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText =
                @$"DELETE FROM DrinkingWater WHERE ID = {id}";
            tableCmd.ExecuteNonQuery();
        }

        return RedirectToPage("./Index");
    }
}
