using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using WaterLogger.Models;
using WaterLogger.Repositories.Interfaces;

namespace WaterLogger.Pages.HabitUnits
{
    public class UpdateModel : PageModel
    {
        private readonly IHabitUnitRepository _repository;

        public UpdateModel(IHabitUnitRepository repository)
        {
            _repository = repository;
        }

        [BindProperty]
        public HabitUnit HabitUnit { get; set; }
        
        public IActionResult OnGet(int id)
        {
            HabitUnit = _repository.GetById(id);
            return Page();
        }

        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
            _repository.Update(HabitUnit);
            return RedirectToPage("./Index");
        }

    }
}