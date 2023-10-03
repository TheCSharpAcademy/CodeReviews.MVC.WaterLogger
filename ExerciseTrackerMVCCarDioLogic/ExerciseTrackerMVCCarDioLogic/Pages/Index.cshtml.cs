using ExerciseTrackerMVCCarDioLogic.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExerciseTrackerMVCCarDioLogic.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<IndexModel> _logger;
        private readonly Context _context;

        public List<ExerciseType> ExerciseTypes { get; set; }

        public IndexModel(ILogger<IndexModel> logger, Context context, IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _configuration = configuration;
        }

        public void OnGet()
        {
            ExerciseTypes = GetAllRecords();
        }

        private List<ExerciseType> GetAllRecords()
        {
            List<ExerciseType> exerciseTypes = _context.ExerciseTypes.ToList();

            return exerciseTypes;
        }
    }
}