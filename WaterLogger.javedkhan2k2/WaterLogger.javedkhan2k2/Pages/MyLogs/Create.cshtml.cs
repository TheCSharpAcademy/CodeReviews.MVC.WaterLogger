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

namespace WaterLogger.Pages.MyLogs
{
    public class CreateModel : PageModel
    {
        private readonly IMyLogRepository _myLogRepository;
        private readonly IHabitRepository _habitRepository;
        public List<Habit> Habits { get; set; }
        public CreateModel(IMyLogRepository myLogRepository, IHabitRepository habitRepository)
        {
            _myLogRepository = myLogRepository;
            _habitRepository = habitRepository;
        }

        public IActionResult OnGet()
        {
            Habits = _habitRepository.GetAll();
            return Page();
        }

        [BindProperty]
        public MyLogAddDTO MyLog { get; set; }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Habits = _habitRepository.GetAll();
                return Page();
            }
            _myLogRepository.Add(MyLog);
            return RedirectToPage("./Index");
        }
    }
}