using FluentValidation;
using FluentValidation.Results;
using HabitGains.Application.Core.Abstractions.Repositories;
using HabitGains.Application.Core.Extensions;
using HabitGains.Domain.Core.Abstractions;
using HabitGains.Domain.Core.Primitives;
using HabitGains.Domain.Entries;

namespace HabitGains.Application.Entries.Create;

public class CreateEntryHandler(
    IEntryRepository entryRepository,
    IDateTimeProvider dateTimeProvider,
    IValidator<CreateEntryRequest> validator
)
{
    public async Task<Result> Handle(CreateEntryRequest request, CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return validationResult.ToValidationError();
        }

        if (request.Date > DateTime.Today)
        {
            return EntryErrors.DateCannotBeInFuture;
        }

        Entry entry = new()
        {
            Id = Guid.NewGuid(),
            HabitId = request.HabitId,
            Date = request.Date,
            Quantity = request.Quantity,
            CreatedAt = dateTimeProvider.UtcNow,
        };

        await entryRepository.Create(entry, cancellationToken);

        return Result.Success();
    }
}
