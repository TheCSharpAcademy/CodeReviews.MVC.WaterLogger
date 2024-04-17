using Logger.frockett.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using System.Globalization;

namespace Logger.frockett.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration config;

        public List<DailyPushupsModel> Records { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            config = configuration;
        }

        public void OnGet()
        {
            Records = GetAllRecords();
        }

		private List<DailyPushupsModel> GetAllRecords()
		{
            using (var connection = new SqliteConnection(config.GetConnectionString("ConnectionString")))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"SELECT * FROM pushups ORDER BY Date";

                var tableData = new List<DailyPushupsModel>();
                SqliteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    tableData.Add(
                        new DailyPushupsModel
                        {
                            Id = reader.GetInt32(0),
                            Date = DateTime.Parse(reader.GetString(1), CultureInfo.CurrentUICulture.DateTimeFormat),
                            Quantity = reader.GetInt32(2)
                        });
                }
                connection.Close();

				return tableData;
			}
		}
	}
}
