using HabitGains.Application.Core.Abstractions.Repositories;
using HabitGains.Domain.Core.Primitives;
using HabitGains.Domain.Habits;

namespace HabitGains.Application.Habits.Delete;

public sealed class DeleteHabitHandler(IHabitRepository habitRepository)
{
    public async Task<Result> Handle(DeleteHabitRequest request, CancellationToken cancellationToken)
    {
        Habit? habit = await habitRepository.GetById(request.Id, cancellationToken);

        if (habit is null)
        {
            return HabitErrors.NotFoundById(request.Id);
        }

        await habitRepository.Delete(request.Id, cancellationToken);

        return Result.Success();
    }
}
