using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProgressTracker.hasona23.Controllers;
using ProgressTracker.hasona23.Models;

namespace ProgressTracker.hasona23.Pages;

public class Update : PageModel
{
    private readonly ProgressController _controller;
    [BindProperty]
    public ProgressModel Progress { get; set; }
    public Update(ProgressController controller)
    {
        _controller = controller;
    }
    
    public IActionResult OnGet(int id)
    {
        Progress = _controller.GetById(id);
        return Page();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid) return Page();
        _controller.UpdateProgress(Progress);
        foreach (var progressSubTask in Progress.SubTasks)
        {
            Console.WriteLine($"{progressSubTask.Id} : {progressSubTask.ProgressId} : {progressSubTask.Title} : {progressSubTask.IsCompleted}");
            
        }
        return RedirectToPage("./Index");
    }
}