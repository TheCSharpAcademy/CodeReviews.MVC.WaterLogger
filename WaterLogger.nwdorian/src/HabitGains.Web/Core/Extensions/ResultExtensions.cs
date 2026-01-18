using HabitGains.Domain.Core.Primitives;

namespace HabitGains.Web.Core.Extensions;

public static class ResultExtensions
{
    public static string[] GetErrors(this Result result)
    {
        if (result.Error is ValidationError validationError)
        {
            return validationError.Errors.Select(e => e.Description).ToArray();
        }

        return new[] { result.Error.Description };
    }
}
