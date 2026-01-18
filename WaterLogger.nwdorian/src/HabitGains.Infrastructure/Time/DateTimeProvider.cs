using HabitGains.Domain.Core.Abstractions;

namespace HabitGains.Infrastructure.Time;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
