using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using PabloRazorPagesTutorial.Models;

namespace PabloRazorPagesTutorial.Pages.WaterDrinkingDir;

public class IndexModel : PageModel
{
    private readonly IConfiguration _configuration;
    public List<DrinkingWater> DrinkingWater { get; set; } = default!;
    
    
    public IndexModel(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task OnGetAsync()
    {
        DrinkingWater = new();
        using (var connection = new SqlConnection(_configuration.GetConnectionString("sqlserver")))
        {
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText =
                @$"SELECT * FROM DrinkingWater";
            tableCmd.ExecuteNonQuery();

            SqlDataReader reader = tableCmd.ExecuteReader();
            while (reader.Read())
            {
                DrinkingWater obj = new()
                {
                    ID = int.Parse(reader["ID"].ToString()),
                    Quantity = int.Parse(reader["Quantity"].ToString()),
                    Date = DateTime.Parse(reader["Date"].ToString()),
                };
                DrinkingWater.Add(obj);
            }
        }
    }
}
