using Microsoft.AspNetCore.Mvc;
using ReadToKidsTracker.Models;
using ReadToKidsTracker.Services;
using System.Diagnostics;

namespace ReadToKidsTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ReadService _readService;

        public HomeController(ILogger<HomeController> logger, ReadService readService)
        {
            _logger = logger;
            _readService = readService;
        }

        public IActionResult Index()
        {
            var readsViewModel = new ReadSessionViewModel();
            try
            {
                readsViewModel.ReadSessions = _readService.GetReadSessions();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Something went wrong when fetching data: {ex.Message}";
            }
            return View(readsViewModel); // passing viewmodel list to view
        }

        [HttpGet]
        public IActionResult SortByDate()
        {
            var readsViewModel = new ReadSessionViewModel();
            try
            {
                readsViewModel.ReadSessions = _readService.GetReadSessions();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Something went wrong when fetching data: {ex.Message}";
            }
            return View("Index",readsViewModel); // passing viewmodel list to view
        }

        [HttpGet]
        public IActionResult SortByBookName()
        {
            var readsViewModel = new ReadSessionViewModel();
            try
            {
                readsViewModel.ReadSessions = _readService.GetReadSessions().OrderBy(x => x.BookName).ToList(); 
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Something went wrong when fetching data: {ex.Message}";
            }
            return View("Index",readsViewModel); // passing viewmodel list to view
        }

        [HttpGet]
        public IActionResult SortByTotalPages()
        {
            var readsViewModel = new ReadSessionViewModel();
            try
            {
                readsViewModel.ReadSessions = _readService.GetReadSessions().OrderByDescending(x => x.TotalPages).ToList();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Something went wrong when fetching data: {ex.Message}";
            }
            return View("Index", readsViewModel); // passing viewmodel list to view
        }

        [HttpGet]
        public IActionResult SortByDuration()
        {
            var readsViewModel = new ReadSessionViewModel();
            try
            {
                readsViewModel.ReadSessions = _readService.GetReadSessions().OrderByDescending(x => x.Duration).ToList();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Something went wrong when fetching data: {ex.Message}";
            }
            return View("Index", readsViewModel); // passing viewmodel list to view
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult SessionDetailView(int id)
        {
            var session = _readService.GetSessionById(id);
            return View(session);
        }

        [HttpGet]
        public IActionResult AddSessionView()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddSession(ReadSessionView newSession)
        {
            if (!ModelState.IsValid)
            {
                return View("AddSessionView", newSession);
            }

            try
            {
                _readService.AddSession(newSession);
                TempData["SuccessMessage"] = "Session added successfully!";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error adding session: {ex.Message}";
                return View("AddSessionView", newSession);
            }

        }

        [HttpPost]
        public IActionResult Update(ReadSessionView updatedSession)
        {
            if (!ModelState.IsValid)
            {
                return View("SessionDetailView", updatedSession);
            }

            try
            {
                _readService.UpdateSession(updatedSession);
                TempData["SuccessMessage"] = "Session updated successfully!";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error updating session: {ex.Message}";
                return View("SessionDetailView", updatedSession);
            }
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                _readService.DeleteSession(id);
                TempData["SuccessMessage"] = "Session deleted successfully!";
                return new OkResult();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error deleting session: {ex.Message}";
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
