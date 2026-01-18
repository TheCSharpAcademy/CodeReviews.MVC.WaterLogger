using HabitGains.Application.Core.Abstractions.Repositories;
using HabitGains.Domain.Core.Primitives;
using HabitGains.Domain.Entries;

namespace HabitGains.Application.Entries.Delete;

public class DeleteEntryHandler(IEntryRepository entryRepository)
{
    public async Task<Result> Handle(DeleteEntryRequest request, CancellationToken cancellationToken)
    {
        Entry? entry = await entryRepository.GetById(request.Id, cancellationToken);

        if (entry is null)
        {
            return EntryErrors.NotFoundById(request.Id);
        }

        await entryRepository.Delete(request.Id, cancellationToken);

        return Result.Success();
    }
}
