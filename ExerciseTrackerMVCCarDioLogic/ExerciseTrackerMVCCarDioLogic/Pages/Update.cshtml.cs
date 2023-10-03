using ExerciseTrackerMVCCarDioLogic.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExerciseTrackerMVCCarDioLogic.Pages
{
    public class UpdateModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly Context _context;

        [BindProperty]
        public ExerciseSession Session { get; set; }

        public UpdateModel(IConfiguration configuration, Context context)
        {
            _configuration = configuration;
            _context = context;
        }

        public IActionResult OnGet(int id)
        {
            Session = GetById(id);
            return Page();
        }

        private ExerciseSession GetById(int id)
        {
            return _context.Sessions.Where(s => s.Id == id).FirstOrDefault();
        }

        public IActionResult OnPost(int id)
        {
            string execiseTypeName = Session.ExerciseTypeName;
            int sessionId = Session.Id;
            ExerciseSession sessionToUpdate = new ExerciseSession
            {
                Id = sessionId,
                Date = Session.Date,
                ExerciseTypeName = execiseTypeName,
                DurationInMinutes = Session.DurationInMinutes,
                Weigth = Session.Weigth,
            };

            _context.Update(sessionToUpdate);
            _context.SaveChanges();

            return RedirectToPage("./Index");
        }
    }
}
