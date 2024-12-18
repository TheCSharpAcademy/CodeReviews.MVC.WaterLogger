using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WaterLogger.Dejmenek.Models;
using WaterLogger.Dejmenek.Repositories;

namespace WaterLogger.Dejmenek.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly IDrinkingWaterRepository _drinkingWaterRepository;
        private readonly ILogger _logger;
        [BindProperty]
        public DrinkingWater DrinkingWater { get; set; } = default!;

        public DeleteModel(IDrinkingWaterRepository drinkingWaterRepository, ILogger<DeleteModel> logger)
        {
            _drinkingWaterRepository = drinkingWaterRepository;
            _logger = logger;
        }

        public IActionResult OnGet(int id)
        {
            try
            {
                DrinkingWater = _drinkingWaterRepository.GetById(id);
                _logger.LogInformation("Successfully retrieved drinking water record with Id: {Id}.", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the drinking water record with Id: {Id}.", id);
            }

            return Page();
        }

        public IActionResult OnPost(int id)
        {
            try
            {
                _drinkingWaterRepository.Delete(id);
                _logger.LogInformation("Successfully deleted drinking water record with Id: {Id}.", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the drinking water record with Id: {Id}.", id);
            }

            return RedirectToPage("./Index");
        }
    }
}
