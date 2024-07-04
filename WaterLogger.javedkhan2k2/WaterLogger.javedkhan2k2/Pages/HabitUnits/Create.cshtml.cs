using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WaterLogger.DTOs;
using WaterLogger.Repositories.Interfaces;

namespace WaterLogger.Pages.HabitUnits
{
    public class CreateModel : PageModel
    {

        private readonly IHabitUnitRepository _repository;
        public CreateModel(IHabitUnitRepository repository)
        {
            _repository = repository;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public HabitUnitAddDTO HabitUnit { get; set; }

        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
            _repository.Add(HabitUnit);
            return RedirectToPage("./Index");
        }

    }
}