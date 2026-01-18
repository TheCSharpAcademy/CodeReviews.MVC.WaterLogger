using HabitGains.Application.Habits.Create;
using HabitGains.Domain.Core.Primitives;
using HabitGains.Web.Core.Extensions;
using HabitGains.Web.ViewModels.Habits;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HabitGains.Web.Pages;

public class CreateModel(CreateHabitHandler useCase) : PageModel
{
    [BindProperty]
    public CreateHabitInput Input { get; set; } = default!;
    public List<string> Errors { get; set; } = [];

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        CreateHabitRequest request = new(Input.Name, Input.Measurement, Input.Favorite);

        Result response = await useCase.Handle(request, cancellationToken);

        if (response.IsFailure)
        {
            Errors.AddRange(response.GetErrors());
            return Page();
        }

        return RedirectToPage("./Index");
    }
}
