using Logger.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using System.Globalization;

namespace Logger.Pages
{
    public class UpdateModel : PageModel
    {
		private readonly IConfiguration _configuration;
		[BindProperty]
		public RideModel Rides { get; set; }
		public UpdateModel(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public IActionResult OnGet(int id)
        {
			Rides = GetRideById(id);
			return Page();
		}

		public IActionResult OnPost(int id) 
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
			{
				connection.Open();
				var tableCmd = connection.CreateCommand();
				tableCmd.CommandText = 
					"UPDATE bikerides SET Date = @date, Distance = @distance, Duration = @duration WHERE Id = @id";
				tableCmd.Parameters.AddWithValue("@id", id);
				tableCmd.Parameters.AddWithValue("@date", Rides.Date);
				tableCmd.Parameters.AddWithValue("@distance", Rides.Distance);
				tableCmd.Parameters.AddWithValue("@duration", Rides.Duration);
				tableCmd.ExecuteNonQuery();
			}
			return RedirectToPage("./Index");
		}
		private RideModel GetRideById(int id)
		{
			var ride = new RideModel();
			using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
			{
				connection.Open();
				var tableCmd = connection.CreateCommand();
				tableCmd.CommandText = "SELECT * FROM bikerides WHERE Id = @id";
				tableCmd.Parameters.AddWithValue("@id", id);
				SqliteDataReader reader = tableCmd.ExecuteReader();
				while (reader.Read())
				{
					ride.Id = reader.GetInt32(0);
					ride.Date = DateTime.Parse(reader.GetString(1),
							CultureInfo.CurrentUICulture.DateTimeFormat);
					ride.Distance = reader.GetDouble(2);
					ride.Duration = reader.GetTimeSpan(3);
				}
				return ride;
			}
		}
	}
}
