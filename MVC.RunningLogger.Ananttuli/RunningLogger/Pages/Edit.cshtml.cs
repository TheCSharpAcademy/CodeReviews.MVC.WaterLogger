using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RunningLogger.Models;
using RunningLogger.Repositories;
using System.Data;

namespace RunningLogger.Pages
{
    public class EditModel : PageModel
    {
        private readonly LogsRepository _logsRepo;
        private readonly UnitsRepository _unitsRepo;

        [BindProperty]
        public Log Log { get; set; }

        [BindProperty]
        public List<SelectListItem> UnitSelectItems { get; set; }

        public EditModel(IConfiguration config, LogsRepository logsRepo, UnitsRepository unitsRepo)
        {
            _logsRepo = logsRepo;
            _unitsRepo = unitsRepo;
        }

        public bool Initialize(int id)
        {
            var log = _logsRepo.GetById(id);
            var allUnits = _unitsRepo.GetAll();

            if (log == null || allUnits.Count < 1)
            {
                ModelState.AddModelError("GET", "Could not load data");
                return false;
            }

            Log = log;

            UnitSelectItems = allUnits
                .Select(unit =>
                    new SelectListItem { Value = unit.UnitId.ToString(), Text = unit.Name })
                .ToList();
            return true;
        }

        public IActionResult OnGet(int id)
        {
            Initialize(id);

            return Page();
        }

        public IActionResult OnPost()
        {
            if (Log?.LogId == null)
            {
                return Redirect("./Index");
            }

            if (!ModelState.IsValid)
            {
                Initialize(Log.LogId);
                return Page();
            }

            var success = _logsRepo.Update(Log);

            if (!success)
            {
                ModelState.AddModelError("EDIT", "Could not update record");
                Initialize(Log.LogId);
                return Page();
            }

            return Redirect("./Index");
        }
    }
}
