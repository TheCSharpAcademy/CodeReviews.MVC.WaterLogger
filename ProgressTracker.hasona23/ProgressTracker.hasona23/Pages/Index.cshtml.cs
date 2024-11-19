using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProgressTracker.hasona23.Controllers;
using ProgressTracker.hasona23.Models;
using ProgressTracker.hasona23.Services;

namespace ProgressTracker.hasona23.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        
        private readonly ProgressController _progressController;
        [BindProperty] 
        public List<ProgressModel> Progresses { get; set; }

        public IndexModel(ILogger<IndexModel> logger, ProgressController progressController)
        {
            _logger = logger;
            _progressController = progressController;
            Progresses = new List<ProgressModel>();
        }

        public void OnGet()
        {
            Progresses = _progressController.GetAllProgress();

        }
        
    }
}
