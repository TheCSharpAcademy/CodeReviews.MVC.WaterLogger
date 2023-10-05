using ExerciseTrackerMVCCarDioLogic.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExerciseTrackerMVCCarDioLogic.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly Context _context;

        [BindProperty]
        public ExerciseSession Session { get; set; }

        public DeleteModel(IConfiguration configuration, Context context)
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
            return _context.Sessions.Where(s => s.Id == id).FirstOrDefault(); ;
        }

        public IActionResult OnPost(int id)
        {
            Session = GetById(id);
            _context.Remove(Session);
            _context.SaveChanges();

            return RedirectToPage("./Index");
        }
    }
}
