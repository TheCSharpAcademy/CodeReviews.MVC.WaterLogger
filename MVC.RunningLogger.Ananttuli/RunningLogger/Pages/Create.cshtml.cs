using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RunningLogger.Models;
using RunningLogger.Repositories;
using System.Data;

namespace RunningLogger.Pages
{
    public class CreateModel : PageModel
    {
        private readonly LogsRepository _logsRepo;
        private readonly UnitsRepository _unitsRepo;

        [BindProperty]
        public Log Log { get; set; }


        [BindProperty]
        public List<SelectListItem> UnitSelectItems { get; private set; }

        public CreateModel(LogsRepository logsRepo, UnitsRepository unitsRepo)
        {
            _logsRepo = logsRepo;
            _unitsRepo = unitsRepo;
        }

        private void Initialize()
        {
            var allUnits = _unitsRepo.GetAll();

            UnitSelectItems = allUnits
                .Select(unit =>
                    new SelectListItem { Value = unit.UnitId.ToString(), Text = unit.Name })
                .ToList();

            Log = Log ?? new Log { StartDateTime = DateTime.Now };
            Log.UnitId = allUnits[0].UnitId;
        }

        public IActionResult OnGet()
        {
            Initialize();
            return Page();
        }

        public ActionResult? OnPost()
        {
            if (!ModelState.IsValid)
            {
                Initialize();
                return Page();
            }

            var success = _logsRepo.Create(Log);

            if (!success)
            {
                ModelState.AddModelError("CREATE", "Could not create record");
                Initialize();
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
