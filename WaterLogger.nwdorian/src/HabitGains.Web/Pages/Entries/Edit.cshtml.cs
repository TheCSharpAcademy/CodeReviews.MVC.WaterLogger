using HabitGains.Application.Entries;
using HabitGains.Application.Entries.GetById;
using HabitGains.Application.Entries.Update;
using HabitGains.Application.Habits.GetById;
using HabitGains.Domain.Core.Primitives;
using HabitGains.Web.Core.Extensions;
using HabitGains.Web.ViewModels.Entries;
using Microsoft.AspNetCore.Mvc;

namespace HabitGains.Web.Pages.Entries;

public class EditModel(
    UpdateEntryHandler updateEntry,
    GetEntryByIdHandler getEntryById,
    GetHabitByIdHandler getHabitById
) : EntriesHabitPageModel
{
    [BindProperty]
    public UpdateEntryForm Input { get; set; } = default!;
    public List<string> UpdateEntryErrors { get; set; } = [];

    public async Task<IActionResult> OnGetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        GetEntryByIdRequest getEntryByIdRequest = new(id);
        Result<EntryResponse> getEntryByIdResponse = await getEntryById.Handle(getEntryByIdRequest, cancellationToken);

        if (getEntryByIdResponse.IsFailure)
        {
            UpdateEntryErrors.AddRange(getEntryByIdResponse.GetErrors());
            return Page();
        }

        Input = new UpdateEntryForm()
        {
            Id = getEntryByIdResponse.Value.Id,
            Date = getEntryByIdResponse.Value.Date,
            Quantity = getEntryByIdResponse.Value.Quantity,
        };

        await PopulateHabit(getEntryByIdResponse.Value.HabitId, getHabitById, cancellationToken);

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        GetEntryByIdRequest getEntryByIdRequest = new(id);
        Result<EntryResponse> getEntryByIdResponse = await getEntryById.Handle(getEntryByIdRequest, cancellationToken);

        if (getEntryByIdResponse.IsFailure)
        {
            UpdateEntryErrors.AddRange(getEntryByIdResponse.GetErrors());
            return Page();
        }

        await PopulateHabit(getEntryByIdResponse.Value.HabitId, getHabitById, cancellationToken);

        UpdateEntryRequest updateEntryRequest = new(id, Input.Date, Input.Quantity);

        Result updateEntryResponse = await updateEntry.Handle(updateEntryRequest, cancellationToken);

        if (updateEntryResponse.IsFailure)
        {
            UpdateEntryErrors.AddRange(updateEntryResponse.GetErrors());
            return Page();
        }

        return RedirectToPage("/Details", new { Habit.Id });
    }
}
