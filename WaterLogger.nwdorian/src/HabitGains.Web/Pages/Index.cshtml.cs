using HabitGains.Application.Core.Abstractions.Repositories;
using HabitGains.Application.Habits.Get;
using HabitGains.Domain.Core.Primitives;
using HabitGains.Web.Core.Constants;
using HabitGains.Web.Core.Extensions;
using HabitGains.Web.ViewModels.Habits;
using HabitGains.Web.ViewModels.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HabitGains.Web.Pages;

public class IndexModel(GetHabitsHandler useCase, IHabitRepository habitRepository) : PageModel
{
    public IReadOnlyList<HabitItem> Habits { get; set; } = default!;
    public Metadata Metadata { get; set; } = default!;
    public SelectList PageSizeOptions { get; set; } = default!;
    public SelectList MeasurementOptions { get; set; } = default!;
    public SelectList FavoriteOptions { get; set; } = default!;
    public string? SortColumn { get; set; }
    public string? SortOrder { get; set; }
    public string? SearchTerm { get; set; }
    public string? MeasurementFilter { get; set; }
    public string? FavoriteFilter { get; set; }
    public List<string> Errors { get; set; } = [];

    public async Task<IActionResult> OnGetAsync(GetHabitsQuery query, CancellationToken cancellationToken = default)
    {
        bool? favorite = query.Favorite switch
        {
            "true" => true,
            "false" => false,
            _ => null,
        };

        GetHabitsRequest request = new(
            query.Measurement,
            favorite,
            query.SearchTerm,
            query.CurrentPage,
            query.PageSize,
            query.SortColumn,
            query.SortOrder
        );

        Result<GetHabitsResponse> response = await useCase.Handle(request, cancellationToken);

        if (response.IsFailure)
        {
            Errors.AddRange(response.GetErrors());
            return Page();
        }

        Habits = response
            .Value.Items.Select(h => new HabitItem(h.Id, h.Name, h.Measurement, h.Favorite, h.CreatedAt))
            .ToList();

        Metadata = new Metadata(
            response.Value.Page,
            response.Value.PageSize,
            response.Value.TotalCount,
            response.Value.TotalPages,
            response.Value.HasPreviousPage,
            response.Value.HasNextPage
        );

        SortColumn = query.SortColumn;
        SortOrder = query.SortOrder;
        SearchTerm = query.SearchTerm;

        MeasurementFilter = query.Measurement;
        MeasurementOptions = new SelectList(
            await habitRepository.GetHabitMeasurements(cancellationToken),
            selectedValue: query.Measurement
        );

        FavoriteFilter = query.Favorite;
        FavoriteOptions = new SelectList(
            new[]
            {
                new { Value = "", Text = "All" },
                new { Value = "true", Text = "Favorite" },
                new { Value = "false", Text = "Not Favorite" },
            },
            "Value",
            "Text",
            query.Favorite
        );

        PageSizeOptions = new SelectList(PagingDefaults.PageSizeOptions, selectedValue: query.PageSize);

        return Page();
    }
}
