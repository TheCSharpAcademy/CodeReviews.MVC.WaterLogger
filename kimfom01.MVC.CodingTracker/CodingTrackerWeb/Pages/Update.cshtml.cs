using CodingTrackerWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CodingTrackerWeb.Repositories;

namespace CodingTrackerWeb.Pages
{
    public class UpdateModel : PageModel
    {
        private readonly ICodingHourRepository _repository;

        [BindProperty]
        public CodingHour CodingHour { get; set; }

        public UpdateModel(ICodingHourRepository repository)
        {
            _repository = repository;
        }

        public IActionResult OnGet(int id)
        {
            CodingHour = _repository.GetById(id);

            return Page();
        }

        public IActionResult OnPost(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _repository.UpdateRecord(id, CodingHour);

            return RedirectToPage("./Index");
        }
    }
}
