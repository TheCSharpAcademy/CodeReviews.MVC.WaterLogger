using FluentValidation;
using FluentValidation.Results;
using HabitGains.Application.Core.Abstractions.Repositories;
using HabitGains.Application.Core.Extensions;
using HabitGains.Application.Core.Pagination;
using HabitGains.Application.Core.Pagination.Habits;
using HabitGains.Domain.Core.Primitives;
using HabitGains.Domain.Habits;

namespace HabitGains.Application.Habits.Get;

public sealed class GetHabitsHandler(IHabitRepository habitRepository, IValidator<GetHabitsRequest> validator)
{
    public async Task<Result<GetHabitsResponse>> Handle(GetHabitsRequest request, CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return validationResult.ToValidationError();
        }

        HabitFilter filter = new(request.SearchTerm, request.Measurement, request.Favorite);
        HabitSorting sorting = new(request.SortColumn, request.SortOrder);
        Paging paging = new(request.Page, request.PageSize);

        int count = await habitRepository.CountHabits(filter, cancellationToken);
        IReadOnlyList<Habit> habits = await habitRepository.GetHabitsPage(filter, sorting, paging, cancellationToken);

        PagedList<HabitResponse> pagedList = new(
            habits.Select(h => new HabitResponse(h.Id, h.Name, h.Measurement, h.Favorite, h.CreatedAt)).ToList(),
            paging.Page,
            paging.PageSize,
            count
        );

        return new GetHabitsResponse(
            pagedList.Page,
            pagedList.PageSize,
            pagedList.TotalCount,
            pagedList.TotalPages,
            pagedList.HasPreviousPage,
            pagedList.HasNextPage,
            pagedList.Items
        );
    }
}
