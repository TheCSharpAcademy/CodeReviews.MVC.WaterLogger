using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace ApplyingTracker.Pages;

public class CreateModel : PageModel
{
    private readonly IApplicationsService _applicationsService;
    [BindProperty] public Application Application { get; set; }

    public CreateModel(IApplicationsService applicationsService)
    {
        _applicationsService = applicationsService;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        await _applicationsService.AddApplication(Application);
        return Redirect("/");
    }
}