using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WaterLogger.Models;
using WaterLogger.Repositories.Interfaces;

namespace WaterLogger.Pages;

public class IndexModel : PageModel
{
    private readonly IDailyExpenseRepository _repository;
    public List<DailyExpense> Expenses { get; set; }
    public IndexModel(IDailyExpenseRepository repository)
    {
        _repository = repository;
    }

    public void OnGet()
    {
        Expenses = _repository.GetAll();
    }
}
