using Data.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace ApplyingTracker.Pages;

public class IndexModel : PageModel
{
    private readonly IApplicationsService _applicationsService;
    public IEnumerable<Application> Applications { get; set; } = [];
    
    public IndexModel(IApplicationsService applicationsService)
    {
        _applicationsService = applicationsService;
    }

    public async Task OnGetAsync()
    {
        var applications = await _applicationsService.GetApplications();
        Applications = applications;
    }
}