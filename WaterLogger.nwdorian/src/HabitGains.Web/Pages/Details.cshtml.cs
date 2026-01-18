using System.Globalization;
using HabitGains.Application.Entries.GetByHabitId;
using HabitGains.Application.Entries.GetForChart;
using HabitGains.Application.Habits;
using HabitGains.Application.Habits.GetById;
using HabitGains.Domain.Core.Primitives;
using HabitGains.Web.Core.Constants;
using HabitGains.Web.Core.Extensions;
using HabitGains.Web.ViewModels.Entries;
using HabitGains.Web.ViewModels.Habits;
using HabitGains.Web.ViewModels.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HabitGains.Web.Pages;

public class DetailsModel(
    GetHabitByIdHandler getHabit,
    GetEntriesByHabitIdHandler getEntries,
    GetEntriesForChartHandler getChartEntries
) : PageModel
{
    public IReadOnlyList<EntryItem> Entries { get; set; } = default!;
    public EntryChartData EntryChartData { get; set; } = default!;
    public Metadata Metadata { get; set; } = default!;
    public HabitItem Habit { get; set; } = default!;
    public decimal TotalQuantity { get; set; }
    public SelectList PageSizeOptions { get; set; } = default!;
    public string? SortColumn { get; set; }
    public string? SortOrder { get; set; }
    public DateTime? DateFromFilter { get; set; }
    public DateTime? DateToFilter { get; set; }
    public decimal? QuantityFromFilter { get; set; }
    public decimal? QuantityToFilter { get; set; }
    public List<string> Errors { get; set; } = [];

    public async Task<IActionResult> OnGetAsync(
        Guid? id,
        GetEntriesQuery query,
        CancellationToken cancellationToken = default
    )
    {
        if (id is null)
        {
            return NotFound();
        }

        GetHabitByIdRequest getHabitRequest = new(id.Value);
        Result<HabitResponse> getHabitResponse = await getHabit.Handle(getHabitRequest, cancellationToken);

        if (getHabitResponse.IsFailure)
        {
            Errors.AddRange(getHabitResponse.GetErrors());
            return Page();
        }

        Habit = new HabitItem(
            getHabitResponse.Value.Id,
            getHabitResponse.Value.Name,
            getHabitResponse.Value.Measurement,
            getHabitResponse.Value.Favorite,
            getHabitResponse.Value.CreatedAt
        );

        GetEntriesByHabitIdRequest getEntriesRequest = new(
            id.Value,
            query.QuantityFrom,
            query.QuantityTo,
            query.DateFrom,
            query.DateTo,
            query.CurrentPage,
            query.PageSize,
            query.SortColumn,
            query.SortOrder
        );

        Result<GetEntriesByHabitIdResponse> getEntriesResponse = await getEntries.Handle(
            getEntriesRequest,
            cancellationToken
        );

        if (getEntriesResponse.IsFailure)
        {
            Errors.AddRange(getEntriesResponse.GetErrors());
            return Page();
        }

        Entries = getEntriesResponse
            .Value.Items.Select(e => new EntryItem(e.Id, e.HabitId, e.Date, e.Quantity))
            .ToList();

        TotalQuantity = getEntriesResponse.Value.TotalQuantity;

        Metadata = new Metadata(
            getEntriesResponse.Value.Page,
            getEntriesResponse.Value.PageSize,
            getEntriesResponse.Value.TotalCount,
            getEntriesResponse.Value.TotalPages,
            getEntriesResponse.Value.HasPreviousPage,
            getEntriesResponse.Value.HasNextPage
        );

        PageSizeOptions = new SelectList(PagingDefaults.PageSizeOptions, selectedValue: query.PageSize);

        SortColumn = query.SortColumn;
        SortOrder = query.SortOrder;
        DateFromFilter = query.DateFrom;
        DateToFilter = query.DateTo;
        QuantityFromFilter = query.QuantityFrom;
        QuantityToFilter = query.QuantityTo;

        GetEntriesForChartRequest getChartEntriesRequest = new(
            id.Value,
            query.QuantityFrom,
            query.QuantityTo,
            query.DateFrom,
            query.DateTo
        );

        GetEntriesForChartResponse getChartEntriesResponse = await getChartEntries.Handle(
            getChartEntriesRequest,
            cancellationToken
        );

        EntryChartData = new EntryChartData()
        {
            Labels = getChartEntriesResponse
                .Entries.Select(e => e.Date.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture))
                .ToList(),
            Values = getChartEntriesResponse.Entries.Select(e => e.Quantity).ToList(),
        };

        return Page();
    }
}
