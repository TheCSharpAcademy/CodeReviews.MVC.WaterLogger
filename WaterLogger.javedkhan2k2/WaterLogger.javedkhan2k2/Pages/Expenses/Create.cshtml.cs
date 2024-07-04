using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using WaterLogger.DTOs;
using WaterLogger.Repositories.Interfaces;

namespace WaterLogger.Pages;

public class CreateModel : PageModel
{
    private readonly IDailyExpenseRepository _repository;

    public CreateModel(IDailyExpenseRepository repository)
    {
        _repository = repository;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public DailyExpenseAddDTO Expense { get; set; }
    public IActionResult OnPost()
    {
        if(!ModelState.IsValid)
        {
            return Page();
        }
        _repository.Add(Expense);
        return RedirectToPage("./Index");
    }

}