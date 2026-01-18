using FluentValidation;
using FluentValidation.Results;
using HabitGains.Application.Core.Abstractions.Repositories;
using HabitGains.Application.Core.Extensions;
using HabitGains.Domain.Core.Abstractions;
using HabitGains.Domain.Core.Primitives;
using HabitGains.Domain.Habits;

namespace HabitGains.Application.Habits.Update;

public class UpdateHabitHandler(
    IHabitRepository habitRepository,
    IValidator<UpdateHabitRequest> validator,
    IDateTimeProvider dateTimeProvider
)
{
    public async Task<Result> Handle(UpdateHabitRequest request, CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return validationResult.ToValidationError();
        }

        Habit? habit = await habitRepository.GetById(request.Id, cancellationToken);

        if (habit is null)
        {
            return HabitErrors.NotFoundById(request.Id);
        }

        if (
            !string.Equals(habit.Name, request.Name, StringComparison.OrdinalIgnoreCase)
            && !await habitRepository.IsNameUnique(request.Name, cancellationToken)
        )
        {
            return HabitErrors.NameNotUnique(request.Name);
        }

        habit.Name = request.Name;
        habit.Measurement = request.Measurement;
        habit.Favorite = request.Favorite;
        habit.UpdatedAt = dateTimeProvider.UtcNow;

        await habitRepository.Update(habit, cancellationToken);

        return Result.Success();
    }
}
