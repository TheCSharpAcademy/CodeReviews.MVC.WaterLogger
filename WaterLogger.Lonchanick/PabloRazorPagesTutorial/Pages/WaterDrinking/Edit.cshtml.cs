using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PabloRazorPagesTutorial.Models;
using System.Collections.Generic;

namespace PabloRazorPagesTutorial.Pages.WaterDrinkingDir;

public class EditModel : PageModel
{
    private readonly IConfiguration _configuration;
    public EditModel(IConfiguration configuration)
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

        DrinkingWater = new();
        using (var connection = new SqlConnection(_configuration.GetConnectionString("sqlserver")))
        {
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText =
                @$"SELECT * FROM DrinkingWater WHERE ID = {id}";
            tableCmd.ExecuteNonQuery();

            using (SqlDataReader reader = tableCmd.ExecuteReader())
            {
                if (!reader.Read())
                    return NotFound("Id does not exist!!!");

                DrinkingWater obj = new()
                {
                    ID = int.Parse(reader["ID"].ToString()),
                    Quantity = int.Parse(reader["Quantity"].ToString()),
                    Date = DateTime.Parse(reader["Date"].ToString()),
                };
                DrinkingWater = obj;

            }
        }

        return Page();
    }

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
                @$"UPDATE DrinkingWater SET Quantity = {DrinkingWater.Quantity}, Date = '{DrinkingWater.Date.ToString("yyyy-MM-dd HH:mm:ss.fffffff")}' WHERE ID={DrinkingWater.ID};";
            tableCmd.ExecuteNonQuery();
        }

        return RedirectToPage("./Index");

    }
}
