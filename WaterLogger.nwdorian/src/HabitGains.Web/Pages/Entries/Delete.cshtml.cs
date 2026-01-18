using HabitGains.Application.Entries;
using HabitGains.Application.Entries.Delete;
using HabitGains.Application.Entries.GetById;
using HabitGains.Application.Habits.GetById;
using HabitGains.Domain.Core.Primitives;
using HabitGains.Web.Core.Extensions;
using HabitGains.Web.ViewModels.Entries;
using Microsoft.AspNetCore.Mvc;

namespace HabitGains.Web.Pages.Entries;

public class DeleteModel(
    DeleteEntryHandler deleteEntry,
    GetEntryByIdHandler getEntryById,
    GetHabitByIdHandler getHabitById
) : EntriesHabitPageModel
{
    public EntryItem Entry { get; set; } = default!;
    public List<string> EntryErrors { get; set; } = [];

    public async Task<IActionResult> OnGetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        GetEntryByIdRequest getEntryByIdRequest = new(id);
        Result<EntryResponse> getEntryByIdResponse = await getEntryById.Handle(getEntryByIdRequest, cancellationToken);

        if (getEntryByIdResponse.IsFailure)
        {
            EntryErrors.AddRange(getEntryByIdResponse.GetErrors());
            return Page();
        }

        Entry = new EntryItem(
            getEntryByIdResponse.Value.Id,
            getEntryByIdResponse.Value.HabitId,
            getEntryByIdResponse.Value.Date,
            getEntryByIdResponse.Value.Quantity
        );

        await PopulateHabit(getEntryByIdResponse.Value.HabitId, getHabitById, cancellationToken);

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(Guid id, CancellationToken cancellationToken = default)
    {
        GetEntryByIdRequest getEntryByIdRequest = new(id);
        Result<EntryResponse> getEntryByIdResponse = await getEntryById.Handle(getEntryByIdRequest, cancellationToken);

        if (getEntryByIdResponse.IsFailure)
        {
            EntryErrors.AddRange(getEntryByIdResponse.GetErrors());
            return Page();
        }

        DeleteEntryRequest deleteEntryRequest = new(id);

        Result deleteEntryResponse = await deleteEntry.Handle(deleteEntryRequest, cancellationToken);

        if (deleteEntryResponse.IsFailure)
        {
            EntryErrors.AddRange(deleteEntryResponse.GetErrors());
            return Page();
        }

        return RedirectToPage("/Details", new { id = getEntryByIdResponse.Value.HabitId });
    }
}
