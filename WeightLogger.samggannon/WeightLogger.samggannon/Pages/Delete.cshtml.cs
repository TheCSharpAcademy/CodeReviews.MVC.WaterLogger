using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeightLogger.samggannon.Data;
using WeightLogger.samggannon.Models;

namespace WeightLogger.samggannon.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly DataAccess _dataFunctions;
        public Weight weightRecord { get; set; }

        public DeleteModel(DataAccess datafunctions)
        {
            _dataFunctions = datafunctions;
        }
        
        public void OnGet(int id)
        {
            var dto = _dataFunctions.GetWeightLogById(id);

            if (dto == null)
            {
                // log, set to null, throw an exception
                weightRecord = null;
            }

            weightRecord = Weight.MapFromDto(dto);
        }

        public IActionResult OnPost(int id)
        {
            
            if (id <= 0)
            {
                ModelState.AddModelError(string.Empty, "Invalid ID.");
                return Page();
            }

            var result = _dataFunctions.DeleteWeightLog(id);

            if (!result)
            {
                ModelState.AddModelError(string.Empty, "Unable to delete the weight record.");
                return Page();
            }

            return RedirectToPage("/Index");
        }
    }
}
