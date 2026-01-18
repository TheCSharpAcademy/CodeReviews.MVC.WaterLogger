using HabitGains.Application.Core.Abstractions.Repositories;
using HabitGains.Domain.Core.Primitives;
using HabitGains.Domain.Entries;

namespace HabitGains.Application.Entries.GetById;

public class GetEntryByIdHandler(IEntryRepository entryRepository)
{
    public async Task<Result<EntryResponse>> Handle(GetEntryByIdRequest request, CancellationToken cancellationToken)
    {
        Entry? entry = await entryRepository.GetById(request.Id, cancellationToken);

        if (entry is null)
        {
            return EntryErrors.NotFoundById(request.Id);
        }

        return new EntryResponse(entry.Id, entry.HabitId, entry.Date, entry.Quantity, entry.CreatedAt);
    }
}
