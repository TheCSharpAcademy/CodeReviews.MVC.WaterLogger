namespace HabitGains.Application.Entries.GetForChart;

public sealed record GetEntriesForChartResponse(IReadOnlyList<EntryChartItem> Entries);
