using HabitGains.Application.Habits;
using HabitGains.Application.Habits.GetById;
using HabitGains.Application.Habits.Update;
using HabitGains.Domain.Core.Primitives;
using HabitGains.Web.ViewModels.Habits;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HabitGains.Web.Pages;

public class EditModel(UpdateHabitHandler update, GetHabitByIdHandler getById) : PageModel
{
    [BindProperty]
    public UpdateHabitInput Input { get; set; } = default!;
    public List<string> Errors { get; set; } = [];

    public async Task<IActionResult> OnGetAsync(Guid? id, CancellationToken cancellationToken = default)
    {
        if (id is null)
        {
            return NotFound();
        }

        GetHabitByIdRequest request = new(id.Value);
        Result<HabitResponse> response = await getById.Handle(request, cancellationToken);

        if (response.IsFailure)
        {
            Errors.AddRange(response.Error.Description);
            return Page();
        }

        Input = new UpdateHabitInput()
        {
            Id = response.Value.Id,
            Name = response.Value.Name,
            Measurement = response.Value.Measurement,
            Favorite = response.Value.Favorite,
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(Guid? id, CancellationToken cancellationToken = default)
    {
        if (id is null)
        {
            return NotFound();
        }

        UpdateHabitRequest request = new(id.Value, Input.Name, Input.Measurement, Input.Favorite);
        Result response = await update.Handle(request, cancellationToken);

        if (response.IsFailure)
        {
            Errors.AddRange(response.Error.Description);
            return Page();
        }

        return RedirectToPage("./Index");
    }
}
