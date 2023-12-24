using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WaterLogger.Speedierone.Model;

namespace WaterLogger.Speedierone.Pages
{
    public class CreateModel : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public MovieLoggerModel MovieLogger { get; set; }
    }
}
