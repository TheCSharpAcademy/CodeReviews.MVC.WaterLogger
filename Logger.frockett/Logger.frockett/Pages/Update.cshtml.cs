using Logger.frockett.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using System.Globalization;

namespace Logger.frockett.Pages;

public class UpdateModel : PageModel
{

	private readonly IConfiguration config;

	[BindProperty]
	public DrinkingWaterModel DrinkingWater { get; set; }

	public UpdateModel(IConfiguration config)
	{
		this.config = config;
	}

	public IActionResult OnGet(int id)
    {
		DrinkingWater = GetById(id);
		return Page();
	}

	private DrinkingWaterModel GetById(int id)
	{
		var drinkingWaterRecord = new DrinkingWaterModel();

		using (var connection = new SqliteConnection(config.GetConnectionString("ConnectionString")))
		{
			connection.Open();
			var cmd = connection.CreateCommand();
			cmd.CommandText = $"SELECT * FROM drinking_water WHERE Id = {id}";

			SqliteDataReader reader = cmd.ExecuteReader();

			while (reader.Read())
			{
				drinkingWaterRecord.Id = reader.GetInt32(0);
				drinkingWaterRecord.Date = DateTime.Parse(reader.GetString(1), CultureInfo.CurrentUICulture.DateTimeFormat);
				drinkingWaterRecord.Quantity = reader.GetInt32(2);
			}
			connection.Close();
			return drinkingWaterRecord;
		}
	}

	public IActionResult OnPost()
	{
		if (!ModelState.IsValid)
		{
			return Page();
		}

		using (var connection = new SqliteConnection(config.GetConnectionString("ConnectionString")))
		{
			connection.Open();
			var tableCmd = connection.CreateCommand();
			tableCmd.CommandText = @$"UPDATE drinking_water SET date ='{DrinkingWater.Date}',
									quantity = {DrinkingWater.Quantity} 
									WHERE Id = {DrinkingWater.Id}";
			tableCmd.ExecuteNonQuery();
			connection.Close();
		}

		return RedirectToPage("./Index");
	}
}
