using FluentValidation;
using FluentValidation.Results;
using HabitGains.Application.Core.Abstractions.Repositories;
using HabitGains.Application.Core.Extensions;
using HabitGains.Domain.Core.Abstractions;
using HabitGains.Domain.Core.Primitives;
using HabitGains.Domain.Habits;

namespace HabitGains.Application.Habits.Create;

public class CreateHabitHandler(
    IHabitRepository habitRepository,
    IDateTimeProvider dateTimeProvider,
    IValidator<CreateHabitRequest> validator
)
{
    public async Task<Result> Handle(CreateHabitRequest request, CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return validationResult.ToValidationError();
        }

        if (!await habitRepository.IsNameUnique(request.Name, cancellationToken))
        {
            return HabitErrors.NameNotUnique(request.Name);
        }

        Habit habit = new()
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Measurement = request.Measurement,
            Favorite = request.Favorite,
            CreatedAt = dateTimeProvider.UtcNow,
        };

        await habitRepository.Create(habit, cancellationToken);

        return Result.Success();
    }
}
