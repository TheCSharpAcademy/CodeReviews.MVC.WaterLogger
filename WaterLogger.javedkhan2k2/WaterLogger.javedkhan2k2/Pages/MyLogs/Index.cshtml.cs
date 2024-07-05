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
    public class IndexModel : PageModel
    {
        private readonly IMyLogRepository _myLogRepository;
        public List<MyLog> MyLogs { get; set; }
        public IndexModel(IMyLogRepository myLogRepository)
        {
            _myLogRepository = myLogRepository;
        }

        public IActionResult OnGet()
        {
            MyLogs = _myLogRepository.GetAll();
            return Page();
        }
    }
}