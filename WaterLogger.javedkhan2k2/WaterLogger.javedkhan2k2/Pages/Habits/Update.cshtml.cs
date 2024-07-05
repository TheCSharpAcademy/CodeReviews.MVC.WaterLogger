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
    public class UpdateModel : PageModel
    {
        private readonly IHabitRepository _habitRepository;
        private readonly IHabitUnitRepository _habitUnitRepository;
        public List<HabitUnit> AllHabitUnits { get; set; }

        public UpdateModel(IHabitRepository habitRepository, IHabitUnitRepository habitUnitRepository)
        {
            _habitRepository = habitRepository;
            _habitUnitRepository = habitUnitRepository;
        }

        public IActionResult OnGet(int id)
        {
            AllHabitUnits = _habitUnitRepository.GetAll();
            Habit = _habitRepository.GetByIdForUpdate(id);
            return Page();
        }

        [BindProperty]
        public HabitUpdateDTO Habit { get; set; }
        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {
                AllHabitUnits = _habitUnitRepository.GetAll();
                return Page();
            }
            _habitRepository.Update(Habit);
            return RedirectToPage("./Index");
        }

    }
}