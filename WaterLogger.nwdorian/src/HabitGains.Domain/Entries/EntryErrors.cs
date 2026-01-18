using HabitGains.Domain.Core.Primitives;

namespace HabitGains.Domain.Entries;

public static class EntryErrors
{
    public static Error NotFoundById(Guid id) =>
        Error.NotFound("Entry.NotFoundById", $"The entry with Id = {id} was not found.");

    public static Error DateCannotBeInFuture =>
        Error.Validation("Entry.DateCannotBeInFuture", "Date can't be in the future.");
}
