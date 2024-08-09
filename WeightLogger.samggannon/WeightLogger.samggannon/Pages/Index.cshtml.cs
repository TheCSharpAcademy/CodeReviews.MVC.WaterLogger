using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeightLogger.samggannon.Data;

namespace WeightLogger.samggannon.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly DataAccess _dataFunctions;

        public string ConnectionStatus { get; set; }

        public IndexModel(ILogger<IndexModel> logger, DataAccess dataAccess)
        {
            _logger = logger;
            _dataFunctions = dataAccess;
        }

        public void OnGet()
        {
            ConnectionStatus = _dataFunctions.TestConnection() ? "Successful" : "Failed";
        }
    }
}
