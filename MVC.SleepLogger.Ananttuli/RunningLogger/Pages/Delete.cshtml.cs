using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RunningLogger.Models;
using RunningLogger.Repositories;

namespace RunningLogger.Pages
{

    public class DeleteModel : PageModel
    {
        private readonly LogsRepository _logsRepo;

        [BindProperty]
        public Log? Log { get; set; }

        public DeleteModel(LogsRepository logsRepository)
        {
            _logsRepo = logsRepository;
        }

        public IActionResult OnGet(int id)
        {
            Log = _logsRepo.GetById(id);

            return Page();
        }

        public IActionResult OnPost()
        {
            if (Log?.LogId == null)
            {
                return Redirect("./Index");
            }

            var success = _logsRepo.DeleteById(Log.LogId);

            return Redirect("./Index");
        }


    }
}
