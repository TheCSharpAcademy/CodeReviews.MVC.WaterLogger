using ExerciseTrackerMVCCarDioLogic.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExerciseTrackerMVCCarDioLogic.Pages
{
    public class DeleteExerciseTypeModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly Context _context;

        [BindProperty]
        public ExerciseType ExerciseType { get; set; }

        public DeleteExerciseTypeModel(IConfiguration configuration, Context context)
        {
            _configuration = configuration;
            _context = context;
        }
        public IActionResult OnGet(int id)
        {
            ExerciseType = GetById(id);
            return Page();
        }

        private ExerciseType GetById(int id)
        {
            return _context.ExerciseTypes.Where(s => s.ExerciseTypeId == id).FirstOrDefault(); ;
        }

        public IActionResult OnPost(int id)
        {
            ExerciseType = GetById(id);
            _context.Remove(ExerciseType);
            _context.SaveChanges();

            return RedirectToPage("./Index");
        }
    }
}
