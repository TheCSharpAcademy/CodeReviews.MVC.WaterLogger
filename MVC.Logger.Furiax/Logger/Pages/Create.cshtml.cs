using Logger.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using System.ComponentModel.DataAnnotations;

namespace Logger.Pages
{
	public class CreateModel : PageModel
	{
		private readonly IConfiguration _configuration;
		public CreateModel(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		[BindProperty]
		public RideModel Ride { get; set; }
		[BindProperty]
		[Range(0, int.MaxValue)]
		public int Hours { get; set; }
		[BindProperty]
		[Range(0, 59)]
		public int Minutes { get; set; }

		public IActionResult OnGet()
		{
			return Page();
		}

		public IActionResult OnPost()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			TimeSpan duration = new TimeSpan(Hours, Minutes, 0);

			using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
			{
				connection.Open();
				var tableCmd = connection.CreateCommand();
				tableCmd.CommandText =
					"INSERT INTO bikerides (date, distance, duration) VALUES (@date, @distance, @duration)";
				tableCmd.Parameters.AddWithValue("@date", Ride.Date);
				tableCmd.Parameters.AddWithValue("@distance", Ride.Distance);
				tableCmd.Parameters.AddWithValue("@duration", duration.ToString());
				tableCmd.ExecuteNonQuery();
			}
			return RedirectToPage("./Index");
		}




	}
}
