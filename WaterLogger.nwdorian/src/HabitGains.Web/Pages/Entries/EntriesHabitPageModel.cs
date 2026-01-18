using HabitGains.Application.Habits;
using HabitGains.Application.Habits.GetById;
using HabitGains.Domain.Core.Primitives;
using HabitGains.Web.Core.Extensions;
using HabitGains.Web.ViewModels.Habits;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HabitGains.Web.Pages.Entries;

public class EntriesHabitPageModel : PageModel
{
    public HabitItem Habit { get; set; } = default!;
    public List<string> GetHabitErrors { get; set; } = [];

    public async Task PopulateHabit(Guid id, GetHabitByIdHandler getHabitById, CancellationToken cancellationToken)
    {
        GetHabitByIdRequest getHabitByIdRequest = new(id);

        Result<HabitResponse> getHabitByIdResponse = await getHabitById.Handle(getHabitByIdRequest, cancellationToken);

        if (getHabitByIdResponse.IsFailure)
        {
            GetHabitErrors.AddRange(getHabitByIdResponse.GetErrors());
            return;
        }

        Habit = new HabitItem(
            getHabitByIdResponse.Value.Id,
            getHabitByIdResponse.Value.Name,
            getHabitByIdResponse.Value.Measurement,
            getHabitByIdResponse.Value.Favorite,
            getHabitByIdResponse.Value.CreatedAt
        );
    }
}
