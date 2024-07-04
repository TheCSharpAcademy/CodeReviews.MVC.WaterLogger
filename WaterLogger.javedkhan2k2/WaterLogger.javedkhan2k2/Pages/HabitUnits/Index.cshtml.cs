using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WaterLogger.Models;
using WaterLogger.Repositories.Interfaces;

namespace WaterLogger.Pages.HabitUnits;

public class IndexModel : PageModel
{
    private readonly IHabitUnitRepository _repository;
    public List<HabitUnit> HabitUnits { get; set; }
    public IndexModel(IHabitUnitRepository repository)
    {
        _repository = repository;
    }

    public IActionResult OnGet()
    {
        HabitUnits = _repository.GetAll();
        return Page();
    }
}
