using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProgressTracker.hasona23.Controllers;
using ProgressTracker.hasona23.Models;

namespace ProgressTracker.hasona23.Pages;

public class Delete : PageModel
{
    private readonly ProgressController _controller;

    public Delete(ProgressController controller)
    {
        _controller = controller;
    }
    [BindProperty]
    public ProgressModel ProgressModel { get; set; }
    public IActionResult OnGet(int id)
    {
        _controller.DeleteProgress(id);
        return RedirectToPage("./Index");
    }
}