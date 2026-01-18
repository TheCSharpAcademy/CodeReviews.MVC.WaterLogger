using FluentValidation;
using FluentValidation.Results;
using HabitGains.Application.Core.Abstractions.Repositories;
using HabitGains.Application.Core.Extensions;
using HabitGains.Domain.Core.Abstractions;
using HabitGains.Domain.Core.Primitives;
using HabitGains.Domain.Entries;

namespace HabitGains.Application.Entries.Update;

public class UpdateEntryHandler(
    IEntryRepository entryRepository,
    IDateTimeProvider dateTimeProvider,
    IValidator<UpdateEntryRequest> validator
)
{
    public async Task<Result> Handle(UpdateEntryRequest request, CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return validationResult.ToValidationError();
        }

        Entry? entry = await entryRepository.GetById(request.Id, cancellationToken);

        if (entry is null)
        {
            return EntryErrors.NotFoundById(request.Id);
        }

        if (request.Date > DateTime.Today)
        {
            return EntryErrors.DateCannotBeInFuture;
        }

        entry.Date = request.Date;
        entry.Quantity = request.Quantity;
        entry.UpdatedAt = dateTimeProvider.UtcNow;

        await entryRepository.Update(entry, cancellationToken);

        return Result.Success();
    }
}
