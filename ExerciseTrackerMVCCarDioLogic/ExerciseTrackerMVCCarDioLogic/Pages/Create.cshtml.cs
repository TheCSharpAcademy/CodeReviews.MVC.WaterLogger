using ExerciseTrackerMVCCarDioLogic.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExerciseTrackerMVCCarDioLogic.Pages
{
    public class Create : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly Context _context;

        [BindProperty]
        public int ExerciseTypeId { get; set; }
        public ExerciseType ExerciseType { get; set; }
        public Create(IConfiguration configuration, Context context)
        {
            _configuration = configuration;
            _context = context;
        }
        public IActionResult OnGet(int id)
        {
            ExerciseTypeId = id;
            ExerciseType = GetById(ExerciseTypeId);
            return Page();
        }

        private ExerciseType GetById(int id)
        {
            return _context.ExerciseTypes.Where(s => s.ExerciseTypeId == id).FirstOrDefault();
        }

        [BindProperty]
        public ExerciseSession MyExerciseSession { get; set; }

        public IActionResult OnPost()
        {
            ExerciseType = GetById(ExerciseTypeId);

            ExerciseSession sessionToCreate = new ExerciseSession
            {
                Weigth = MyExerciseSession.Weigth,
                DurationInMinutes = MyExerciseSession.DurationInMinutes,
                Date = MyExerciseSession.Date,
                ExerciseTypeName = ExerciseType.ExerciseTypeName,
                exerciseType = ExerciseType
            };


            _context.Add(sessionToCreate);
            _context.SaveChanges();

            return RedirectToPage("./Index");
        }
    }
}
