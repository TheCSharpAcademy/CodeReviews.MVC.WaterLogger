using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using WaterLogger.DTOs;
using WaterLogger.Models;
using WaterLogger.Repositories.Interfaces;

namespace WaterLogger.Pages.Habits
{
    public class CreateModel : PageModel
    {
        
        private readonly IHabitRepository _habitRepository;
        private readonly IHabitUnitRepository _habitUnitRepository;
        public List<HabitUnit> AllHabitUnits { get; set; }

        public CreateModel(IHabitRepository habitRepository, IHabitUnitRepository habitUnitRepository)
        {
            _habitRepository = habitRepository;
            _habitUnitRepository = habitUnitRepository;
        }

        [BindProperty]
        public HabitAddDTO Habit { get; set; }

        public IActionResult OnGet()
        {
            AllHabitUnits = _habitUnitRepository.GetAll();
            return Page();
        }

        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {
                AllHabitUnits = _habitUnitRepository.GetAll();
                return Page();
            }
            _habitRepository.Add(Habit);
            return RedirectToPage("./Index");
        }

    }
}