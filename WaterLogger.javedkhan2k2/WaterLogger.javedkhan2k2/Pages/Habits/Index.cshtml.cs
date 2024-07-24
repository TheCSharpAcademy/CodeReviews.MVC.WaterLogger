using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using WaterLogger.Models;
using WaterLogger.Repositories.Interfaces;

namespace WaterLogger.Pages.Habits
{
    public class IndexModel : PageModel
    {
        private readonly IHabitRepository _habitRepository;
        public List<Habit> Habits { get; set; }
        public IndexModel(IHabitRepository habitRepository)
        {
            _habitRepository = habitRepository;
        }


        public IActionResult OnGet()
        {
            Habits = _habitRepository.GetAll();
            return Page();
        }
    }
}