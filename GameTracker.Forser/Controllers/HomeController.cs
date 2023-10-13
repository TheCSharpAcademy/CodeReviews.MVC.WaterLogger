using Microsoft.AspNetCore.Mvc;

namespace GameTracker.Forser.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
