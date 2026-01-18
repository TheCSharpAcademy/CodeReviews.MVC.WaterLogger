using HabitGains.Application.Habits;
using HabitGains.Application.Habits.Delete;
using HabitGains.Application.Habits.GetById;
using HabitGains.Domain.Core.Primitives;
using HabitGains.Web.ViewModels.Habits;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HabitGains.Web.Pages;

public class DeleteModel(DeleteHabitHandler useCase, GetHabitByIdHandler getById) : PageModel
{
    public HabitItem Habit { get; set; } = default!;
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

        Habit = new HabitItem(
            response.Value.Id,
            response.Value.Name,
            response.Value.Measurement,
            response.Value.Favorite,
            response.Value.CreatedAt
        );

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(Guid? id, CancellationToken cancellationToken = default)
    {
        if (id is null)
        {
            return NotFound();
        }

        DeleteHabitRequest request = new(id.Value);
        Result response = await useCase.Handle(request, cancellationToken);

        if (response.IsFailure)
        {
            Errors.AddRange(response.Error.Description);
        }

        return RedirectToPage("./Index");
    }
}
