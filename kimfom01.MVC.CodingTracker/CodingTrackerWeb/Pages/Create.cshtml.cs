using CodingTrackerWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CodingTrackerWeb.Repositories;

namespace CodingTrackerWeb.Pages
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public CodingHour CodingHour { get; set; }
        private readonly ICodingHourRepository _repository;

        public CreateModel(ICodingHourRepository repository)
        {
            _repository = repository;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _repository.InsertRecord(CodingHour);

            return RedirectToPage("./Index");
        }
    }
}
