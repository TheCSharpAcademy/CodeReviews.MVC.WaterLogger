using ExerciseTrackerMVCCarDioLogic.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExerciseTrackerMVCCarDioLogic.Pages
{
    public class ExerciseSessionsPageModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly Context _context;

        public List<ExerciseSession> Sessions { get; set; }

        public ExerciseType ExerciseType { get; set; }

        public ExerciseSessionsPageModel( Context context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public void OnGet(int id)
        {
            ExerciseType = GetById(id);
            Sessions = GetRecords(ExerciseType.ExerciseTypeName);
        }

        private ExerciseType GetById(int id)
        {
            return _context.ExerciseTypes.Where(s => s.ExerciseTypeId == id).FirstOrDefault();
        }

        private List<ExerciseSession> GetRecords(string exerciseTypeName)
        {
            List<ExerciseSession> sessions = _context.Sessions.Where(r => r.ExerciseTypeName == exerciseTypeName).ToList();

            return sessions;
        }
    }
}
