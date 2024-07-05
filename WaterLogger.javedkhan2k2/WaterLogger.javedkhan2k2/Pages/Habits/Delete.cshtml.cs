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
    public class DeleteModel : PageModel
    {
        private readonly IHabitRepository _habitRepository;
        public Habit Habit { get; set; }

        public DeleteModel(IHabitRepository habitRepository)
        {
            _habitRepository = habitRepository;
        }

        public IActionResult OnGet(int id)
        {
            Habit = _habitRepository.GetById(id);
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            _habitRepository.Delete(id);
            return RedirectToPage("./Index");
        }
    }
}