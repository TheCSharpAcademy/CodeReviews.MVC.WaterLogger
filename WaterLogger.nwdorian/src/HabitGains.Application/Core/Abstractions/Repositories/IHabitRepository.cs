using HabitGains.Application.Core.Pagination;
using HabitGains.Application.Core.Pagination.Habits;
using HabitGains.Domain.Habits;

namespace HabitGains.Application.Core.Abstractions.Repositories;

public interface IHabitRepository
{
    Task<IReadOnlyList<Habit>> GetHabitsPage(
        HabitFilter filter,
        HabitSorting sorting,
        Paging paging,
        CancellationToken cancellationToken
    );
    Task<int> CountHabits(HabitFilter filter, CancellationToken cancellationToken);
    Task<Habit?> GetById(Guid id, CancellationToken cancellationToken);
    Task Create(Habit habit, CancellationToken cancellationToken);
    Task Delete(Guid id, CancellationToken cancellationToken);
    Task Update(Habit habit, CancellationToken cancellationToken);
    Task<List<string>> GetHabitMeasurements(CancellationToken cancellationToken);
    Task<bool> IsNameUnique(string name, CancellationToken cancellationToken);
    Task<bool> Any();
    Task BulkInsert(List<Habit> habits);
}
