using FluentValidation.Results;
using HabitGains.Domain.Core.Primitives;

namespace HabitGains.Application.Core.Extensions;

public static class ValidationExtensions
{
    public static ValidationError ToValidationError(this ValidationResult validationResult)
    {
        Error[] errors = validationResult
            .Errors.Select(failure => Error.Validation(code: failure.PropertyName, description: failure.ErrorMessage))
            .ToArray();

        return new(errors);
    }
}
