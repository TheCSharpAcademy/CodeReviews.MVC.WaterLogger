using HabitGains.Domain.Core.Primitives;

namespace HabitGains.Domain.Habits;

public static class HabitErrors
{
    public static Error NameNotUnique(string name) =>
        Error.Conflict("Habit.Name.AlreadyExists", $"The habit with a name {name} already exists!");

    public static Error NotFoundById(Guid id) =>
        Error.NotFound("Habit.NotFoundById", $"The habit with Id = {id} was not found.");
}
