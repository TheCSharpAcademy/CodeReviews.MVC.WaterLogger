using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using PabloRazorPagesTutorial.Models;

namespace PabloRazorPagesTutorial.Pages.WaterDrinkingDir;

public class DetailsModel : PageModel
{
    private readonly IConfiguration _configuration;
    public DetailsModel(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public DrinkingWater DrinkingWater { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
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
                @$"SELECT * FROM DrinkingWater WHERE ID = {id}";
            tableCmd.ExecuteNonQuery();

            using (SqlDataReader reader = tableCmd.ExecuteReader())
            {
                if (!reader.Read())
                    return NotFound("Ese Id does not exist!!!");

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
}
