using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WaterLogger.Models;
using WaterLogger.Repositories.Interfaces;

namespace WaterLogger.Pages
{
    public class UpdateModel : PageModel
    {
        private readonly IDailyExpenseRepository _repository;

        public UpdateModel(IDailyExpenseRepository repository)
        {
            _repository = repository;
        }

        [BindProperty]
        public DailyExpense Expense {get;set;}
        public IActionResult OnGet(int id)
        {
            Expense = _repository.GetDailyExpenseById(id);
            return Page();
        }

        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
            _repository.UpdateDailyExpense(Expense);
            return RedirectToPage("./Index");
        }

    }
}