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
    public class UpdateModel : PageModel
    {
        private readonly IMyLogRepository _myLogRepository;
        private readonly IHabitRepository _habitRepository;
        public List<Habit> Habits { get; set; }
        public UpdateModel(IMyLogRepository myLogRepository, IHabitRepository habitRepository)
        {
            _myLogRepository = myLogRepository;
            _habitRepository = habitRepository;
        }

        [BindProperty]
        public MyLogUpdateDTO MyLog { get; set; }
        public IActionResult OnGet(int id)
        {
            MyLog = _myLogRepository.GetByIdForUpdate(id);
            Habits = _habitRepository.GetAll();
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            if (!ModelState.IsValid)
            {
                Habits = _habitRepository.GetAll();
                return Page();
            }
            _myLogRepository.Update(MyLog);
            return RedirectToPage("./Index");
        }


    }
}