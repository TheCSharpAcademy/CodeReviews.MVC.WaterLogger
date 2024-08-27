using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace ApplyingTracker.Pages;

public class DeleteModel : PageModel
{
    private readonly IApplicationsService _applicationsService;
    
    public DeleteModel(IApplicationsService applicationsService)
    {
        _applicationsService = applicationsService;
    }
    
    public async Task<IActionResult> OnPostAsync(int id)
    {
        var application = await _applicationsService.GetApplicationById(id);
        if (application is null) return Page();
        await _applicationsService.DeleteApplication(application);
        return Redirect("/");
    }
}