using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeightLogger.samggannon.Data;
using WeightLogger.samggannon.Models;

namespace WeightLogger.samggannon.Pages
{
    public class EditModel : PageModel
    {
        private readonly DataAccess _dataFunctions;

        [BindProperty]
        public Weight? weightRecord { get; set; }

        public EditModel(DataAccess datafunctions)
        {
            _dataFunctions = datafunctions;
        }

        public void OnGet(int id)
        {
            GetRecordById(id);
            
        }

        public IActionResult OnPost(int id)
        {

            if (id <= 0)
            {
                ModelState.AddModelError(string.Empty, "Invalid ID.");
                return Page();
            }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid Id");
                return Page();
            }

            var result = _dataFunctions.EditWeightLog(weightRecord.Id, weightRecord.weightValue, weightRecord.loggedDate.ToString());

            if (!result)
            {
                ModelState.AddModelError(string.Empty, "Unable to update the weight record.");
                return Page();
            }

            return RedirectToPage("/Index");
        }

        private void GetRecordById(int id)
        {
            var dto = _dataFunctions.GetWeightLogById(id);

            if (dto == null)
            {
                // log, set to null, throw an exception
            }

            weightRecord = Weight.MapFromDto(dto);
        }
    }
}
