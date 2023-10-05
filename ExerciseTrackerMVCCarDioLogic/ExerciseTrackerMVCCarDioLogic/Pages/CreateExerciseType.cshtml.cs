using ExerciseTrackerMVCCarDioLogic.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExerciseTrackerMVCCarDioLogic.Pages
{
    public class CreateExerciseTypeModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly Context _context;
        public CreateExerciseTypeModel(IConfiguration configuration, Context context)
        {
            _configuration = configuration;
            _context = context;
        }
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ExerciseType ExerciseType { get; set; }

        public IActionResult OnPost()
        {

            _context.Add(ExerciseType);
            _context.SaveChanges();

            return RedirectToPage("./Index");
        }
    }
}
