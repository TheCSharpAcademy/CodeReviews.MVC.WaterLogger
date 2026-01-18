using HabitGains.Application.Core.Pagination;
using HabitGains.Application.Core.Pagination.Entries;
using HabitGains.Domain.Entries;

namespace HabitGains.Application.Core.Abstractions.Repositories;

public interface IEntryRepository
{
    Task<IReadOnlyList<Entry>> GetEntriesPageByHabitId(
        Guid habitId,
        EntryFilter filter,
        EntrySorting sorting,
        Paging paging,
        CancellationToken cancellationToken
    );
    Task<int> CountEntriesByHabitId(Guid habitId, EntryFilter filter, CancellationToken cancellationToken);
    Task<decimal> GetTotalQuantityByHabitId(Guid habitId, EntryFilter filter, CancellationToken cancellationToken);
    Task<IReadOnlyList<Entry>> GetEntriesForChart(
        Guid habitId,
        EntryFilter filter,
        CancellationToken cancellationToken
    );
    Task<Entry?> GetById(Guid id, CancellationToken cancellationToken);
    Task Create(Entry entry, CancellationToken cancellationToken);
    Task Update(Entry entry, CancellationToken cancellationToken);
    Task Delete(Guid id, CancellationToken cancellationToken);
    Task BulkInsert(List<Entry> entries);
    Task<bool> Any();
}
