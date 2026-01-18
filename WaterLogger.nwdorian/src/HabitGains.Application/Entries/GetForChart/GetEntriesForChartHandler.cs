using HabitGains.Application.Core.Abstractions.Repositories;
using HabitGains.Application.Core.Pagination.Entries;
using HabitGains.Domain.Entries;

namespace HabitGains.Application.Entries.GetForChart;

public sealed class GetEntriesForChartHandler(IEntryRepository entryRepository)
{
    public async Task<GetEntriesForChartResponse> Handle(
        GetEntriesForChartRequest request,
        CancellationToken cancellationToken
    )
    {
        EntryFilter filter = new(request.QuantityFrom, request.QuantityTo, request.DateFrom, request.DateTo);

        IReadOnlyList<Entry> entries = await entryRepository.GetEntriesForChart(
            request.HabitId,
            filter,
            cancellationToken
        );

        IReadOnlyList<EntryChartItem> entryChartItems = entries
            .GroupBy(e => e.Date.Date)
            .OrderBy(g => g.Key)
            .Select(g => new EntryChartItem(g.Key, g.Sum(x => x.Quantity)))
            .ToList();

        return new GetEntriesForChartResponse(entryChartItems);
    }
}
