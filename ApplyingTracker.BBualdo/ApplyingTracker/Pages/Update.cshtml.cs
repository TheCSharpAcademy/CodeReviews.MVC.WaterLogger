using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace ApplyingTracker.Pages;

public class UpdateModel : PageModel
{
    private readonly IApplicationsService _applicationsService;
    [BindProperty] public Application? Application { get; set; }

    public UpdateModel(IApplicationsService applicationsService)
    {
        _applicationsService = applicationsService;
    }
    
    public async Task OnGetAsync(int id)
    {
        Application = await _applicationsService.GetApplicationById(id);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();
        
        await _applicationsService.UpdateApplication(Application);
        return Redirect("/");
    }
}