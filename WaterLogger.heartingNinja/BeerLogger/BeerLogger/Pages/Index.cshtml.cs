using BeerLogger.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System.Globalization;

namespace BeerLogger.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public List<DrinkingBeerModel> Records { get; set; }

        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }  

        public void OnGet()
        {
            Records = GetAllRecords();
        }

        private List<DrinkingBeerModel> GetAllRecords()
        {
            using (var connection = new
                SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                    $"SELECT * FROM drinking_beer ";

                var tableData = new List<DrinkingBeerModel>();
                SqliteDataReader reader = tableCmd.ExecuteReader();

                while (reader.Read())
                {
                    tableData.Add(
                    new DrinkingBeerModel
                    {
                        Id = reader.GetInt32(0),
                        Date = DateTime.Parse(reader.GetString(1),
                        CultureInfo.CurrentUICulture.DateTimeFormat),
                        Quantity = reader.GetFloat(2)
                    }); ;
                }

                return tableData;
            }
        }
    }
}