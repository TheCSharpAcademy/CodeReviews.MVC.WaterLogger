using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WaterLogger.Dejmenek.Models;
using WaterLogger.Dejmenek.Repositories;

namespace WaterLogger.Dejmenek.Pages
{
    public class CreateModel : PageModel
    {
        private readonly IDrinkingWaterRepository _drinkingWaterRepository;
        private readonly IMeasureRepository _measureRepository;
        private readonly ILogger _logger;
        public List<SelectListItem> Options { get; set; } = new List<SelectListItem>();

        public CreateModel(IDrinkingWaterRepository drinkingWaterRepository, IMeasureRepository measureRepository, ILogger<CreateModel> logger)
        {
            _drinkingWaterRepository = drinkingWaterRepository;
            _measureRepository = measureRepository;
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            try
            {
                List<Measure> measures = _measureRepository.GetAllMeasures();
                foreach (var measure in measures)
                {
                    Options.Add(new SelectListItem
                    {
                        Value = measure.Id.ToString(),
                        Text = measure.Name
                    });
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving measures");
            }

            return Page();
        }

        [BindProperty]
        public DrinkingWater DrinkingWater { get; set; } = default!;

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                _drinkingWaterRepository.Create(DrinkingWater);
                _logger.LogInformation("Successfully created drinking water record.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the drinking water record.");
            }

            return RedirectToPage("./Index");
        }
    }
}
