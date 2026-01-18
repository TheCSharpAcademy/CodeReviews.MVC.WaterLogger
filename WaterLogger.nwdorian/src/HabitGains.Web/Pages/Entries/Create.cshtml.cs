using HabitGains.Application.Entries.Create;
using HabitGains.Application.Habits.GetById;
using HabitGains.Domain.Core.Primitives;
using HabitGains.Web.Core.Extensions;
using HabitGains.Web.ViewModels.Entries;
using Microsoft.AspNetCore.Mvc;

namespace HabitGains.Web.Pages.Entries;

public class CreateModel(CreateEntryHandler createEntry, GetHabitByIdHandler getHabitById) : EntriesHabitPageModel
{
    [BindProperty]
    public CreateEntryForm Input { get; set; } = default!;
    public List<string> CreateEntryErrors { get; set; } = [];

    public async Task<IActionResult> OnGet(Guid id, CancellationToken cancellationToken)
    {
        await PopulateHabit(id, getHabitById, cancellationToken);

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(Guid id, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await PopulateHabit(id, getHabitById, cancellationToken);

        CreateEntryRequest createEntryRequest = new(id, Input.Date, Input.Quantity);

        Result createEntryResponse = await createEntry.Handle(createEntryRequest, cancellationToken);

        if (createEntryResponse.IsFailure)
        {
            CreateEntryErrors.AddRange(createEntryResponse.GetErrors());
            return Page();
        }

        return RedirectToPage("/Details", new { id });
    }
}
