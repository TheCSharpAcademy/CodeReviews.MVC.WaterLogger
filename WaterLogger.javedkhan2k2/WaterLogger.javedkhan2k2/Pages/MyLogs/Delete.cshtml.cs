using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using WaterLogger.Models;
using WaterLogger.Repositories.Interfaces;

namespace WaterLogger.Pages.MyLogs
{
    public class DeleteModel : PageModel
    {
        private readonly IMyLogRepository _myLogRepository;
        public MyLog MyLog { get; set; }
        public DeleteModel(IMyLogRepository myLogRepository)
        {
            _myLogRepository = myLogRepository;
        }

        public IActionResult OnGet(int id)
        {
            MyLog = _myLogRepository.GetById(id);
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            _myLogRepository.Delete(id);
            return RedirectToPage("./Index");
        }
    }
}