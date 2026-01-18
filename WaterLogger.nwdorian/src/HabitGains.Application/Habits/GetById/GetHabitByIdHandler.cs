using HabitGains.Application.Core.Abstractions.Repositories;
using HabitGains.Domain.Core.Primitives;
using HabitGains.Domain.Habits;

namespace HabitGains.Application.Habits.GetById;

public class GetHabitByIdHandler(IHabitRepository habitRepository)
{
    public async Task<Result<HabitResponse>> Handle(GetHabitByIdRequest request, CancellationToken cancellationToken)
    {
        Habit? habit = await habitRepository.GetById(request.Id, cancellationToken);

        if (habit is null)
        {
            return HabitErrors.NotFoundById(request.Id);
        }

        return new HabitResponse(habit.Id, habit.Name, habit.Measurement, habit.Favorite, habit.CreatedAt);
    }
}
