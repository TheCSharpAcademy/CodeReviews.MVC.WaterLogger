using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.Sqlite;
using Water_Logger.Data;
using Water_Logger.Models;

namespace Water_Logger.Pages
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public required DrinkingWaterVM LoggingData { get; set; } = new();
        
        private Database _db;
        public CreateModel(Database db)
        {
            _db = db;
        }

        public void OnGet()
        {
           
            List<SelectListItem> list = new List<SelectListItem>();   
            foreach(var prop in typeof(Measures).GetProperties())
            {
                list.Add(new SelectListItem(){
                    Text = prop.GetCustomAttribute<DisplayAttribute>()?.Name,
                    Value = prop.GetValue(null)?.ToString() 
                });
            }
            LoggingData.StandardList = list;

        }
        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page(); 
            }

            _db.Create(LoggingData.DrinkingWater ?? new DrinkingWater());

            TempData["success"] = "Log Created Successfully!!";

            return RedirectToPage("/Index");
        }
    }
}
