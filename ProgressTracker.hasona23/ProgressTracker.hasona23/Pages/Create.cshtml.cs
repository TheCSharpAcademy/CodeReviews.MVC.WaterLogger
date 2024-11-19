using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProgressTracker.hasona23.Controllers;
using ProgressTracker.hasona23.Models;

namespace ProgressTracker.hasona23.Pages;

public class Create : PageModel
{
    private readonly ProgressController _controller;
    [BindProperty]
    public ProgressModel Progress { get; set; }

    public Create(ProgressController controller)
    {
        _controller = controller;
    }
    public IActionResult OnGet()
    {
        return Page();
    }
    public IActionResult OnPost()
    {
        if (!ModelState.IsValid) 
            return Page();
       
        _controller.AddProgress(Progress);
        return RedirectToPage("./Index");
    }

}