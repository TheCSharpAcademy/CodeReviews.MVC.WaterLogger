using FluentValidation;
using FluentValidation.Results;
using HabitGains.Application.Core.Abstractions.Repositories;
using HabitGains.Application.Core.Extensions;
using HabitGains.Application.Core.Pagination;
using HabitGains.Application.Core.Pagination.Entries;
using HabitGains.Domain.Core.Primitives;
using HabitGains.Domain.Entries;

namespace HabitGains.Application.Entries.GetByHabitId;

public class GetEntriesByHabitIdHandler(
    IEntryRepository entryRepository,
    IValidator<GetEntriesByHabitIdRequest> validator
)
{
    public async Task<Result<GetEntriesByHabitIdResponse>> Handle(
        GetEntriesByHabitIdRequest request,
        CancellationToken cancellationToken
    )
    {
        ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return validationResult.ToValidationError();
        }

        EntryFilter filter = new(request.QuantityFrom, request.QuantityTo, request.DateFrom, request.DateTo);
        EntrySorting sorting = new(request.SortColumn, request.SortOrder);
        Paging paging = new(request.Page, request.PageSize);

        int count = await entryRepository.CountEntriesByHabitId(request.HabitId, filter, cancellationToken);
        IReadOnlyList<Entry> entries = await entryRepository.GetEntriesPageByHabitId(
            request.HabitId,
            filter,
            sorting,
            paging,
            cancellationToken
        );

        decimal totalQuantity = await entryRepository.GetTotalQuantityByHabitId(
            request.HabitId,
            filter,
            cancellationToken
        );

        PagedList<EntryResponse> pagedList = new(
            entries.Select(e => new EntryResponse(e.Id, e.HabitId, e.Date, e.Quantity, e.CreatedAt)).ToList(),
            paging.Page,
            paging.PageSize,
            count
        );

        return new GetEntriesByHabitIdResponse(
            pagedList.Page,
            pagedList.PageSize,
            pagedList.TotalCount,
            pagedList.TotalPages,
            pagedList.HasPreviousPage,
            pagedList.HasNextPage,
            totalQuantity,
            pagedList.Items
        );
    }
}
