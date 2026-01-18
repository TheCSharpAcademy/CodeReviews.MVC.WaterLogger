using HabitGains.Domain.Core.Abstractions;
using Microsoft.AspNetCore.Diagnostics;

namespace HabitGains.Web.Core.Infrastructure;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IDateTimeProvider dateTimeProvider)
    : IExceptionHandler
{
    public ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        logger.LogError(
            "Error Message: {ExceptionMessage}, Time of occurrence {Time}",
            exception.Message,
            dateTimeProvider.UtcNow
        );

        return ValueTask.FromResult(false);
    }
}
